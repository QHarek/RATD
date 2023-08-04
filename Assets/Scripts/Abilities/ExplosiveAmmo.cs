using UnityEngine;

public class ExplosiveAmmo : IActiveAbility
{
    public void ActivateAbility(Vector3 abilityEffectSpawnPosition, ActiveAbilityDataSO ability, float towerDamage)
    {
        foreach (var enemyCollider in Physics.OverlapSphere(
            abilityEffectSpawnPosition, ability.Radius, 1 << LayerMask.NameToLayer("Enemies")))
        {
            enemyCollider.gameObject.GetComponent<EnemyHP>().TakeDamage(ability.DamageMultiplier * towerDamage);
        }
    }
}
