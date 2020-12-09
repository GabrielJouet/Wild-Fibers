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

    public void Activate(UnityAction buttonCallBack)
    {
        gameObject.SetActive(true);

        _buttonComponent.onClick.RemoveAllListeners();
        _buttonComponent.onClick.AddListener(buttonCallBack);

        _buttonImage.color = Color.white;
        _backgroundImage.color = Color.white;

        _buttonComponent.enabled = true;
    }

    public void Desactivate()
    {
        _buttonImage.color = Color.gray;
        _backgroundImage.color = Color.gray;

        _buttonComponent.enabled = false;
    }
}