using UnityEngine;

public interface IEnemyObserver
{
    public void OnNotify(EnemyAction enemyAction, GameObject enemy);
}
