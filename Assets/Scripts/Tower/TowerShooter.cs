using System.Collections.Generic;
using UnityEngine;

public sealed class TowerShooter : TowerShooterSubject, IEnemyObserver
{
    private const float PredictionTime = 0.15f;
    private const float ProjectileSpeedModifier = 4;

    private List<GameObject> _enemies = new List<GameObject>();
    private ParticleSystem _weapon;
    private GameObject _target;
    private TowerStats _towerStats;

    private float _lastShotTime;

    private void Awake()
    {
        _towerStats = GetComponent<TowerStats>();
        _weapon = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        AddObserver(GetComponent<TowerAbilitiesHolder>());
    }

    private void FixedUpdate()
    {
        if (_target && 
            Vector3.Distance(transform.position, _target.transform.position) <= _towerStats.ShootingRange && 
            Timer.CustomTime > _lastShotTime + _towerStats.AttackDelay)
        {
            Shoot();
            _lastShotTime = Timer.CustomTime;
        }
    }

    //Capture enemies, that entered shooting range
    private void OnTriggerEnter(Collider other)
    {
        _enemies.Add(other.gameObject);
        if(!_target)
            ChangeTarget();
    }

    private void ChangeTarget()
    {
        if (_enemies.Count > 0)
        {
            _target = _enemies[0];
            _weapon.trigger.AddCollider(_target.GetComponent<Collider>());
        }
        else
        {
            _target = null;
        }
    }

    private void Shoot()
    {
        Vector3 esteminatedTargetPosition = _target.GetComponent<EnemyMovement>().NextPredictedPosition;
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.velocity = esteminatedTargetPosition * ProjectileSpeedModifier;
        _weapon.Emit(emitParams, _towerStats.ProjectilesCount);
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

        int numEnter = _weapon.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, 
            out ParticleSystem.ColliderData strickenEnemiesData);

        for (int i = 0; i < numEnter; i++)
        {
            var strickenEnemy = strickenEnemiesData.GetCollider(i, 0).gameObject;
            if (strickenEnemy != null)
            {
                strickenEnemy.GetComponent<EnemyStats>().TakeDamage(_towerStats.CurrentDamage);
                NotifyObservers(TowerShooterAction.EnemyHitted, strickenEnemy.transform.position);
                return;
            }
        }        
    }

    public void OnNotify(EnemyAction enemyAction, GameObject enemy)
    {
        if (enemyAction == EnemyAction.Died || enemyAction == EnemyAction.BossDied)
        {
            _enemies.Remove(enemy);
            _weapon.trigger.RemoveCollider(enemy.GetComponent<Collider>());

            if (enemy == _target)
            {
                ChangeTarget();
            }
        }
    }
}
