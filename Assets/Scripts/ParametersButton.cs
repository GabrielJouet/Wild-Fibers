using UnityEngine;

public class ParametersButton : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private GameObject _menuObject;
    [SerializeField]
    private GameObject _hider;


    private bool _opened = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenMenu();
    }


    public void OpenMenu()
    {
        _menuObject.SetActive(!_opened);
        _hider.SetActive(!_opened);

        _opened = !_opened;
    }
}