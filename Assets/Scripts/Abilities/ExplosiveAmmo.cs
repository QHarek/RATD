using UnityEngine;

public class ExplosiveAmmo : IActiveAbility
{
    public void ActivateAbility(Vector3 abilityEffectSpawnPosition, ActiveAbilityDataSO ability, float towerDamage)
    {
        foreach (var enemyCollider in Physics.OverlapSphere(
            abilityEffectSpawnPosition, ability.Radius, 1 << LayerMask.NameToLayer("Enemies")))
        {
            enemyCollider.gameObject.GetComponent<EnemyStats>().TakeDamage(ability.DamageMultiplier * towerDamage);
        }
    }
}
