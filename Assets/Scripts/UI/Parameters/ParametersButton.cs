using UnityEngine;

/// <summary>
/// Class used to add a parameter and pause button.
/// </summary>
public class ParametersButton : MonoBehaviour
{
    [Header("UI Elements")]

    /// <summary>
    /// Related menu object.
    /// </summary>
    [SerializeField]
    private GameObject _menuObject;

    /// <summary>
    /// Hider object related.
    /// </summary>
    [SerializeField]
    private GameObject _hider;



    /// <summary>
    /// Update method called each frame.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenMenu();
    }


    /// <summary>
    /// Method used to open menu.
    /// </summary>
    public void OpenMenu()
    {
        _menuObject.SetActive(!_menuObject.activeSelf);
        _hider.SetActive(!_hider.activeSelf);
    }
}