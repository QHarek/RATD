using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "New Item Data", order = 51)]
public class ItemDataSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _damageBonus;
    [SerializeField] private int _attackSpeedBonus;
    [SerializeField] private int _price;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;

    public string Name => _name;
    public int DamageBonus => _damageBonus;
    public int AttackSpeedBonus => _attackSpeedBonus;
    public int Price => _price;
    public string Description => _description;
    public Sprite Icon => _icon;
}
