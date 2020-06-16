using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    [SerializeField]
    private Button _sellButton;

    [SerializeField]
    private RectTransform _rectTransform;


    public void Activate(Vector2 newPosition, TowerSlot newUsedTowerSlot)
    {
        _rectTransform.localPosition = newPosition;

        _sellButton.onClick.RemoveAllListeners();
        _sellButton.onClick.AddListener(() => newUsedTowerSlot.ResellTower());
    }
}