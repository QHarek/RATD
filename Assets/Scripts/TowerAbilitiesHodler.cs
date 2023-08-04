using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TowerAbilitiesHodler : MonoBehaviour, ITowerShooterObserver
{
    [SerializeField] private List<ActiveAbilityDataSO> _abilitiesList;

    private TowerShooter _towerShooter;
    private TowerStats _towerStats;
    private Vector3 _abilityEffectSpawnPosition;

    private void Awake()
    {
        _towerShooter = GetComponent<TowerShooter>();
        _towerStats = GetComponent<TowerStats>();
    }

    public void OnNotify(TowerShooterAction towerShooterAction)
    {
        if(towerShooterAction == TowerShooterAction.EnemyHitted)
            foreach (ActiveAbilityDataSO ability in _abilitiesList)
            {
                if(ability.AbilityType == AbilityType.Active)
                {
                    if (ability.ActivationChance >= Random.Range(1,100))
                    {
                        _abilityEffectSpawnPosition = _towerShooter.Target.transform.position;
                        Instantiate(ability.TriggerEffect, _abilityEffectSpawnPosition, Quaternion.identity);
                        ActivateAbility(ability);
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
}
