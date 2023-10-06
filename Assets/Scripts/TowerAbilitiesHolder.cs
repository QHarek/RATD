using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TowerAbilitiesHolder : MonoBehaviour, ITowerShooterObserver
{
    [SerializeField] private List<AbilityDataSO> _abilitiesList;
    
    private List<ActiveAbilityDataSO> _activeAbilitiesList;
    private List<PassiveAbilityDataSO> _passiveAbilitiesList;
    private List<FamiliarAbilityDataSO> _familiarAbilitiesList;
    private TowerStats _towerStats;
    private Vector3 _abilityEffectSpawnPosition;

    public List<AbilityDataSO> AbilitiesList => _abilitiesList;

    private void Awake()
    {
        _activeAbilitiesList = new List<ActiveAbilityDataSO>();
        _passiveAbilitiesList = new List<PassiveAbilityDataSO>();
        _familiarAbilitiesList = new List<FamiliarAbilityDataSO>();
        _towerStats = GetComponent<TowerStats>();
    }

    private void Start()
    {
        SplitAbilities();
    }

    private void SplitAbilities()
    {
        foreach (AbilityDataSO ability in _abilitiesList)
        {
            switch (ability.AbilityType)
            {
                case AbilityType.Active:
                    {
                        _activeAbilitiesList.Add(ability as ActiveAbilityDataSO);
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
    }

    private void ActivateAbility(ActiveAbilityDataSO ability)
    {
        string className = ability.Name.Replace(" ", "");
        string methodName = "ActivateAbility";
        object[] methodParams = new object[] { _abilityEffectSpawnPosition, ability, _towerStats.CurrentDamage };

        System.Type type = System.Type.GetType(className);
        var abilityLogic = System.Activator.CreateInstance(type);

        if (type != null && typeof(IActiveAbility).IsAssignableFrom(type)) //Using only children of IActiveAbility
        {
            MethodInfo methodInfo = type.GetMethod(methodName);
            if (methodInfo != null)
            {
                methodInfo.Invoke(abilityLogic, methodParams);
            }
        }
    }

    internal void AddAbility(AbilityDataSO ability)
    {
        _abilitiesList.Add(ability);

        switch (ability.AbilityType)
        {
            case AbilityType.Active:
                {
                    _activeAbilitiesList.Add(ability as ActiveAbilityDataSO);
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
                if (activeAbility.ActivationChance >= Random.Range(1, 100))
                {
                    _abilityEffectSpawnPosition = targetPosition;
                    Instantiate(activeAbility.TriggerEffect, _abilityEffectSpawnPosition, Quaternion.identity);
                    ActivateAbility(activeAbility);
                }
            }
    }
}
