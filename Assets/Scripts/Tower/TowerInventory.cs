using System.Collections.Generic;
using UnityEngine;

public class TowerInventory : MonoBehaviour, IShopObserver
{
    [SerializeField] private int _slotsLimit;
    [SerializeField] private GameObject _inventoryContentUI;
    [SerializeField] private GameObject _inventoryItemPrefab;
    [SerializeField] private TowerStats _towerStats;

    [SerializeField]private List<InventoryItem> _items;
    private int _overallDamageBonus;
    private int _overallAttackSpeedBonus;
    private bool _isFull;

    public List<InventoryItem> Items => _items;
    public int OverallDamageBonus => _overallDamageBonus;
    public int OverallAttackSpeedBonus => _overallAttackSpeedBonus;
    public bool IsFull => _isFull;

    private void Awake()
    {
        _isFull = false;
        _items = new List<InventoryItem>();
    }

    private void CalculateBonuses()
    {
        _overallAttackSpeedBonus = 0;
        _overallDamageBonus = 0;

        foreach (var inventoryItem in _items)
        {
            _overallAttackSpeedBonus += inventoryItem.ItemData.AttackSpeedBonus;
            _overallDamageBonus += inventoryItem.ItemData.DamageBonus;
        }

        _towerStats.UpdateDamageAndAttackSpeed();
    }

    public void OnNotify(ShopAction shopAction, ItemDataSO itemData)
    {
        if (shopAction == ShopAction.BuyItem)
        {
            _inventoryItemPrefab.GetComponent<InventoryItem>().ItemData = itemData;
            GameObject newItem = Instantiate(_inventoryItemPrefab, _inventoryContentUI.transform);
            _inventoryItemPrefab.GetComponent<InventoryItem>().ItemData = null;
            _items.Add(newItem.GetComponent<InventoryItem>());
            if (_items.Count == _slotsLimit)
                _isFull = true;
            CalculateBonuses();
        }
        else if (shopAction == ShopAction.SellItem)
        {
            _isFull = false;
            CalculateBonuses();
        }
    }
}
