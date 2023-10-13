using UnityEngine;

public abstract class AbilityDataSO : ScriptableObject
{
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private string _name;
    [SerializeField] private string _tooltip;

    [SerializeField] protected int _level;
    [SerializeField] protected int _maxLevel;
    [SerializeField] protected int _maxRandomLevel;


    protected internal AbilityType _abilityType;

    public Sprite IconSprite => _iconSprite;
    public string Name => _name;
    public string Tooltip => _tooltip;
    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public int MaxRandomLevel => _maxRandomLevel;

    public AbilityType AbilityType => _abilityType;


    internal abstract void Updrade();

    internal void RandomizeLevel()
    {
        _level = Random.Range(0, _maxRandomLevel);
    }
}
