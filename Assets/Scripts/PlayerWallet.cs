using UnityEngine;

public class PlayerWallet : MonoBehaviour, IEnemyObserver, IEnemySpawnerObserver
{
    [SerializeField] private TMPro.TextMeshProUGUI _goldText;
    [SerializeField] private TMPro.TextMeshProUGUI _dicesText;
    [SerializeField] private EconomySettingsSO _economySettingsSO;

    private int _gold;
    private int _dices;

    public int Gold => _gold;
    public int Dices => _dices;
    public EconomySettingsSO EconomySettingsSO => _economySettingsSO;

    private void Start()
    {
        FindObjectOfType<EnemySpawnerSubject>().AddObserver(this);
        _gold = _economySettingsSO.BasicGold;
        _dices = _economySettingsSO.BasicDices;

        _goldText.text = _gold.ToString();
        _dicesText.text = _dices.ToString();
    }

    public void Pay(int gold, int dices)
    {
        _gold -= gold;
        _dices -= dices;

        _goldText.text = _gold.ToString();
        _dicesText.text = _dices.ToString();
    }

    public void Receive(int gold, int dices)
    {
        _gold += gold;
        _dices += dices;

        _goldText.text = _gold.ToString();
        _dicesText.text = _dices.ToString();
    }

    void IEnemyObserver.OnNotify(EnemyAction enemyAction)
    {
        if (enemyAction == EnemyAction.Died)
        {
            _gold += _economySettingsSO.EnemyBounty;
            _goldText.text = _gold.ToString();
        }

        if (enemyAction == EnemyAction.BossDied)
        {
            _gold += _economySettingsSO.BossBounty;
            _goldText.text = _gold.ToString();
        }
    }

    void IEnemySpawnerObserver.OnNotify(int waveNumber)
    {
        if(waveNumber > 1)
        {
            _gold += _economySettingsSO.BasicWaveGoldBounty + _economySettingsSO.WaveGoldBountyMultiplier * waveNumber;
            _dices += _economySettingsSO.BasicWaveDiceBounty + waveNumber / _economySettingsSO.WavesToDiceBountyIncreace;
            _goldText.text = _gold.ToString();
            _dicesText.text = _dices.ToString();
        }
    }
}
