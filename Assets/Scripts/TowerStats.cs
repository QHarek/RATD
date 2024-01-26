using UnityEngine;

public class TowerStats : TowerStatsSubject, IEnemyObserver
{
    [SerializeField] private TowerInventory _towerInventory;
    [SerializeField] private float _shootingRange;
    [SerializeField] private float _currentFlatDamage;
    [SerializeField] private float _currentDamage;
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _baseAttackDelay;
    [SerializeField] private int _level;
    [SerializeField] private int _currentExperience;
    [SerializeField] private int _projectilesCount;
    [SerializeField] private int _lifes;

    public float ShootingRange => _shootingRange;
    public float CurrentDamage => _currentDamage;
    public float AttackDelay => _attackDelay;
    public int ProjectilesCount => _projectilesCount;
    public int Lifes => _lifes;

    public const int EXPERIENCEFORLEVELUP = 5;

    private void Start()
    {
        AddObserver(FindObjectOfType<StatsFiller>());

        _currentFlatDamage = _baseDamage;
        _currentDamage = _baseDamage;
        _attackDelay = _baseAttackDelay;

        NotifyObserversDamageStats(_currentDamage, _currentFlatDamage, _towerInventory.OverallDamageBonus, _attackDelay, _baseAttackDelay, _towerInventory.OverallAttackSpeedBonus);
    }

    public void UpdateDamageAndAttackSpeed()
    {
        _currentDamage = _currentFlatDamage * (1 + (float) _towerInventory.OverallDamageBonus / 100);
        _attackDelay = _baseAttackDelay / (1 + (float) _towerInventory.OverallAttackSpeedBonus / 100);

        NotifyObserversDamageStats(_currentDamage, _currentFlatDamage, _towerInventory.OverallDamageBonus, _attackDelay, _baseAttackDelay, _towerInventory.OverallAttackSpeedBonus);
    }

    public void OnNotify(EnemyAction enemyAction)
    {
        if (enemyAction == EnemyAction.Died)
        {
            _currentExperience++;
            if (_currentExperience % EXPERIENCEFORLEVELUP == 0)
            {
                _level++;
                _currentFlatDamage += _level * 2;
                UpdateDamageAndAttackSpeed();
            }

            NotifyObserversExperienceGained(_currentExperience, _level);
        }
    }
}
