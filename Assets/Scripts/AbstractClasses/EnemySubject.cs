using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySubject : MonoBehaviour
{
    private List<IEnemyObserver> _observers = new List<IEnemyObserver>();

    public void AddObserver(IEnemyObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IEnemyObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObservers(EnemyAction enemyAction)
    {
        foreach (IEnemyObserver observer in _observers)
        {
            observer.OnNotify(enemyAction);
        }
    }

}
