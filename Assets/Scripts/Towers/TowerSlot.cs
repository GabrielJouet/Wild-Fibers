using System.Collections.Generic;
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    [SerializeField]
    private List<Tower> _availableTowers;

    [SerializeField]
    private GameObject _chooseButton;

    [SerializeField]
    private GameObject _sellButton;

    [SerializeField]
    private RessourceController _ressourceController;


    private List<TowerSlot> _otherSlots = new List<TowerSlot>();

    private Tower _currentTower = null;
    private bool _chooserActive = false;
    private bool _sellerActive = false;


    private void Start()
    {
        foreach (TowerSlot current in FindObjectsOfType<TowerSlot>())
            if(current != this)
                _otherSlots.Add(current);
    }


    private void OnMouseDown()
    {
        Vector2 cameraScreen = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 finalPosition = new Vector2(cameraScreen.x - Screen.width / 2, cameraScreen.y - Screen.height / 2);

        ResetOtherChooser();

        if (_currentTower == null)
        {
            if(!_chooserActive)
                _chooseButton.GetComponent<ChooseButton>().Activate(finalPosition, this);

            _chooseButton.SetActive(!_chooserActive);
            _chooserActive = !_chooserActive;
        }
        else
        {
            if(!_sellerActive)
                _sellButton.GetComponent<SellButton>().Activate(finalPosition, this);

            _sellButton.SetActive(!_sellerActive);
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
            _chooseButton.SetActive(false);
        }
    }


    public void ResellTower()
    {
        _ressourceController.AddGold(Mathf.FloorToInt(_currentTower.GetPrice() / 4));

        Destroy(_currentTower.gameObject);
        _currentTower = null;

        _sellButton.SetActive(false);

        ResetChooser();
    }


    public void ResetChooser()
    {
        _chooserActive = false;
        _sellerActive = false;
    }


    private void ResetOtherChooser()
    {
        _chooseButton.SetActive(false);
        _sellButton.SetActive(false);

        foreach (TowerSlot current in _otherSlots)
            current.ResetChooser();
    }
}