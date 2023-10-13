using UnityEngine;

public class TowerStats : MonoBehaviour, IEnemyObserver
{
    [SerializeField] private float _shootingRange;
    [SerializeField] private float _currentDamage;
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _level;
    [SerializeField] private float _currentExperience;
    [SerializeField] private int _projectilesCount;
    [SerializeField] private int _lifes;

    public float ShootingRange => _shootingRange;
    public float CurrentDamage => _currentDamage;
    public float AttackDelay => _attackDelay;
    public int ProjectilesCount => _projectilesCount;
    public int Lifes => _lifes;

    public void OnNotify(EnemyAction enemyAction)
    {
        if (enemyAction == EnemyAction.Died)
        {
            _currentExperience++;
            if (_currentExperience % 5 == 0)
            {
                _level++;
                _currentDamage += _level * 2;
            } 
        }
    }

    private void Start()
    {
        _currentDamage = _baseDamage;
    }
}
