using UnityEngine;
using UnityEngine.UI;

/*
 * Class used in sell tower buttons
 */
public class SellButton : MonoBehaviour
{
    [Header("UI Elements")]
    //The actual button component
    [SerializeField]
    private Button _sellButton;

    //Transform component of button
    [SerializeField]
    private RectTransform _rectTransform;

    //Text component that handles price
    [SerializeField]
    private Text _priceText;



    //Method used to change button behavior
    //
    //Parameters => newPosition, new position for Button according to mouse position
    //              newUsedTower, which tower will be concerned by this behavior?
    //              newPrice, what is the new price for this tower?
    public void Activate(Vector2 newPosition, Tower newUsedTower, int newPrice)
    {
        _rectTransform.localPosition = newPosition;
        _priceText.text = newPrice.ToString();

        _sellButton.onClick.RemoveAllListeners();
        _sellButton.onClick.AddListener(() => newUsedTower.ResellTower());
    }
}