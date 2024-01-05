using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreItem : ShopSubject, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private ItemDataSO _itemData;
    [SerializeField] private GameObject _descriptionWindow;

    private PlayerWallet _playerWallet;
    private TowerInventory _towerInventory;
    private TMPro.TextMeshProUGUI _storeItemNameLabelText;

    private void Awake()
    {
        _icon.sprite = _itemData.Icon;
    }

    private void Start()
    {
        _playerWallet = FindObjectOfType<PlayerWallet>();
        _towerInventory = FindObjectOfType<TowerInventory>();
        _storeItemNameLabelText = GameObject.Find("StoreItemNameLabel").GetComponentInChildren<TMPro.TextMeshProUGUI>();

        AddObserver(_towerInventory);
        AddObserver(_playerWallet);
    }

    private void BuyItem()
    {
        if(_playerWallet.Gold >= _itemData.Price && !_towerInventory.IsFull)
        {
            NotifyObservers(ShopAction.BuyItem, _itemData);
        }
    }

    private void ShowDescription()
    {
        GameObject newDescription = Instantiate(_descriptionWindow, GameObject.Find("DescriptionCanvas").transform);
        newDescription.GetComponent<DescriptionWindowBehavior>().NameLabel.text = _itemData.name;
        newDescription.GetComponent<DescriptionWindowBehavior>().LevelLabel.text = " ";
        newDescription.GetComponent<DescriptionWindowBehavior>().SalePriceLabel.text = (_itemData.Price / 2).ToString();
        newDescription.GetComponent<DescriptionWindowBehavior>().DescriptionLabel.text = _itemData.Description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            BuyItem();
        if (eventData.button == PointerEventData.InputButton.Right)
            ShowDescription();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _storeItemNameLabelText.text = _itemData.Name + " (" + _itemData.Price + " coins)";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _storeItemNameLabelText.text = "";
    }
}
