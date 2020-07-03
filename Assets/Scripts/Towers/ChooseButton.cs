using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseButton : MonoBehaviour
{
    [SerializeField]
    private List<Button> _interactableButtons;

    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    private List<Text> _prices;


    public void Activate(Vector2 newPosition, TowerSlot newUsedTowerSlot)
    {
        _rectTransform.localPosition = newPosition;

        for (int i = 0; i < _interactableButtons.Count; i++)
        {
            int a = i;
            _interactableButtons[i].onClick.RemoveAllListeners();
            _interactableButtons[i].onClick.AddListener(() => newUsedTowerSlot.ChooseTower(a));
        }
    }
}