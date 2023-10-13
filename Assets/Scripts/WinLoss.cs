using System.Collections.Generic;
using UnityEngine;

public class WinLoss : MonoBehaviour
{
    [SerializeField] private GameObject _tower;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _winLossCavas;
    [SerializeField] private TMPro.TextMeshProUGUI _winLossText;
    [SerializeField] private List<GameObject> _objectsToHide;

    void Update()
    {
        if(_tower.GetComponent<TowerStats>().Lifes <= _spawnPoint.GetComponent<EnemySpawner>().EnemyCount)
        {
            Lose();
        }

        if(_spawnPoint.GetComponent<EnemySpawner>().WaveNumber == 101 && _spawnPoint.GetComponent<EnemySpawner>().EnemyCount == 0)
        {
            Win();
        }
    }

    private void Lose()
    {
        foreach (var gameObject in _objectsToHide)
        {
            gameObject.SetActive(false);
        }

        _winLossCavas.SetActive(true);
        _winLossText.text = "Game Over";
        _winLossText.color = Color.red;
        Time.timeScale = 0;
    }

    private void Win()
    {
        foreach (var gameObject in _objectsToHide)
        {
            gameObject.SetActive(false);
        }

        _winLossCavas.SetActive(true);
        _winLossText.text = "You nailed it!";
        _winLossText.color = Color.green;
        Time.timeScale = 0;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
