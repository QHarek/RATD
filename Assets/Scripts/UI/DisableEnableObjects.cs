using UnityEngine;

public class DisableEnableObjects : MonoBehaviour
{
    [SerializeField] private GameObject _shopUI;

    public void ToggleShopUI()
    {
        _shopUI.SetActive(!_shopUI.activeSelf);
    }
}
