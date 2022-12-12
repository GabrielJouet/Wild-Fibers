using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tower info button used in library.
/// </summary>
[RequireComponent(typeof(Button))]
public class TowerInfo : MonoBehaviour
{
    /// <summary>
    /// ScreenShot image component, used to display actual tower.
    /// </summary>
    [SerializeField]
    private Image _screenShot;


    /// <summary>
    /// Locked sprite game object, used to show that this tower is locked.
    /// </summary>
    [SerializeField]
    private GameObject _lockedSprite;


    /// <summary>
    /// Tower data buffered in this tower info.
    /// </summary>
    private Tower _tower;
    public Tower Tower
    {
        get => _tower;

        set
        {
            _tower = value;
            GetComponent<Button>().enabled = true;

            _lockedSprite.SetActive(false);
            _screenShot.sprite = _tower.ScreenShot;
            _screenShot.gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// Method called to activate this button.
    /// </summary>
    public void Activate()
    {
        transform.parent.parent.parent.parent.parent.parent.GetComponent<Library>().ShowTowerInfo(this);
    }
}