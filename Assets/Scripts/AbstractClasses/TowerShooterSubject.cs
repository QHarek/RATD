using System.Collections.Generic;
using UnityEngine;

public abstract class TowerShooterSubject : MonoBehaviour
{
    private List<ITowerShooterObserver> _observers = new List<ITowerShooterObserver>();

    public void AddObserver(ITowerShooterObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(ITowerShooterObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObservers(TowerShooterAction towerShooterAction, Transform target)
    {
        foreach (ITowerShooterObserver observer in _observers)
        {
            observer.OnNotify(towerShooterAction, target);
        }
    }
}
