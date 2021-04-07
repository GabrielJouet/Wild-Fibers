using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Class used in sell tower buttons
 */
public class TowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Elements")]
    //The actual button component
    [SerializeField]
    private Button _buttonComponent;

    [SerializeField]
    private Image _buttonImage;
    [SerializeField]
    private Image _backgroundImage;

    //Text component that handles price
    [SerializeField]
    private Text _priceText;
    private int _price;

    [SerializeField]
    private Sprite _lockedSprite;

    [SerializeField]
    private Text _descriptionText;

    private RessourceController _resourceController;

    private bool _locked = false;


    public void Initialize(Sprite newSprite, int newPrice, RessourceController newController, string newDescription)
    {
        _resourceController = newController;

        _descriptionText.text = newDescription;
        _buttonImage.sprite = newSprite;
        _buttonImage.SetNativeSize();
        _buttonImage.rectTransform.sizeDelta *= 2.4f;

        _price = newPrice;
        _priceText.text = _price.ToString();
    }


    public void Initialize(int newPrice)
    {
        _priceText.text = newPrice.ToString();
    }


    private void Update()
    {
        if(_resourceController != null && !_locked)
            UpdateState(_price <= _resourceController.GoldCount);
    }


    public void ChangeBehavior(UnityAction buttonCallBack)
    {
        _buttonComponent.onClick.RemoveAllListeners();
        _buttonComponent.onClick.AddListener(buttonCallBack);
        _buttonComponent.onClick.AddListener(() => _descriptionText?.rectTransform.parent.gameObject.SetActive(false));
    }


    public void Lock()
    {
        _locked = true;
        UpdateState(false);
        _buttonImage.sprite = _lockedSprite;
        _buttonImage.SetNativeSize();
        _buttonImage.rectTransform.sizeDelta *= 2.4f;
        _priceText.enabled = false;
    }


    public void UpdateState(bool activated)
    {
        _buttonComponent.enabled = activated;

        Color newColor = activated ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1);
        _buttonImage.color = newColor;
        _backgroundImage.color = newColor;
    }



    /// <summary>
    /// Method used to highlight the level button if not locked.
    /// </summary>
    /// <param name="eventData">The pointer event</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_locked && _descriptionText != null)
            _descriptionText.rectTransform.parent.gameObject.SetActive(true);
    }


    /// <summary>
    /// Method used to desactivate the highlight for the level button if not locked.
    /// </summary>
    /// <param name="eventData">The pointer event</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_locked && _descriptionText != null)
            _descriptionText.rectTransform.parent.gameObject.SetActive(false);
    }
}