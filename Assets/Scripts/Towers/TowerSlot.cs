using System.Collections.Generic;
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    [SerializeField]
    private List<Tower> _availableTowers;

    [SerializeField]
    private GameObject _chooseButton;


    private bool _towerCreated = false;

    private void OnMouseDown()
    {
        if(!_towerCreated)
        {
            _chooseButton.SetActive(true);

            Vector2 cameraScreen = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 finalPosition = new Vector2(cameraScreen.x - Screen.width / 2, cameraScreen.y - Screen.height / 2);
            _chooseButton.GetComponent<ChooseButton>().Activate(finalPosition, this);
        }
    }


    public void ChooseTower(int index)
    {
        Instantiate(_availableTowers[index], transform.position, Quaternion.identity);
        _chooseButton.SetActive(false);

        _towerCreated = true;
    }
}