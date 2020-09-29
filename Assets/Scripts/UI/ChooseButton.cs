using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class used in tower construction UI
 */
public class ChooseButton : MonoBehaviour
{
    [Header("Components")]
    //Player controller that handles tower list and ressources
    [SerializeField]
    private PlayerController _playerController;


    [Header("Button Elements")]
    //List of interactable buttons
    [SerializeField]
    private List<Button> _interactableButtons;

    //Transform component of choose button
    [SerializeField]
    private List<Image> _buttonSprites;
    [SerializeField]
    private RectTransform _rectTransform;

    //List of texts components handling prices
    [SerializeField]
    private List<Text> _prices;



    //Start method, called when an object is started
    private void Start()
    {
        List<Tower> buffer = _playerController.GetTowers();

        for(int i = 0; i < _interactableButtons.Count; i ++)
        {
            _buttonSprites[i].sprite = buffer[i].GetIcon();
            _prices[i].text = buffer[i].GetPrice().ToString();
        }
    }


    //Method used to activate choose buttons when the button is pressed
    //
    //Parameters => newPosition, new buttons position according to mouse
    //              newUsedTowerSlot, the tower slot pressed
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