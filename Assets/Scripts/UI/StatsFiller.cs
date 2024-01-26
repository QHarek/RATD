using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsFiller : MonoBehaviour, IEnemyStatsObserver, IEnemyObserver, ITowerStatsObserver
{
    [SerializeField] private GameObject _statsWindow;
    [SerializeField] private Slider _experienceBar;
    [SerializeField] private TextMeshProUGUI _levelField;
    [SerializeField] private TextMeshProUGUI _totalTowerDamageField;
    [SerializeField] private TextMeshProUGUI _baseTowerDamageField;
    [SerializeField] private TextMeshProUGUI _bonusTowerDamageField;
    [SerializeField] private TextMeshProUGUI _totalTowerAttackDelayField;
    [SerializeField] private TextMeshProUGUI _baseTowerAttackDelayField;
    [SerializeField] private TextMeshProUGUI _bonusTowerAttackSpeedField;
    [SerializeField] private TextMeshProUGUI _enemiesKilledField;
    [SerializeField] private TextMeshProUGUI _totalDamageDealtField;

    private int _currentExperience;
    private int _currentLevel;
    private float _totalDamage;
    private float _baseDamage;
    private float _bonusDamage;
    private float _totalAttackDelay;
    private float _baseAttackDelay;
    private float _bonusAttackSpeed;
    private int _enemiesKilled;
    private float _totalDamageDealt;

    private void Start()
    {
        _currentExperience = 0;
        _currentLevel = 0;
        _totalDamage = 0;
        _baseDamage = 0;
        _bonusDamage = 0;
        _totalAttackDelay = 0;
        _baseAttackDelay = 0;
        _bonusAttackSpeed = 0;
        _totalDamageDealt = 0;
        _enemiesKilled = 0;
        _experienceBar.maxValue = TowerStats.EXPERIENCEFORLEVELUP;
    }

    internal void UpdateFields()
    {
        _experienceBar.value = _currentExperience % TowerStats.EXPERIENCEFORLEVELUP;
        _levelField.text = "LEVEL   " + _currentLevel.ToString();
        _totalTowerDamageField.text = _totalDamage.ToString();
        _baseTowerDamageField.text = _baseDamage.ToString();
        _bonusTowerDamageField.text = _bonusDamage.ToString() + "%";
        _totalTowerAttackDelayField.text = _totalAttackDelay.ToString().Substring(0, Mathf.Min(4, _totalAttackDelay.ToString().Length)) + " sec";
        _baseTowerAttackDelayField.text = _baseAttackDelay.ToString() + " sec";
        _bonusTowerAttackSpeedField.text = _bonusAttackSpeed.ToString() + "%";
        _enemiesKilledField.text = _enemiesKilled.ToString();
        _totalDamageDealtField.text = ((int) _totalDamageDealt).ToString();
    }

    void ITowerStatsObserver.OnNotifyExperienceGained(int currentExperience, int level)
    {
        _currentExperience = currentExperience;
        _currentLevel = level;

        if (_statsWindow.activeSelf)
        {
            _experienceBar.value = _currentExperience % TowerStats.EXPERIENCEFORLEVELUP;
            _levelField.text = "LEVEL   " + _currentLevel.ToString();
        }
    }

    void ITowerStatsObserver.OnNotifyDamageStats(float totalDamage, float baseDamage, float bonusDamage, float totalAttackDelay, float baseAttackDelay, float bonusAttackSpeed)
    {
        _totalDamage = totalDamage;
        _baseDamage = baseDamage;
        _bonusDamage = bonusDamage;
        _totalAttackDelay = totalAttackDelay;
        _baseAttackDelay = baseAttackDelay;
        _bonusAttackSpeed = bonusAttackSpeed;

        if (_statsWindow.activeSelf)
        {
            _totalTowerDamageField.text = _totalDamage.ToString();
            _baseTowerDamageField.text = _baseDamage.ToString();
            _bonusTowerDamageField.text = _bonusDamage.ToString() + "%";
            _totalTowerAttackDelayField.text = _totalAttackDelay.ToString().Substring(0, Mathf.Min(4, _totalAttackDelay.ToString().Length)) + " sec";
            _baseTowerAttackDelayField.text = _baseAttackDelay.ToString() + " sec";
            _bonusTowerAttackSpeedField.text = _bonusAttackSpeed.ToString() + "%";
        }
    }

    void IEnemyObserver.OnNotify(EnemyAction enemyAction)
    {
        if (enemyAction == EnemyAction.Died || enemyAction == EnemyAction.BossDied)
        {
            _enemiesKilled += 1;

            if (_statsWindow.activeSelf)
            {
                _enemiesKilledField.text = _enemiesKilled.ToString();
            }
        }
    }

    void IEnemyStatsObserver.OnNotify(EnemyStatsAction enemyStatsAction, float value)
    {
        if(enemyStatsAction == EnemyStatsAction.UpdateHP && value < 0)
        {
            _totalDamageDealt += -value;

            if (_statsWindow.activeSelf)
            {
                _totalDamageDealtField.text = _totalDamageDealt.ToString();
            }
        }
    }
}
