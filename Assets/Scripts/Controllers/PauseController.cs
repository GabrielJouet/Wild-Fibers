using UnityEngine;

/*
 * Class used to handle pause either when winning or pausing game
 */
public class PauseController : MonoBehaviour
{
    [Header("UI Elements")]
    //Main menu object
    [SerializeField]
    private GameObject _menuObject;
    //Hider, hier is the border of the menu, shading every other textures
    [SerializeField]
    private GameObject _hider;


    [Header("Component")]
    //Background selecter used when clicking on the background of the game
    [SerializeField]
    private BackgroudSelecter _backgroundSelecter;


    //Did the game actually paused or not?
    private bool _paused = false;



    //Fixed Update Method
    //Called every frame (usually 60 or 144 times per second)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame(_menuObject);
    }


    //Method used to pause the game and freeze time
    //
    //Parameter => The menu related to this pause
    public void PauseGame(GameObject menuToActivate)
    {
        //TO REWORK
        menuToActivate.SetActive(!_paused);
        _hider.SetActive(!_paused);
        _backgroundSelecter.enabled = _paused;

        foreach (Tower current in FindObjectsOfType<Tower>())
            current.PauseBehavior();

        foreach (Enemy current in FindObjectsOfType<Enemy>())
        {
            if(current.isActiveAndEnabled)
            {
                current.Pause(_paused);
                current.enabled = _paused;
            }
            else
            {
                current.enabled = _paused;
                current.Pause(_paused);
            }
        }

        foreach (Spawner current in FindObjectsOfType<Spawner>())
            current.PauseSpawn();

        foreach (EnemyPool current in FindObjectsOfType<EnemyPool>())
            current.enabled = _paused;

        FindObjectOfType<RessourceController>().enabled = _paused;

        FindObjectOfType<LevelController>().enabled = _paused;

        _paused = !_paused;
    }
}