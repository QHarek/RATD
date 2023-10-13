using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BountySettings", menuName = "New Bounty Settings", order = 51)]
public class EconomySettingsSO : ScriptableObject
{
    [SerializeField] private int _enemyBounty;
    [SerializeField] private int _bossBounty;
    [SerializeField] private int _basicWaveGoldBounty;
    [SerializeField] private int _waveGoldBountyMultiplier;
    [SerializeField] private int _basicWaveDiceBounty;
    [SerializeField] private int _wavesToDiceBountyIncreace;
    
    [SerializeField] private int _basicGold;
    [SerializeField] private int _basicDices;
    [SerializeField] private int _rollAbilityCostGold;
    [SerializeField] private int _removeAbilityCostDices;
    [SerializeField] private int _transcendentUpgradeCostDices;
    [SerializeField] private List<int> _upgradeAbilityCostGold;

    public int EnemyBounty=> _enemyBounty;
    public int BossBounty => _bossBounty;
    public int BasicWaveGoldBounty => _basicWaveGoldBounty;
    public int WaveGoldBountyMultiplier => _waveGoldBountyMultiplier;
    public int BasicWaveDiceBounty => _basicWaveDiceBounty;
    public int WavesToDiceBountyIncreace => _wavesToDiceBountyIncreace;

    public int BasicGold => _basicGold;
    public int BasicDices => _basicDices;
    public int RollAbilityCostGold => _rollAbilityCostGold;
    public int RemoveAbilityCostDices => _removeAbilityCostDices;
    public int TranscendentUpgradeCostDices => _transcendentUpgradeCostDices;
    public List<int> UpgradeAbilityCostGold => _upgradeAbilityCostGold;
}
