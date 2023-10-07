using UnityEngine;

[CreateAssetMenu(fileName = "New ActiveAbilityData", menuName = "Ability Data/New Active Ability Data", order = 51)]
public class ActiveAbilityDataSO : AbilityDataSO
{
    [SerializeField] private GameObject _triggerEffect;
    [SerializeField] private float _activationChance;
    [SerializeField] private float _activeTime;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _radius;
    [SerializeField] private float _damageMultiplier;

    public GameObject TriggerEffect => _triggerEffect;
    public float ActivationChance  => _activationChance;
    public float ActiveTime => _activeTime;
    public float CooldownTime => _cooldownTime;
    public float Radius => _radius;
    public float DamageMultiplier => _damageMultiplier;

    private void Awake()
    {
        _abilityType = AbilityType.Active;
    }

    internal override void Updrade()
    {
        if (_level < _maxLevel)
        {
            _level++;
        }
    }
}
