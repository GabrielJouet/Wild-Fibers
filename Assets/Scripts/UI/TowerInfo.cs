using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour
{
    private TowerData _tower;
    public TowerData Tower
    {
        get
        {
            return _tower;
        }

        set
        {
            _tower = value;
            GetComponent<Image>().sprite = Tower.ScreenShot;
        }
    }


    public void Activate()
    {
        transform.parent.parent.parent.parent.GetComponent<Library>().ShowTowerInfo(this);
    }
}