using UnityEngine;

public interface ITowerShooterObserver
{
    public void OnNotify(TowerShooterAction towerShooterAction, Transform target);
}
