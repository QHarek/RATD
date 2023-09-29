using UnityEngine;

public abstract class AbilityDataSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _tooltip;
    [SerializeField] private Sprite _iconSprite;

    protected internal AbilityType _abilityType;

    public AbilityType AbilityType => _abilityType;
    public string Name => _name;
    public string Tooltip => _tooltip;

    public Sprite IconSprite => _iconSprite;
}
