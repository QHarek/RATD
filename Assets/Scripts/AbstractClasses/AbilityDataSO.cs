using UnityEngine;

public abstract class AbilityDataSO : ScriptableObject
{
    protected internal AbilityType _abilityType;

    public AbilityType AbilityType => _abilityType;
}
