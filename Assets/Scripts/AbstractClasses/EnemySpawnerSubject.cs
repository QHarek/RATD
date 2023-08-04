using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawnerSubject : MonoBehaviour
{
    private List<IEnemySpawnerObserver> _observers = new List<IEnemySpawnerObserver>();

    public void AddObserver(IEnemySpawnerObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IEnemySpawnerObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObservers(int waveNumber)
    {
        foreach (IEnemySpawnerObserver observer in _observers)
        {
            observer.OnNotify(waveNumber);
        }
    }

}

