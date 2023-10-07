using UnityEngine;

public class PlayerWallet : MonoBehaviour, IEnemyObserver, IEnemySpawnerObserver
{
    [SerializeField] private TMPro.TextMeshProUGUI _goldText;
    [SerializeField] private TMPro.TextMeshProUGUI _dicesText;
    [SerializeField] private BountySettingsSO _bountySettingsSO;

    private int _gold;
    private int _dices;

    private void Start()
    {
        FindObjectOfType<EnemySpawnerSubject>().AddObserver(this);
    }

    void IEnemyObserver.OnNotify(EnemyAction enemyAction)
    {
        if (enemyAction == EnemyAction.Die)
        {
            _gold += _bountySettingsSO.EnemyBounty;
            _goldText.text = _gold.ToString();
        }
    }

    void IEnemySpawnerObserver.OnNotify(int waveNumber)
    {
        if(waveNumber > 1)
        {
            _gold += _bountySettingsSO.BasicWaveGoldBounty + _bountySettingsSO.WaveGoldBountyMultiplier * waveNumber;
            _dices += _bountySettingsSO.BasicWaveDiceBounty + waveNumber / _bountySettingsSO.WavesToDiceBountyIncreace;
            _goldText.text = _gold.ToString();
            _dicesText.text = _dices.ToString();
        }
    }
}
