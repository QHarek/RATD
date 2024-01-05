using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityPicker : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _defaultIconSprite;
    [SerializeField] private AbilityPoolSO _allAbilities;
    [SerializeField] private EconomySettingsSO _economySettingsSO;
    [SerializeField] private GameObject[] _hideForTranscendent;
    [SerializeField] private GameObject _descriptionWindow;

    private static List<AbilityDataSO> _currentlyAvailableAbilities;

    private TMPro.TextMeshProUGUI _abilityNameLabelText;
    private TowerAbilitiesHolder _towerAbilitiesHodler;
    private AbilityDataSO _ability;
    private PlayerWallet _playerWallet;

    private void Awake()
    {
        _currentlyAvailableAbilities = new List<AbilityDataSO>();
    }

    private void Start()
    {
        _towerAbilitiesHodler = FindObjectOfType<TowerAbilitiesHolder>();
        _playerWallet = FindObjectOfType<PlayerWallet>();
        _abilityNameLabelText = GameObject.Find("AbilityNameLabel").GetComponentInChildren<TMPro.TextMeshProUGUI>();

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
        if (_currentlyAvailableAbilities.Count != 0 && _playerWallet.Gold >= _economySettingsSO.RollAbilityCostGold)
        {
            int abilityIndex = Random.Range(0, _currentlyAvailableAbilities.Count - 1);
            _ability = _currentlyAvailableAbilities[abilityIndex];
            _towerAbilitiesHodler.AddAbility(_ability);
            _icon.sprite = _ability.IconSprite;
            _currentlyAvailableAbilities.Remove(_ability);
            _ability.RandomizeLevel();

            _playerWallet.Pay(_economySettingsSO.RollAbilityCostGold, 0);

            _abilityNameLabelText.text = _ability.Name + " (Level " + _ability.Level + ")";
        }
    }

    private void ShowDescription()
    {
        GameObject newDescription = Instantiate(_descriptionWindow, GameObject.Find("DescriptionCanvas").transform);
        newDescription.GetComponent<DescriptionWindowBehavior>().NameLabel.text = _ability.Name;

        if (_ability.Level == _ability.MaxLevel && _ability.Name != "Tower of Greed")
        {
            newDescription.GetComponent<DescriptionWindowBehavior>().LevelLabel.text = "Transcendent";
        }
        else
        {
            newDescription.GetComponent<DescriptionWindowBehavior>().LevelLabel.text = "Level: " + _ability.Level.ToString();
        }

        newDescription.GetComponent<DescriptionWindowBehavior>().HideElementsForAbilities();
        newDescription.GetComponent<DescriptionWindowBehavior>().DescriptionLabel.text = _ability.Description;
    }

    private void HideElementsForTranscendent()
    {
        foreach (var element in _hideForTranscendent)
        {
            element.SetActive(false);
        }
    }

    public void RemoveAbility()
    {
        if (_ability)
            if (_ability.Level < _ability.MaxLevel)
                if (_playerWallet.Dices >= _economySettingsSO.RemoveAbilityCostDices)
                {
                    _playerWallet.Pay(0, _economySettingsSO.RemoveAbilityCostDices);

                    _currentlyAvailableAbilities.Add(_ability);
                    _towerAbilitiesHodler.RemoveAbility(_ability);
                    _icon.sprite = _defaultIconSprite;
                    _ability = null;
                    _abilityNameLabelText.text = "";
                }
    }

    public void UpgradeAbility()
    {
        if (_ability)
            if (_ability.Level < _ability.MaxLevel)
                if (_ability.Level == _ability.MaxLevel - 1 && !_ability.Name.Contains("Tower of Greed")) //preTranscendent level check
                {
                    if (_playerWallet.Dices >= _economySettingsSO.TranscendentUpgradeCostDices)
                    {
                        _playerWallet.Pay(0, _economySettingsSO.TranscendentUpgradeCostDices);

                        _ability.Updrade(_ability.Level + 1);
                        _abilityNameLabelText.text = _ability.Name + " (Transcendent)";
                        HideElementsForTranscendent();
                    }
                }
                else
                {
                    if (_playerWallet.Gold >= _economySettingsSO.UpgradeAbilityCostGold[_ability.Level])
                    {
                        _playerWallet.Pay(_economySettingsSO.UpgradeAbilityCostGold[_ability.Level], 0);

                        _ability.Updrade(_ability.Level + 1);
                        _abilityNameLabelText.text = _ability.Name + " (Level " + _ability.Level + ")";
                    }
                }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !_ability)
            RollAbility();
        if (eventData.button == PointerEventData.InputButton.Right && _ability)
            ShowDescription();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_ability)
        {
            if(_ability.Level == _ability.MaxLevel)
                _abilityNameLabelText.text = _ability.Name + " (Transcendent)";
            else
                _abilityNameLabelText.text = _ability.Name + " (Level " + _ability.Level + ")";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _abilityNameLabelText.text = "";
    }
}
