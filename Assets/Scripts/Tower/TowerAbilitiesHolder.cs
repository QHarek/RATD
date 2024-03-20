using System.Collections.Generic;
using UnityEngine;

public class TowerAbilitiesHolder : MonoBehaviour, ITowerShooterObserver
{
    [SerializeField] private List<AbilityDataSO> _abilitiesList;
    
    private List<ActiveAbilityDataSO> _activeAbilitiesList;
    private List<PassiveAbilityDataSO> _passiveAbilitiesList;
    private List<FamiliarAbilityDataSO> _familiarAbilitiesList;
    private Dictionary<AbilityDataSO, float> _cooldowns;
    private Vector3 _abilityEffectSpawnPosition;

    public List<AbilityDataSO> AbilitiesList => _abilitiesList;

    private void Awake()
    {
        _activeAbilitiesList = new List<ActiveAbilityDataSO>();
        _passiveAbilitiesList = new List<PassiveAbilityDataSO>();
        _familiarAbilitiesList = new List<FamiliarAbilityDataSO>();
        _cooldowns = new Dictionary<AbilityDataSO, float>();
    }

    internal void AddAbility(AbilityDataSO ability)
    {
        _abilitiesList.Add(ability);

        switch (ability.AbilityType)
        {
            case AbilityType.Active:
                {
                    _activeAbilitiesList.Add(ability as ActiveAbilityDataSO);
                    if(_activeAbilitiesList[_activeAbilitiesList.Count - 1].CooldownTime > 0)
                    {
                        _cooldowns.Add(ability, Timer.CustomTime);
                    }

                    break;
                }
            case AbilityType.Passive:
                {
                    _passiveAbilitiesList.Add(ability as PassiveAbilityDataSO);
                    break;
                }
            case AbilityType.Familiar:
                {
                    _familiarAbilitiesList.Add(ability as FamiliarAbilityDataSO);
                    break;
                }
        }
    }

    internal void RemoveAbility(AbilityDataSO ability)
    {
        _abilitiesList.Remove(ability);

        switch (ability.AbilityType)
        {
            case AbilityType.Active:
                {
                    _activeAbilitiesList.Remove(ability as ActiveAbilityDataSO);
                    if (_cooldowns.ContainsKey(ability))
                    {
                        _cooldowns.Remove(ability);
                    }
                    break;
                }
            case AbilityType.Passive:
                {
                    _passiveAbilitiesList.Remove(ability as PassiveAbilityDataSO);
                    break;
                }
            case AbilityType.Familiar:
                {
                    _familiarAbilitiesList.Remove(ability as FamiliarAbilityDataSO);
                    break;
                }
        }
    }

    public void OnNotify(TowerShooterAction towerShooterAction, Vector3 targetPosition)
    {
        if (towerShooterAction == TowerShooterAction.EnemyHitted && _activeAbilitiesList.Count > 0)
            foreach (ActiveAbilityDataSO activeAbility in _activeAbilitiesList)
            {
                if (activeAbility.ActivationChance >= Random.Range(1, 101) && Timer.CustomTime - _cooldowns[activeAbility] >= activeAbility.CooldownTime)
                {
                    _cooldowns[activeAbility] = Timer.CustomTime;
                    _abilityEffectSpawnPosition = targetPosition;
                    Instantiate(activeAbility.TriggerEffect, _abilityEffectSpawnPosition, Quaternion.identity);
                }
            }
    }
}
