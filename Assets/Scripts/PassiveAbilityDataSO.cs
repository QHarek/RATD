using UnityEngine;

[CreateAssetMenu(fileName = "New PassiveAbilityData", menuName = "Ability Data/New Passive Ability Data", order = 51)]
public class PassiveAbilityDataSO : AbilityDataSO
{


    internal override void Updrade(int level)
    {
        if (_level < _maxLevel)
        {
            _level++;
        }
    }
}

