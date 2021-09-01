using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tower info button used in library.
/// </summary>
[RequireComponent(typeof(Button))]
public class TowerInfo : MonoBehaviour
{
    /// <summary>
    /// Tower data buffered in this tower info.
    /// </summary>
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


    /// <summary>
    /// Method called to activate this button.
    /// </summary>
    public void Activate()
    {
        transform.parent.parent.parent.parent.GetComponent<Library>().ShowTowerInfo(this);
    }
}