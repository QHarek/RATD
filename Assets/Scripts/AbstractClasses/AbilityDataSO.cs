using UnityEngine;

public abstract class AbilityDataSO : ScriptableObject
{
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private string _name;
    [SerializeField] private string _tooltip;
    [SerializeField] private int _level;
    [SerializeField] private int _maxLevel;


    protected internal AbilityType _abilityType;

    public Sprite IconSprite => _iconSprite;
    public string Name => _name;
    public string Tooltip => _tooltip;
    public int Level => _level;

    public AbilityType AbilityType => _abilityType;

    internal abstract void Updrade();

    internal void RandomizeLevel()
    {
        _level = Random.Range(1, _maxLevel);
    }
}
