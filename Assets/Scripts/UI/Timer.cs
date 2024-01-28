using UnityEngine;
using TMPro;
using System.Text;

public sealed class Timer : MonoBehaviour
{
    private const string TIMERLABEL = "Time: ";
    private const float STARTGAMEDELAY = 5;

    private static float _customTime;

    private TextMeshProUGUI _timer;
    private StringBuilder _stringBuilder;
    private float _lastTimeUpdate;
    private float _timerValue;
    private bool _gameStarted;

    public static float CustomTime => _customTime;

    public float TimerValue => _timerValue;
    public bool GameStarted => _gameStarted;

    private void Awake()
    {
        _customTime = 0;
        _timer = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _customTime += Time.deltaTime;
    }

    private void Start()
    {
        _gameStarted = false;
        _timerValue = STARTGAMEDELAY;
        _lastTimeUpdate = _customTime;
        _stringBuilder = new StringBuilder();
        _stringBuilder.Append(TIMERLABEL);
        _stringBuilder.Append(FormatTime(Mathf.RoundToInt(_timerValue)));
        _timer.text = _stringBuilder.ToString();
        _stringBuilder.Clear();
    }

    private void FixedUpdate()
    {
        if (_customTime > STARTGAMEDELAY)
        {
            if(!_gameStarted)
            {
                _timerValue = _customTime - STARTGAMEDELAY;
                _gameStarted = true;
            }
            _timerValue += Time.fixedDeltaTime;
            if(_customTime - _lastTimeUpdate >= 1)
                UpdateTimer();
        }
        else
        {
            _timerValue -= Time.fixedDeltaTime;
            if (_customTime - _lastTimeUpdate >= 1) 
                UpdateTimer();
        }

    }

    private void UpdateTimer()
    {
        _stringBuilder.Append(TIMERLABEL);
        _stringBuilder.Append(FormatTime(Mathf.RoundToInt(_timerValue)));
        _timer.text = _stringBuilder.ToString();
        _stringBuilder.Clear();
        _lastTimeUpdate = (int) _customTime;
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
