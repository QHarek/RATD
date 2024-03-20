using UnityEngine;

[CreateAssetMenu(fileName = "New FamiliarAbilityData", menuName = "Ability Data/New Familiar Ability Data", order = 51)]
public class FamiliarAbilityDataSO : AbilityDataSO
{
    internal override void InitializeAbility()
    {
        throw new System.NotImplementedException();
    }

    internal override void Updrade(int level)
    {
        if (_level < _maxLevel)
        {
            _level++;
        }
    }
}

