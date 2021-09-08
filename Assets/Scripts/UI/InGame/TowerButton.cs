using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class used to handle button sell and buy.
/// </summary>
public class TowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Elements")]

    /// <summary>
    /// Button component.
    /// </summary>
    [SerializeField]
    private Button _buttonComponent;

    /// <summary>
    /// Image component.
    /// </summary>
    [SerializeField]
    private Image _buttonImage;

    /// <summary>
    /// Background image component.
    /// </summary>
    [SerializeField]
    private Image _backgroundImage;


    /// <summary>
    /// Text component of the price.
    /// </summary>
    [SerializeField]
    private Text _priceText;

    /// <summary>
    /// Actual price displayed.
    /// </summary>
    private int _price;


    /// <summary>
    /// Locked sprite.
    /// </summary>
    [SerializeField]
    private Sprite _lockedSprite;

    /// <summary>
    /// Description of the tower.
    /// </summary>
    private Text _descriptionText;

    /// <summary>
    /// Description game object of the tower.
    /// </summary>
    private GameObject _descriptionObject;

    /// <summary>
    /// Ressource controller component.
    /// </summary>
    private RessourceController _resourceController;

    /// <summary>
    /// Does the button is locked?
    /// </summary>
    private bool _locked = false;

    /// <summary>
    /// Does the button is locked?
    /// </summary>
    private string _description;


    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newSprite">The sprite to display</param>
    /// <param name="newPrice">The new price of the tower</param>
    /// <param name="newController">The ressource controller to use</param>
    /// <param name="newDescription">The new description of this button</param>
    public void Initialize(Sprite newSprite, int newPrice, RessourceController newController, string newDescription)
    {
        _resourceController = newController;

        _description = newDescription;
        _buttonImage.sprite = newSprite;
        _buttonImage.SetNativeSize();
        _buttonImage.rectTransform.sizeDelta *= 2.4f;

        _price = newPrice;
        _priceText.text = _price.ToString();
    }


    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <remarks>Only used when reselling a tower</remarks>
    /// <param name="newPrice">The new price of this button</param>
    public void Initialize(int newPrice)
    {
        _priceText.text = newPrice.ToString();
    }



    /// <summary>
    /// Update method is called each frame.
    /// </summary>
    private void Update()
    {
        if(_resourceController != null && !_locked)
            UpdateState(_price <= _resourceController.GoldCount);
    }


    /// <summary>
    /// Method called when we want to change the behavior of a button.
    /// </summary>
    /// <param name="buttonCallBack">The call back used when pressing the button</param>
    public void ChangeBehavior(UnityAction buttonCallBack)
    {
        _buttonComponent.onClick.RemoveAllListeners();
        _buttonComponent.onClick.AddListener(buttonCallBack);
        _buttonComponent.onClick.AddListener(() => _descriptionText?.rectTransform.parent.gameObject.SetActive(false));
    }


    /// <summary>
    /// Method called when we want to change the target description object.
    /// </summary>
    /// <param name="descriptionObject">The description game object related</param>
    public void SetTargetDescription(GameObject descriptionObject)
    {
        _descriptionObject = descriptionObject;
        _descriptionText = descriptionObject.GetComponentInChildren<Text>();
    }


    /// <summary>
    /// Method called to lock a specific button.
    /// </summary>
    public void Lock()
    {
        _locked = true;
        UpdateState(false);
        _buttonImage.sprite = _lockedSprite;
        _buttonImage.SetNativeSize();
        _buttonImage.rectTransform.sizeDelta *= 2.4f;
        _priceText.enabled = false;
    }


    /// <summary>
    /// Method called to update a button state.
    /// </summary>
    /// <param name="activated">Does the button is activated or not?</param>
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
        if (!_locked)
        {
            _descriptionObject.SetActive(true);
            _descriptionText.text = _description;
        }
    }


    /// <summary>
    /// Method used to desactivate the highlight for the level button if not locked.
    /// </summary>
    /// <param name="eventData">The pointer event</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_locked)
            _descriptionObject.SetActive(false);
    }
}