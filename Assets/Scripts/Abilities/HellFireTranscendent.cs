using System.Collections.Generic;
using UnityEngine;

public class HellFireTranscendent : MonoBehaviour, IEnemyObserver
{
    [SerializeField] private ActiveAbilityDataSO _ability;

    private const int DAMAGETICKSPERSECOND = 10;

    private List<GameObject> _targets;
    private float _lastHitTime;

    private float _towerDamage => FindObjectOfType<TowerStats>().CurrentDamage;

    private void Awake()
    {
        _targets = new List<GameObject>();
    }

    private void Update()
    {
        if (Timer.CustomTime - _lastHitTime > 1f / DAMAGETICKSPERSECOND)
        {
            HitEnemies();
        }
    }

    private void HitEnemies()
    {
        _lastHitTime = Timer.CustomTime;
        List<GameObject> enemiesToHit = new List<GameObject>();
        enemiesToHit.AddRange(_targets);

        if (_targets.Count > 0)
        {
            foreach (var enemy in enemiesToHit)
            {
                enemy.GetComponent<EnemyStats>().TakeDamage(_towerDamage * _ability.DamageMultiplier / DAMAGETICKSPERSECOND);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            other.gameObject.GetComponent<EnemyBehavior>().AddObserver(this);
            _targets.Add(other.gameObject);
        }
    }

    public void OnNotify(EnemyAction enemyAction, GameObject enemy)
    {
        if(enemyAction == EnemyAction.Died || enemyAction == EnemyAction.BossDied)
        {
            _targets.Remove(enemy);
        }
    }
}
