using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private Button _sellButton;
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private Text _priceText;


    public void Activate(Vector2 newPosition, Tower newUsedTower, int newPrice)
    {
        _rectTransform.localPosition = newPosition;
        _priceText.text = newPrice.ToString();

        _sellButton.onClick.RemoveAllListeners();
        _sellButton.onClick.AddListener(() => newUsedTower.ResellTower());
    }
}