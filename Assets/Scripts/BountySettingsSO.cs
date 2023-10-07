using UnityEngine;

[CreateAssetMenu(fileName = "New BountySettings", menuName = "New Bounty Settings", order = 51)]
public class BountySettingsSO : ScriptableObject
{
    [SerializeField] int _enemyBounty;
    [SerializeField] int _bossBounty;
    [SerializeField] int _basicWaveGoldBounty;
    [SerializeField] int _waveGoldBountyMultiplier;
    [SerializeField] int _basicWaveDiceBounty;
    [SerializeField] int _wavesToDiceBountyIncreace;

    public int EnemyBounty=> _enemyBounty;
    public int BossBounty => _bossBounty;
    public int BasicWaveGoldBounty => _basicWaveGoldBounty;
    public int WaveGoldBountyMultiplier => _waveGoldBountyMultiplier;
    public int BasicWaveDiceBounty => _basicWaveDiceBounty;
    public int WavesToDiceBountyIncreace => _wavesToDiceBountyIncreace;
}
