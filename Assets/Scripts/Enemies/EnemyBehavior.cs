using System.Collections;
using UnityEngine;

public sealed class EnemyBehavior : EnemySubject
{
    [SerializeField] private bool _isBoss;

    private bool _isDead = false; //For death animation

    public bool IsDead => _isDead;
    public bool IsBoss => _isBoss;

    private void Start()
    {
        AddObserver(FindObjectOfType<TowerShooter>());
        AddObserver(FindObjectOfType<TowerStats>());
        AddObserver(FindObjectOfType<EnemiesCounter>());
        AddObserver(FindObjectOfType<PlayerWallet>());
        AddObserver(FindObjectOfType<StatsFiller>());

        NotifyObservers(EnemyAction.Spawned, gameObject);
        GetComponent<Animator>().SetBool("IsDead", _isDead);
    }

    internal void StartDie()
    {
        if (!_isDead)
        {
            _isDead = true;
            gameObject.layer = LayerMask.NameToLayer("Default");

            if (!_isBoss)
                NotifyObservers(EnemyAction.Died, gameObject);
            else
                NotifyObservers(EnemyAction.BossDied, gameObject);

            GetComponent<EnemyMovement>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<Animator>().SetBool("IsDead", _isDead);
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
