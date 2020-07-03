using System.Collections.Generic;
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    [Header("Tower Information")]
    [SerializeField]
    private List<Tower> _availableTowers;


    [Header("Component")]
    [SerializeField]
    private RessourceController _ressourceController;
    [SerializeField]
    private InformationUIController _informationUIController;

    private Tower _currentTower = null;

    private bool _chooserActive = false;
    private bool _sellerActive = false;


    private void OnMouseDown()
    {
        Vector2 cameraScreen = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 finalPosition = new Vector2(cameraScreen.x - Screen.width / 2, cameraScreen.y - Screen.height / 2);

        if (_currentTower == null)
        {
            if(!_chooserActive)
                _informationUIController.ActivateTowerChooseButton(finalPosition, this);
            else
                _informationUIController.DisableTowerChooseButton();

            _chooserActive = !_chooserActive;
        }
        else
        {
            if(!_sellerActive)
            {
                _informationUIController.ActivateTowerSellButton(finalPosition, this, Mathf.FloorToInt(_currentTower.GetPrice() / 4));

                _informationUIController.SetTowerInformation(_currentTower.GetIcon(), 
                                                             _currentTower.GetName(), 
                                                             _currentTower.GetDamage(), 
                                                             _currentTower.GetArmorThrough(), 
                                                             _currentTower.GetTimeBetweenShots());
            }
            else
            {
                _informationUIController.DisableTowerInformation();
                _informationUIController.DisableTowerSellButton();
            }

            _sellerActive = !_sellerActive;
        }
    }


    public void ChooseTower(int index)
    {
        if(_ressourceController.GetGoldCount() < _availableTowers[index].GetPrice())
        {
            //TO DO DISPLAY BAD CHOICE
        }
        else
        {
            _ressourceController.RemoveGold(_availableTowers[index].GetPrice());

            _currentTower = Instantiate(_availableTowers[index], transform.position, Quaternion.identity);
            _informationUIController.DisableTowerChooseButton();
        }
    }


    public void ResellTower()
    {
        _ressourceController.AddGold(Mathf.FloorToInt(_currentTower.GetPrice() / 4));
        _informationUIController.DisableTowerInformation();

        Destroy(_currentTower.gameObject);
        _currentTower = null;

        _informationUIController.DisableTowerSellButton();
    }


    public void ResetChooser()
    {
        _chooserActive = false;
        _sellerActive = false;
    }
}