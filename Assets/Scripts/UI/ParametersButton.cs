using UnityEngine;

/*
 * Pause menu UI
 */
public class ParametersButton : MonoBehaviour
{
    [Header("UI Elements")]
    //Related menu object
    [SerializeField]
    private GameObject _menuObject;

    //Hider object used to shade around
    [SerializeField]
    private GameObject _hider;


    //Does the menu is already opened?
    private bool _opened = false;



    //Update method, called every frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenMenu();
    }


    //Method used to open / close menu UI
    public void OpenMenu()
    {
        _menuObject.SetActive(!_opened);
        _hider.SetActive(!_opened);

        _opened = !_opened;
    }
}