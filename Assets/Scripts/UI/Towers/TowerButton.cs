using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 * Class used in sell tower buttons
 */
public class TowerButton : MonoBehaviour
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

    private RessourceController _resourceController;



    public void Initialize(Sprite newSprite, int newPrice, RessourceController newController)
    {
        _resourceController = newController;
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
        if(_resourceController != null)
            UpdateState(_price <= _resourceController.GoldCount);
    }


    public void ChangeBehavior(UnityAction buttonCallBack)
    {
        _buttonComponent.onClick.RemoveAllListeners();
        _buttonComponent.onClick.AddListener(buttonCallBack);
    }

    public void UpdateState(bool activated)
    {
        _buttonComponent.enabled = activated;

        Color newColor = activated ? Color.white : Color.gray;
        _buttonImage.color = newColor;
        _backgroundImage.color = newColor;
    }
}