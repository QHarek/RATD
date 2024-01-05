using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionWindowBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform _textTransform;
    [SerializeField] private TMPro.TextMeshProUGUI _nameLabel;
    [SerializeField] private TMPro.TextMeshProUGUI _levelLabel;
    [SerializeField] private TMPro.TextMeshProUGUI _salePriceLabel;
    [SerializeField] private TMPro.TextMeshProUGUI _descriptionLabel;
    [SerializeField] private GameObject[] _hideForAbilities;

    private Vector2 _dragOffset;
    private Vector2 _canvasSize;
    private Vector2 _windowSize;

    public TMPro.TextMeshProUGUI NameLabel => _nameLabel;
    public TMPro.TextMeshProUGUI LevelLabel => _levelLabel;
    public TMPro.TextMeshProUGUI SalePriceLabel => _salePriceLabel;
    public TMPro.TextMeshProUGUI DescriptionLabel => _descriptionLabel;

    private void Awake()
    {
        transform.position = new Vector2(325, 640);
    }

    private void Start()
    {
        _canvasSize = (GameObject.Find("DescriptionCanvas").transform as RectTransform).sizeDelta;
        _windowSize = GetComponent<RectTransform>().sizeDelta;
        _textTransform.position = new Vector3(_textTransform.position.x, _textTransform.rect.height / 2, _textTransform.position.z);
    }

    public void HideElementsForAbilities()
    {
        foreach (var element in _hideForAbilities)
        {
            element.SetActive(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragOffset = (Vector2) transform.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector2(
            Mathf.Clamp(eventData.position.x + _dragOffset.x, _windowSize.x / 2, _canvasSize.x - _windowSize.x / 2),
            Mathf.Clamp(eventData.position.y + _dragOffset.y, _windowSize.y / 2, _canvasSize.y - _windowSize.y / 2)
        );
    }

    public void CloseDescription()
    {
        Destroy(gameObject);
    }
}
