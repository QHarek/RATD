using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStatsSubject : MonoBehaviour
{
    private List<IEnemyStatsObserver> _observers = new List<IEnemyStatsObserver>();

    public void AddObserver(IEnemyStatsObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IEnemyStatsObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObservers(EnemyStatsAction enemyStatsAction)
    {
        foreach (IEnemyStatsObserver observer in _observers)
        {
            observer.OnNotify(enemyStatsAction);
        }
    }

}
