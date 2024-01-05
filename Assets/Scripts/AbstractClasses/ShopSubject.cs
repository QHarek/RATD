using System.Collections.Generic;
using UnityEngine;

public abstract class ShopSubject : MonoBehaviour
{
    private List<IShopObserver> _observers = new List<IShopObserver>();

    public void AddObserver(IShopObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IShopObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObservers(ShopAction shopAction, ItemDataSO item)
    {
        foreach (IShopObserver observer in _observers)
        {
            observer.OnNotify(shopAction, item);
        }
    }
}
