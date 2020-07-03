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
    [SerializeField]
    private SphereCollider _collider;

    private Tower _currentTower = null;

    private bool _chooserActive = false;


    private void OnMouseDown()
    {
        if(_currentTower == null)
        {
            Vector2 cameraScreen = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 finalPosition = new Vector2(cameraScreen.x - Screen.width / 2, cameraScreen.y - Screen.height / 2);

            if (!_chooserActive)
                _informationUIController.ActivateTowerChooseButton(finalPosition, this);
            else
                _informationUIController.DisableTowerChooseButton();

            _chooserActive = !_chooserActive;
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
            _currentTower.Initialize(this, _ressourceController, _informationUIController);

            _collider.enabled = false;
            _informationUIController.DisableTowerChooseButton();
        }
    }


    public void ResetChooser()
    {
        _chooserActive = false;
    }


    public void ResetSlot()
    {
        _currentTower = null;
        _collider.enabled = true;
    }
}