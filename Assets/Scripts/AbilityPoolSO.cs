using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AbilityPool", menuName = "Object Pools/New Ability Pool", order = 51)]
public class AbilityPoolSO : ScriptableObject
{
    [SerializeField]
    private List<AbilityDataSO> _abilityPool;

    internal int AbilitiesCount()
    {
        return _abilityPool.Count;
    }

    internal AbilityDataSO GetAbilityAtIndex(int abilityIndex)
    {
        return _abilityPool[abilityIndex];
    }

    internal void RemoveAbilityAtIndex(int abilityIndex)
    {
        _abilityPool.RemoveAt(abilityIndex);
    }

    internal void AddAbility(AbilityDataSO ability)
    {
        _abilityPool.Add(ability);
    }
}
