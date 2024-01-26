public interface ITowerStatsObserver
{
    public void OnNotifyDamageStats(float totalDamage, float baseDamage, float bonusDamage, float totalAttackDelay, float baseAttackDelay, float bonusAttackSpeed);
    public void OnNotifyExperienceGained(int currentExperience, int level);
}