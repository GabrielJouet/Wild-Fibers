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


    private Tower _currentTower = null;


    private void OnMouseDown()
    {
        Vector2 cameraScreen = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 finalPosition = new Vector2(cameraScreen.x - Screen.width / 2, cameraScreen.y - Screen.height / 2);

        if (_currentTower == null)
        {
            _chooseButton.SetActive(true);
            _chooseButton.GetComponent<ChooseButton>().Activate(finalPosition, this);
        }
        else
        {
            _sellButton.SetActive(true);
            _sellButton.GetComponent<SellButton>().Activate(finalPosition, this);
        }
    }


    public void ChooseTower(int index)
    {
        _currentTower = Instantiate(_availableTowers[index], transform.position, Quaternion.identity);

        _chooseButton.SetActive(false);
    }


    public void ResellTower()
    {
        Destroy(_currentTower.gameObject);
        _currentTower = null;

        _sellButton.SetActive(false);
    }
}