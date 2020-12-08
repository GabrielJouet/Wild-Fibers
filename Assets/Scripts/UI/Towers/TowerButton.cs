using UnityEngine;
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
    public Button Button { get => _buttonComponent; }

    [SerializeField]
    private Image _buttonSprite;

    //Text component that handles price
    [SerializeField]
    private Text _price;


    public void Initialize(Sprite newSprite, int newPrice)
    {
        _buttonSprite.sprite = newSprite;
        _buttonSprite.SetNativeSize();
        _buttonSprite.rectTransform.sizeDelta *= 2.4f;
        _price.text = newPrice.ToString();
    }

    public void Initialize(int newPrice)
    {
        _price.text = newPrice.ToString();
    }
}