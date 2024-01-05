using UnityEngine;

public class TowerStats : MonoBehaviour, IEnemyObserver
{
    [SerializeField] private TowerInventory _towerInventory;
    [SerializeField] private float _shootingRange;
    [SerializeField] private float _currentFlatDamage;
    [SerializeField] private float _currentDamage;
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _baseAttackDelay;
    [SerializeField] private float _level;
    [SerializeField] private float _currentExperience;
    [SerializeField] private int _projectilesCount;
    [SerializeField] private int _lifes;

    public float ShootingRange => _shootingRange;
    public float CurrentDamage => _currentDamage;
    public float AttackDelay => _attackDelay;
    public int ProjectilesCount => _projectilesCount;
    public int Lifes => _lifes;

    private void Start()
    {
        _currentFlatDamage = _baseDamage;
        _currentDamage = _baseDamage;
    }

    public void UpdateDamageAndAttackSpeed()
    {
        _currentDamage = _currentFlatDamage * (1 + (float) _towerInventory.OverallDamageBonus / 100);
        _attackDelay = _baseAttackDelay / (1 + (float) _towerInventory.OverallAttackSpeedBonus / 100);
    }

    public void OnNotify(EnemyAction enemyAction)
    {
        if (enemyAction == EnemyAction.Died)
        {
            _currentExperience++;
            if (_currentExperience % 5 == 0)
            {
                _level++;
                _currentFlatDamage += _level * 2;
                UpdateDamageAndAttackSpeed();
            } 
        }
    }
}
