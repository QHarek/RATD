using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemySpawner : EnemySpawnerSubject
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _bosses;
    [SerializeField] private float _startGameDelay;
    [SerializeField] private float _delayBetweenWawes;
    [SerializeField] private float _enemySpawnDelay;
    [SerializeField] private int _enemyCount;

    private float _timer;
    private float _lastEnemySpawnedTime;
    private int _waveNumber;
    private bool _gameStarted;

    public const int MAXWAVENUMBER = 100;

    public float StartGameDelay => _startGameDelay;
    public int WaveNumber => _waveNumber;

    private void Start()
    {
        _gameStarted = false;
        _waveNumber = 0;
        _timer = _startGameDelay;
    }

    private void FixedUpdate()
    {
        if (!_gameStarted)
            _timer -= Time.deltaTime;
        else
            _timer += Time.deltaTime;

        if (Time.time > _startGameDelay && !_gameStarted)
            StartGame();

        if (_timer > _waveNumber * _delayBetweenWawes && _gameStarted && _waveNumber <= MAXWAVENUMBER)
            StartNextWave();
    }

    private void StartGame()
    {
        StartNextWave();
        _gameStarted = true;
    }

    private IEnumerator SpawnEnemies()
    {
        int enemySpawnCounter = 0;
        List<GameObject> waveEnemyPool;
        DefineEnemyPool(out waveEnemyPool);

        while (enemySpawnCounter < waveEnemyPool.Count)
        {
            GameObject enemy = Instantiate(waveEnemyPool[enemySpawnCounter], transform.position,
                Quaternion.Euler(0, 90, 0), GameObject.Find("EnemyPool").transform);

            if (_waveNumber % 10 == 0 && enemySpawnCounter == waveEnemyPool.Count - 1)
                enemy.GetComponent<EnemyHP>().ModifyBossHPByWaveNumber(_waveNumber);
            else
                enemy.GetComponent<EnemyHP>().ModifyHPByWaveNumber(_waveNumber);

            _lastEnemySpawnedTime = Time.time;
            enemySpawnCounter++;
            yield return new WaitUntil(() => _lastEnemySpawnedTime + _enemySpawnDelay < Time.time);
        }
    }       

    private void StartNextWave()
    {
        _waveNumber++;
        NotifyObservers(_waveNumber);
        StartCoroutine(SpawnEnemies());
    }

    private void DefineEnemyPool(out List<GameObject> enemyPool)
    {
        int waveType = _waveNumber - _waveNumber / 10 * 10; //Getting units digit
        enemyPool = new List<GameObject>();

        switch (waveType)
        {
            case 1:
                {
                    for(int i = 0; i < _enemyCount; i++)
                        enemyPool.Add(_enemies[i % 2]);
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < _enemyCount; i++)
                        enemyPool.Add(_enemies[i % 2 + 2]);
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < _enemyCount; i++)
                        enemyPool.Add(_enemies[i % 2 + 4]);
                    break;
                }
            case 4:
                {
                    for (int i = 0; i < _enemyCount; i++)
                        enemyPool.Add(_enemies[i % 2]);
                    break;
                }
            case 5:
                {
                    for (int i = 0; i < _enemyCount; i++)
                        enemyPool.Add(_enemies[i % 2 + 2]);
                    break;
                }
            case 6:
                {
                    for (int i = 0; i < _enemyCount; i++)
                        enemyPool.Add(_enemies[i % 2 + 4]);
                    break;
                }
            case 7:
                {
                    for (int i = 0; i < _enemyCount; i++)
                        enemyPool.Add(_enemies[i % 2]);
                    break;
                }
            case 8:
                {
                    for (int i = 0; i < _enemyCount; i++)
                        enemyPool.Add(_enemies[i % 2 + 2]);
                    break;
                }
            case 9:
                {
                    for (int i = 0; i < _enemyCount; i++)
                        enemyPool.Add(_enemies[i % 2 + 4]);
                    break;
                }
            case 0:
                {
                    for (int i = 0; i < _enemyCount; i++)
                        enemyPool.Add(_enemies[i % 6]);
                    enemyPool.Add(_bosses[0]);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    internal void IncreaceEnemyCount(int value)
    {
        _enemyCount += value;
    }
}
