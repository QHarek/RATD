using UnityEngine;
using TMPro;
using System.Text;

public sealed class Timer : MonoBehaviour
{
    private const string TIMERLABEL = "Time: ";

    private EnemySpawner _enemySpawner;
    private TextMeshProUGUI _timer;
    private StringBuilder _stringBuilder;
    private float _lastTimeUpdate;
    private float _gameTime;
    private bool _gameStarted;

    private void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        _timer = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _gameStarted = false;
        _gameTime = 5;
        _lastTimeUpdate = Time.time;
        _stringBuilder = new StringBuilder();
        _stringBuilder.Append(TIMERLABEL);
        _stringBuilder.Append(FormatTime(Mathf.RoundToInt(_enemySpawner.StartGameDelay)));
        _timer.text = _stringBuilder.ToString();
        _stringBuilder.Clear();
    }

    private void FixedUpdate()
    {
        if (Time.time > _enemySpawner.StartGameDelay)
        {
            if(!_gameStarted)
            {
                _gameTime = Time.time - _enemySpawner.StartGameDelay;
                _gameStarted = true;
            }
            _gameTime += Time.fixedDeltaTime;
            if(Time.time > _lastTimeUpdate + 1)
                UpdateTimer();
        }
        else
        {
            _gameTime -= Time.fixedDeltaTime;
            if (Time.time > _lastTimeUpdate + 1) 
                UpdateTimer();
        }

    }

    private void UpdateTimer()
    {
        _stringBuilder.Append(TIMERLABEL);
        _stringBuilder.Append(FormatTime(Mathf.RoundToInt(_gameTime)));
        _timer.text = _stringBuilder.ToString();
        _stringBuilder.Clear();
        _lastTimeUpdate = (int)Time.time;
    }

    private string FormatTime(int timeInSeconds)
    {
        StringBuilder sb = new StringBuilder();
        if (timeInSeconds / 3600 < 10)
        {
            sb.Append(0);
            sb.Append(timeInSeconds / 3600);
        }
        else
        {
            sb.Append(timeInSeconds / 3600);
        }
        sb.Append(':');
        if (timeInSeconds / 60 - timeInSeconds / 3600 * 60 < 10)
        {
            sb.Append(0);
            sb.Append(timeInSeconds / 60 - timeInSeconds / 3600 * 60);
        }
        else
        {
            sb.Append(timeInSeconds / 60 - timeInSeconds / 3600 * 60);
        }
        sb.Append(':');
        if (timeInSeconds % 60 < 10)
        {
            sb.Append(0);
            sb.Append(timeInSeconds % 60);
        }
        else
        {
            sb.Append(timeInSeconds % 60);
        }
        return sb.ToString();
    }
}
