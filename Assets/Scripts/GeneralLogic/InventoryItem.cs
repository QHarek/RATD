using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : ShopSubject, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private ItemDataSO _itemData;
    [SerializeField] private GameObject _descriptionWindow;
    
    private PlayerWallet _playerWallet;
    private TowerInventory _towerInventory;
    private TMPro.TextMeshProUGUI _inventoryItemNameLabelText;

    public ItemDataSO ItemData { get => _itemData; internal set => _itemData = value; }

    private void Awake()
    {
        _icon.sprite = _itemData.Icon;
    }

    private void Start()
    {
        _playerWallet = FindObjectOfType<PlayerWallet>();
        _towerInventory = FindObjectOfType<TowerInventory>();
        _inventoryItemNameLabelText = GameObject.Find("InventoryItemNameLabel").GetComponentInChildren<TMPro.TextMeshProUGUI>();

        AddObserver(_towerInventory);
        AddObserver(_playerWallet);
    }

    private void ShowDescription()
    {
        GameObject newDescription = Instantiate(_descriptionWindow, GameObject.Find("DescriptionCanvas").transform);
        newDescription.GetComponent<DescriptionWindowBehavior>().NameLabel.text = _itemData.name;
        newDescription.GetComponent<DescriptionWindowBehavior>().LevelLabel.text = " ";
        newDescription.GetComponent<DescriptionWindowBehavior>().SalePriceLabel.text = (_itemData.Price / 2).ToString();
        newDescription.GetComponent<DescriptionWindowBehavior>().DescriptionLabel.text = _itemData.Description;
    }

    public void SellItem()
    {
        _towerInventory.Items.Remove(this);
        _inventoryItemNameLabelText.text = "";
        NotifyObservers(ShopAction.SellItem, ItemData);
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ShowDescription();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _inventoryItemNameLabelText.text = _itemData.Name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inventoryItemNameLabelText.text = "";
    }
}
