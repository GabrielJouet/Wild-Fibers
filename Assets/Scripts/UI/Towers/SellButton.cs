using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used for tower sell button.
/// </summary>
[RequireComponent(typeof(Button), typeof(RectTransform))]
public class SellButton : MonoBehaviour
{
    /// <summary>
    /// Text component.
    /// </summary>
    [SerializeField]
    private Text _priceText;


    /// <summary>
    /// Button component.
    /// </summary>
    [SerializeField]
    private Button _sellButton;

    /// <summary>
    /// Transform component.
    /// </summary>
    [SerializeField]
    private RectTransform _rectTransform;


    /// <summary>
    /// Method used to activate the button.
    /// </summary>
    /// <param name="newPosition">The new position of the button</param>
    /// <param name="newUsedTower">The tower related</param>
    /// <param name="newPrice">The new price to display</param>
    public void Activate(Vector2 newPosition, Tower newUsedTower, int newPrice)
    {
        _rectTransform.localPosition = newPosition;
        _priceText.text = newPrice.ToString();

        _sellButton.onClick.RemoveAllListeners();
        _sellButton.onClick.AddListener(() => newUsedTower.ResellTower());
    }
}