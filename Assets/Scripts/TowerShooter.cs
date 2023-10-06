using System;
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
    private Vector3 _targetPosition;

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
        if (_target && _target.GetComponent<EnemyMovement>().OnPath && 
            Vector3.Distance(transform.position, _target.transform.position) <= _towerStats.ShootingRange && 
            Time.time > _lastShotTime + _towerStats.AttackDelay)
        {
            Shoot();
            _lastShotTime = Time.time;
        }
    }

    //Capture enemies, that entered shooting range
    private void OnTriggerEnter(Collider other)
    {
        _enemies.Add(other.gameObject);
        if(!_target)
            GetTarget();
    }

    private void GetTarget()
    {
        if (_enemies.Count > 0)
        {
            _target = _enemies[0];
            _weapon.trigger.AddCollider(_target.GetComponent<Collider>());
            _targetPosition = _target.transform.position;
        }
        else
        {
            _target = null;
        }
    }

    private void Shoot()
    {
        Vector3 esteminatedTargetPosition = PredictTargetPosition();
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.velocity = esteminatedTargetPosition * ProjectileSpeedModifier;
        _weapon.Emit(emitParams, _towerStats.ProjectilesCount);
    }

    private Vector3 PredictTargetPosition()
    {
        float _nextPathAngle = _target.GetComponent<EnemyMovement>().NextPathAngle +
            _target.GetComponent<EnemyMovement>().AngleVelocity * PredictionTime;
        float estEnemyX = transform.position.x - Mathf.Cos(_nextPathAngle) * EnemyMovement.PathRadius;
        float estEnemyY = transform.position.y;
        float estEnemyZ = transform.position.z - Mathf.Sin(_nextPathAngle) * EnemyMovement.PathRadius;
        return new Vector3(estEnemyX, estEnemyY, estEnemyZ);
    }

    internal void ChangeTarget()
    {
        UpdateEnemiesList();
        GetTarget();
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
                strickenEnemy.GetComponent<EnemyHP>().TakeDamage(_towerStats.CurrentDamage);
                NotifyObservers(TowerShooterAction.EnemyHitted, _targetPosition);
                return;
            }
        }        
    }

    private void UpdateEnemiesList()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].layer == LayerMask.NameToLayer("Default"))
            {
                if(_weapon.trigger.GetCollider(0) == _enemies[i].GetComponent<Collider>())
                    _weapon.trigger.RemoveCollider(_enemies[i].GetComponent<Collider>());

                _enemies.RemoveAt(i);
                i--;
            }
        }
    }

    public void OnNotify(EnemyAction enemyAction)
    {
        if (enemyAction == EnemyAction.Die)
        {
            ChangeTarget();
        }
    }
}
