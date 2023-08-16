using System.Text;
using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour, IEnemySpawnerObserver
{
    private const string WAVELABEL = "Wave: ";

    private TextMeshProUGUI _waveCounter;
    private StringBuilder _stringBuilder;

    private void Awake()
    {
        _waveCounter = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _stringBuilder = new StringBuilder();
        FindObjectOfType<EnemySpawnerSubject>().AddObserver(this);
    }

    public void OnNotify(int waveNumber)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(WAVELABEL);
        _stringBuilder.Append(waveNumber);
        _waveCounter.text = _stringBuilder.ToString();
    }
}
