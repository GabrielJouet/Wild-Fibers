using UnityEngine;
using UnityEngine.UI;

public class InfoIcon : MonoBehaviour
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


    private Enemy _enemy;
    public Enemy Enemy
    {
        get
        {
            return _enemy;
        }

        set
        {
            _enemy = value;
            GetComponent<Image>().sprite = Enemy.ScreenShot;
        }
    }


    public void Activate()
    {
        transform.parent.parent.parent.GetComponent<Library>().ShowSpecificInfo(this);
    }
}