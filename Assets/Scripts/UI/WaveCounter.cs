using System.Text;
using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour, IEnemySpawnerObserver
{
    private TextMeshProUGUI _waveCounter;
    private StringBuilder _stringBuilder;
    private string _waveLabel;

    private void Awake()
    {
        _waveCounter = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _waveLabel = "Wave: ";
        _stringBuilder = new StringBuilder();
        FindObjectOfType<EnemySpawnerSubject>().AddObserver(this);
    }

    public void OnNotify(int waveNumber)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(_waveLabel);
        _stringBuilder.Append(waveNumber);
        _waveCounter.text = _stringBuilder.ToString();
    }
}
