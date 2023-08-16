using System.Collections;
using UnityEngine;

public sealed class EnemyBehavior : EnemySubject
{
    private bool _isDead = false; //For death animation

    public bool IsDead => _isDead;

    private void Start()
    {
        AddObserver(FindObjectOfType<TowerShooter>());
        AddObserver(FindObjectOfType<TowerStats>());
        AddObserver(FindObjectOfType<EnemiesCounter>());

        NotifyObservers(EnemyAction.Spawned);
        GetComponent<Animator>().SetBool("IsDead", _isDead);
    }

    internal void StartDie()
    {
        _isDead = true;
        gameObject.layer = LayerMask.NameToLayer("Default");
        NotifyObservers(EnemyAction.Die);
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().SetBool("IsDead", _isDead);
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
