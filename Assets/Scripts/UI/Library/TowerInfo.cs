using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TowerInfo : MonoBehaviour
{
    private TowerData _tower;
    public TowerData Tower
    {
        get => _tower;

        set
        {
            _tower = value;
            GetComponent<Button>().enabled = true;
            transform.GetChild(0).GetComponent<Image>().sprite = Tower.ScreenShot;
        }
    }


    public void Activate()
    {
        transform.parent.parent.parent.parent.GetComponent<Library>().ShowTowerInfo(this);
    }
}