using UnityEngine;

[CreateAssetMenu(fileName = "New ActiveAbilityData", menuName = "Ability Data/New Active Ability Data", order = 51)]
public class ActiveAbilityDataSO : AbilityDataSO
{
    [SerializeField] private GameObject _triggerEffect;
    [SerializeField] private float _basicActivationChance;
    [SerializeField] private float _basicActiveTime;
    [SerializeField] private float _basicCooldownTime;
    [SerializeField] private float _basicRadius;
    [SerializeField] private float _basicDamageMultiplier;
    [SerializeField] private float _activationChancePerUpgrade;
    [SerializeField] private float _damageMultiplierPerUpgrade;
    [SerializeField] private float _radiusPerUpgrade;

    private float _activationChance;
    private float _activeTime;
    private float _cooldownTime;
    private float _radius;
    private float _damageMultiplier;

    public GameObject TriggerEffect => _triggerEffect;
    public float ActivationChance  => _activationChance;
    public float ActiveTime => _activeTime;
    public float CooldownTime => _cooldownTime;
    public float Radius => _radius;
    public float DamageMultiplier => _damageMultiplier;

    private void Awake()
    {
        _abilityType = AbilityType.Active;
        _activationChance = _basicActivationChance;
        _activeTime = _basicActiveTime;
        _cooldownTime = _basicCooldownTime;
        _radius = _basicRadius;
        _damageMultiplier = _basicDamageMultiplier;
    }

    internal override void Updrade(int level)
    {
        if (_level < _maxLevel)
        {
            _level++;
            _activationChance = _basicActivationChance + level * _activationChancePerUpgrade;
            _damageMultiplier = _basicDamageMultiplier + level * _damageMultiplierPerUpgrade;
            _radius = _basicRadius + level * _radiusPerUpgrade;
        }
    }
}
