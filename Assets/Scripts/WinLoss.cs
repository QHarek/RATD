using UnityEngine;

public class WinLoss : MonoBehaviour
{
    [SerializeField] private GameObject _tower;
    [SerializeField] private GameObject _spawnPoint;

    void Update()
    {
        if(_tower.GetComponent<TowerStats>().Lifes <= _spawnPoint.GetComponent<EnemySpawner>().EnemyCount)
        {
            Lose();
        }

        if(_spawnPoint.GetComponent<EnemySpawner>().WaveNumber == 101)
        {
            Win();
        }
    }

    private void Lose()
    {
        Time.timeScale = 0;
    }

    private void Win()
    {
        Time.timeScale = 0;
    }
}
