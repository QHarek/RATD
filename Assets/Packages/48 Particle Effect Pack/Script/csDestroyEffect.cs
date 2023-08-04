using UnityEngine;

public class csDestroyEffect : MonoBehaviour {

    [SerializeField] private float _destroyTime = 0.5f;

    private float _spawnTime;

    private void Start()
    {
        _spawnTime = Time.time;
    }

    private void Update () {
        if (Time.time > _spawnTime + _destroyTime)
            Destroy(gameObject);
    }
}
