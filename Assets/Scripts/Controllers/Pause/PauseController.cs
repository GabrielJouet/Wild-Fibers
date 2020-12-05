using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("UI Elements")]
    //Main menu object
    [SerializeField]
    protected GameObject _menuObject;
    //Hider, hier is the border of the menu, shading every other textures
    [SerializeField]
    protected GameObject _hider;


    //Did the game actually paused or not?
    protected bool _paused = false;



    //Fixed Update Method
    //Called every frame (usually 60 or 144 times per second)
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame(true);
    }


    //Method used to pause the game and freeze time
    public virtual void PauseGame(bool showMenu) { }
}
