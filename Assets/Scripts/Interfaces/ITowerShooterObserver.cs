using UnityEngine;

public interface ITowerShooterObserver
{
    public void OnNotify(TowerShooterAction towerShooterAction, Vector3 targetPosition);
}
