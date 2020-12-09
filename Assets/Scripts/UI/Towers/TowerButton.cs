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
    private Text _price;


    public void Initialize(Sprite newSprite, int newPrice)
    {
        _buttonImage.sprite = newSprite;
        _buttonImage.SetNativeSize();
        _buttonImage.rectTransform.sizeDelta *= 2.4f;
        _price.text = newPrice.ToString();
    }

    public void Initialize(int newPrice)
    {
        _price.text = newPrice.ToString();
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