using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    [SerializeField]
    private List<Tower> _availableTowers;

    [SerializeField]
    private GameObject _chooseButton;


    private void OnMouseDown()
    {
        _chooseButton.SetActive(true);
        Vector2 cameraScreen = Camera.main.WorldToScreenPoint(transform.position);

        _chooseButton.GetComponent<RectTransform>().localPosition = new Vector2(cameraScreen.x - Screen.width/2, cameraScreen.y - Screen.height / 2);
    }


    public void ChooseTower(int index)
    {
        Instantiate(_availableTowers[index]);
    }
}