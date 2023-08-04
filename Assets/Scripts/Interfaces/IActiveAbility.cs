using UnityEngine;

public interface IActiveAbility
{
    public void ActivateAbility(Vector3 abilityEffectSpawnPosition, ActiveAbilityDataSO ability, float towerDamage);
}
