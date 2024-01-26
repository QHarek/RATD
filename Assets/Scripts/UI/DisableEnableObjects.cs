using UnityEngine;

public class DisableEnableObjects : MonoBehaviour
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _stats;

    public void ToggleShopUI()
    {
        _shop.SetActive(!_shop.activeSelf);
    }

    public void ToggleStatsUI()
    {
        _stats.SetActive(!_stats.activeSelf);
        if(_stats.activeSelf)
        FindObjectOfType<StatsFiller>().UpdateFields();
    }
}
