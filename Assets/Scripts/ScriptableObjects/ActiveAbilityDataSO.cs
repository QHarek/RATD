using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New ActiveAbilityData", menuName = "Ability Data/New Active Ability Data", order = 51)]
public class ActiveAbilityDataSO : AbilityDataSO 
{
    [SerializeField] private GameObject _basicTriggerEffect;
    [SerializeField] private float _basicActivationChance;
    [SerializeField] private float _basicActiveTime;
    [SerializeField] private float _basicCooldownTime;
    [SerializeField] private float _basicRadius;
    [SerializeField] private float _basicDamageMultiplier;
    [SerializeField] private float[] _basicBonusEffectsValue;

    [SerializeField] private float _activationChancePerUpgrade;
    [SerializeField] private float _activeTimePerUpgrade;
    [SerializeField] private float _cooldownReducePerUpgrade;
    [SerializeField] private float _radiusPerUpgrade;
    [SerializeField] private float _damageMultiplierPerUpgrade;
    [SerializeField] private float[] _bonusEffectsValuePerUpgrade;

    [SerializeField] private Sprite _transcendentIconSprite;
    [SerializeField] private GameObject _transcendentTriggerEffect;
    [SerializeField] private float _transcendentActivationChance;
    [SerializeField] private float _transcendentActiveTime;
    [SerializeField] private float _transcendentCooldownTime;
    [SerializeField] private float _transcendentRadius;
    [SerializeField] private float _transcendentDamageMultiplier;
    [SerializeField] private float[] _transcendentBonusEffectsValuePerUpgrade;

    private GameObject _triggerEffect;
    private float _activationChance;
    private float _activeTime;
    private float _cooldownTime;
    private float _radius;
    private float _damageMultiplier;

    private GameObject _activeTriggerEffect;

    public GameObject TriggerEffect => _triggerEffect;
    public float ActivationChance  => _activationChance;
    public float ActiveTime => _activeTime;
    public float CooldownTime => _cooldownTime;
    public float Radius => _radius;
    public float DamageMultiplier => _damageMultiplier;

    internal override void InitializeAbility()
    {
        _abilityType = AbilityType.Active;
        _level = Random.Range(0, _maxRandomLevel);

        _triggerEffect = _basicTriggerEffect;
        _activationChance = _basicActivationChance + _level * _activationChancePerUpgrade;
        _activeTime = _basicActiveTime + _level * _activeTimePerUpgrade;
        _cooldownTime = _basicCooldownTime - _level * _cooldownReducePerUpgrade;
        _radius = _basicRadius + _level * _radiusPerUpgrade;
        _damageMultiplier = _basicDamageMultiplier + _level * _damageMultiplierPerUpgrade;

        if (_activationChance == -1)
        {
            _activeTriggerEffect = Instantiate(_triggerEffect, GameObject.Find("TowerMage").transform.position, Quaternion.identity);
        }
    }

    internal override void Updrade(int level)
    {
        if (_level < _maxLevel)
        {
            _level++;

            if(_level == _maxLevel)
            {
                _activationChance = _transcendentActivationChance;
                _activeTime = _transcendentActiveTime;
                _cooldownTime = _transcendentCooldownTime;
                _radius = _transcendentRadius;
                _damageMultiplier = _transcendentDamageMultiplier;
                _triggerEffect = _transcendentTriggerEffect;

                GameObject.Find("Abilities").GetComponentsInChildren<Image>().FirstOrDefault(image => image.sprite.name == IconSprite.name).sprite = _transcendentIconSprite;

                if (_activationChance == -1)
                {
                    Destroy(_activeTriggerEffect);
                    Instantiate(_transcendentTriggerEffect, GameObject.Find("TowerMage").transform.position, Quaternion.identity);
                }
            }
            else
            {
                _activationChance += _activationChancePerUpgrade;
                _activeTime += _activeTimePerUpgrade;
                _cooldownTime -= _cooldownReducePerUpgrade;
                _radius += _radiusPerUpgrade;
                _damageMultiplier += _damageMultiplierPerUpgrade;
            }
        }
    }
}
