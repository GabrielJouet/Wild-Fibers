using System.Collections.Generic;
using UnityEngine;

/*
 * Class used in tower construction UI
 */
public class TowerButtonController : MonoBehaviour
{
    [Header("Components")]
    //Player controller that handles tower list and ressources
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private RessourceController _ressourceController;


    [Header("Button Elements")]
    //Transform component of choose button
    [SerializeField]
    private List<TowerButton> _towerButtons;
    [SerializeField]
    private TowerButton _sellButton;
    [SerializeField]
    private RectTransform _rectTransform;



    //Start method, called when an object is started
    private void Start()
    {
        List<Tower> buffer = _playerController.GetTowers();

        for (int i = 0; i < _towerButtons.Count; i ++)
            _towerButtons[i].Initialize(buffer[i].Icon, buffer[i].Price);
    }


    //Method used to activate choose buttons when the button is pressed
    //
    //Parameters => newPosition, new buttons position according to mouse
    //              newUsedTowerSlot, the tower slot pressed
    public void ActivatePurchaseButtons(Vector2 newPosition, TowerSlot newUsedTowerSlot)
    {
        _rectTransform.localPosition = newPosition;

        for (int i = 0; i < _towerButtons.Count; i++)
        {
            Tower buffer = _playerController.GetTower(i);

            if (buffer.Price > _ressourceController.GoldCount)
                _towerButtons[i].Desactivate();
            else
                _towerButtons[i].Activate(() => newUsedTowerSlot.ChooseTower(buffer));
        }
    }


    public void DesactivatePurchaseButtons()
    {
        for (int i = 0; i < _towerButtons.Count; i++)
            _towerButtons[i].gameObject.SetActive(false);
    }


    public void ActivateSellButton(Vector2 newPosition, Tower newTower, int newPrice)
    {
        _rectTransform.localPosition = newPosition;

        _sellButton.gameObject.SetActive(true);
        _sellButton.Initialize(newPrice);
        _sellButton.Activate(() => newTower.ResellTower());
    }


    public void DesactivateSellButton()
    {
        _sellButton.gameObject.SetActive(false);
    }
}