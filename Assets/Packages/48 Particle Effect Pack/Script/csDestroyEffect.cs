using UnityEngine;

public class csDestroyEffect : MonoBehaviour {

    [SerializeField] private float _destroyTime = 0.5f;

    private float _spawnTime;

    private void Start()
    {
        _spawnTime = Timer.CustomTime;
    }

    private void Update () {
        if (Timer.CustomTime > _spawnTime + _destroyTime)
            Destroy(gameObject);
    }
}
