using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerAbilityPicker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private AbilityPoolSO _allAbilities;
    
    private static List<AbilityDataSO> _currentlyAvailableAbilities;

    private TowerAbilitiesHodler _towerAbilitiesHodler;
    private AbilityDataSO _ability;

    private void Awake()
    {
        _currentlyAvailableAbilities = new List<AbilityDataSO>();
        _towerAbilitiesHodler = FindObjectOfType<TowerAbilitiesHodler>();
    }

    private void Start()
    {
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

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !_ability)
            RollAbility();
    }
}
