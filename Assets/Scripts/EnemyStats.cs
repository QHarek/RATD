using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public sealed class EnemyStats : EnemyStatsSubject
{
    [SerializeField] private TextAsset _enemyHPByWaveJSON;
    [SerializeField] private TextAsset _bossHPByWaveJSON;

    private EnemyBehavior _enemyBehavior;

    private static Dictionary<int, float> _enemyHPByWave;
    private static Dictionary<int, float> _bossHPByWave;
    private float _maxHP;
    private float _currentHP;

    public float CurrentHP => _currentHP;
    public float MaxHP => _maxHP;

    private void Awake()
    {
        _enemyBehavior = GetComponent<EnemyBehavior>();
    }

    private void Start()
    {
        AddObserver(FindObjectOfType<StatsFiller>());
    }

    internal void TakeDamage(float damage)
    {
        float recievedDamage = Mathf.Clamp(damage, 0, _currentHP);
        _currentHP -= recievedDamage;
        NotifyObservers(EnemyStatsAction.UpdateHP, -recievedDamage);

        if (_currentHP <= 0)
            _enemyBehavior.StartDie();
    }

    internal void TakeHeal(float heal)
    {
        _currentHP = Mathf.Min(_currentHP + heal, _maxHP);
        NotifyObservers(EnemyStatsAction.UpdateHP, heal);
    }

    internal void ModifyHPByWaveNumber(int waveNumber)
    {
        if (_enemyHPByWave != null)
        {
            _maxHP = _enemyHPByWave[waveNumber];
            _currentHP = _maxHP;
        }
        else
        {
            _enemyHPByWave = JsonConvert.DeserializeObject<Dictionary<int, float>>(_enemyHPByWaveJSON.text);
            _maxHP = _enemyHPByWave[waveNumber];
            _currentHP = _maxHP;
        }
    }
    
    internal void ModifyBossHPByWaveNumber(int waveNumber)
    {
        if (_bossHPByWave != null)
        {
            _maxHP = _bossHPByWave[waveNumber];
            _currentHP = _maxHP;
        }
        else
        {
            _bossHPByWave = JsonConvert.DeserializeObject<Dictionary<int, float>>(_bossHPByWaveJSON.text);
            _maxHP = _bossHPByWave[waveNumber];
            _currentHP = _maxHP;
        }
    }
}
