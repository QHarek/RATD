using UnityEngine;

public abstract class AbilityDataSO : ScriptableObject
{
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] protected int _level;
    [SerializeField] protected int _maxLevel;
    [SerializeField] protected int _maxRandomLevel;


    protected internal AbilityType _abilityType;

    public Sprite IconSprite => _iconSprite;
    public string Name => _name;
    public string Description => _description;
    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public int MaxRandomLevel => _maxRandomLevel;

    public AbilityType AbilityType => _abilityType;

    internal abstract void InitializeAbility();

    internal abstract void Updrade(int level);
}
