using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityPicker : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _defaultIconSprite;
    [SerializeField] private AbilityPoolSO _allAbilities;
    
    private static List<AbilityDataSO> _currentlyAvailableAbilities;

    private GameObject _abilityNameLabel;
    private TMPro.TextMeshProUGUI _abilityNameLabelText;
    private TowerAbilitiesHolder _towerAbilitiesHodler;
    private AbilityDataSO _ability;

    private void Awake()
    {
        _currentlyAvailableAbilities = new List<AbilityDataSO>();
        _towerAbilitiesHodler = FindObjectOfType<TowerAbilitiesHolder>();
    }

    private void Start()
    {
        _abilityNameLabel = GameObject.Find("AbilityNameLabel");
        _abilityNameLabelText = _abilityNameLabel.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        FillCurrentlyAvailableAbilitiesPool();
    }

    private void FillCurrentlyAvailableAbilitiesPool()
    {
        for (int i = 0; i < _allAbilities.AbilitiesCount(); i++)
        {
            if(!_currentlyAvailableAbilities.Contains(_allAbilities.GetAbilityAtIndex(i)))
                _currentlyAvailableAbilities.Add(_allAbilities.GetAbilityAtIndex(i));
        }
    }

    private void RollAbility()
    {
        if (_currentlyAvailableAbilities.Count != 0)
        {
            int abilityIndex = Random.Range(0, _currentlyAvailableAbilities.Count - 1);
            _ability = _currentlyAvailableAbilities[abilityIndex];
            _towerAbilitiesHodler.AddAbility(_ability);
            _icon.sprite = _ability.IconSprite;
            _currentlyAvailableAbilities.Remove(_ability);
            _ability.RandomizeLevel();

            _abilityNameLabelText.text = _ability.Name + " (Level " + _ability.Level + ")";
        }
    }

    public void RemoveAbility()
    {
        _currentlyAvailableAbilities.Add(_ability);
        _towerAbilitiesHodler.RemoveAbility(_ability);
        _icon.sprite = _defaultIconSprite;
        _ability = null;
        _abilityNameLabelText.text = "";
    }

    public void UpgradeAbility()
    {
        if (_ability)
        {
            _ability.Updrade();
            _abilityNameLabelText.text = _ability.Name + " (Level " + _ability.Level + ")";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !_ability)
            RollAbility();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_ability)
        {
            _abilityNameLabelText.text = _ability.Name + " (Level " + _ability.Level + ")";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _abilityNameLabelText.text = "";
    }
}
