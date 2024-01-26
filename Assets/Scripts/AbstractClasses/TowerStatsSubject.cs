using System.Collections.Generic;
using UnityEngine;

public abstract class TowerStatsSubject : MonoBehaviour
{
    private List<ITowerStatsObserver> _observers = new List<ITowerStatsObserver>();

    public void AddObserver(ITowerStatsObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(ITowerStatsObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObserversDamageStats(float totalDamage, float baseDamage, float bonusDamage, float totalAttackSpeed, float baseAttackSpeed, float bonusAttackSpeed)
    {
        foreach (ITowerStatsObserver observer in _observers)
        {
            observer.OnNotifyDamageStats(totalDamage, baseDamage, bonusDamage, totalAttackSpeed, baseAttackSpeed, bonusAttackSpeed);
        }
    }

    protected void NotifyObserversExperienceGained(int currentExperience, int level)
    {
        foreach (ITowerStatsObserver observer in _observers)
        {
            observer.OnNotifyExperienceGained(currentExperience, level);
        }
    }
}
