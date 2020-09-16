using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private PlayerController _playerController;


    [Header("Button Elements")]
    [SerializeField]
    private List<Button> _interactableButtons;
    [SerializeField]
    private List<Image> _buttonSprites;
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private List<Text> _prices;



    private void Start()
    {
        List<Tower> buffer = _playerController.GetTowers();

        for(int i = 0; i < _interactableButtons.Count; i ++)
        {
            _buttonSprites[i].sprite = buffer[i].GetIcon();
            _prices[i].text = buffer[i].GetPrice().ToString();
        }
    }


    public void Activate(Vector2 newPosition, TowerSlot newUsedTowerSlot)
    {
        _rectTransform.localPosition = newPosition;

        for (int i = 0; i < _interactableButtons.Count; i++)
        {
            int a = i;
            _interactableButtons[i].onClick.RemoveAllListeners();
            _interactableButtons[i].onClick.AddListener(() => newUsedTowerSlot.ChooseTower(_playerController.GetTower(a)));
        }
    }
}