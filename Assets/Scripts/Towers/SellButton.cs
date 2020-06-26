using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    [SerializeField]
    private Button _sellButton;

    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    private Text _price;


    public void Activate(Vector2 newPosition, TowerSlot newUsedTowerSlot, int newPrice)
    {
        _rectTransform.localPosition = newPosition;
        _price.text = newPrice.ToString();

        _sellButton.onClick.RemoveAllListeners();
        _sellButton.onClick.AddListener(() => newUsedTowerSlot.ResellTower());
    }
}