using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handle object info in library.
/// </summary>
[RequireComponent(typeof(Button))]
public class ObjectInfo : MonoBehaviour
{
    /// <summary>
    /// ScreenShot image component, used to display actual enemy.
    /// </summary>
    [SerializeField]
    protected Image _screenShot;


    /// <summary>
    /// Locked sprite game object, used to show that this enemy is locked.
    /// </summary>
    [SerializeField]
    protected GameObject _lockedSprite;


    /// <summary>
    /// Library shortcut object.
    /// </summary>
    private Library _library;



    /// <summary>
    /// Method called at initialization.
    /// </summary>
    /// <param name="library">Library object used to activate the info</param>
    public void Initialize(Library library)
    {
        _library = library;

        GetComponent<Button>().enabled = true;
        _screenShot.gameObject.SetActive(true);
        _lockedSprite.SetActive(false);
    }


    /// <summary>
    /// Method called to activate this button.
    /// </summary>
    public void Activate()
    {
        _library.ShowInfo(this);
    }
}