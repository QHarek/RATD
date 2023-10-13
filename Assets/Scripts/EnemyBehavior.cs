using System.Collections;
using UnityEngine;

public sealed class EnemyBehavior : EnemySubject
{
    [SerializeField] private bool _isBoss;

    private bool _isDead = false; //For death animation

    public bool IsDead => _isDead;
    public bool IsBoss => _isBoss;

    private void Start()
    {
        AddObserver(FindObjectOfType<TowerShooter>());
        AddObserver(FindObjectOfType<TowerStats>());
        AddObserver(FindObjectOfType<EnemiesCounter>());
        AddObserver(FindObjectOfType<PlayerWallet>());

        NotifyObservers(EnemyAction.Spawned);
        GetComponent<Animator>().SetBool("IsDead", _isDead);
    }

    internal void StartDie()
    {
        _isDead = true;
        gameObject.layer = LayerMask.NameToLayer("Default");

        if(!_isBoss)
            NotifyObservers(EnemyAction.Died);
        else
            NotifyObservers(EnemyAction.BossDied);

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
