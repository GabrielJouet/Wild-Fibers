using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private GameObject _menuObject;
    [SerializeField]
    private GameObject _hider;


    [Header("Component")]
    [SerializeField]
    private BackgroudSelecter _backgroundSelecter;


    private bool _paused = false;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame(_menuObject);
    }


    public void PauseGame(GameObject menuToActivate)
    {
        //TO REWORK
        menuToActivate.SetActive(!_paused);
        _hider.SetActive(!_paused);
        _backgroundSelecter.enabled = _paused;

        foreach (Tower current in FindObjectsOfType<Tower>())
            current.enabled = _paused;

        foreach (Enemy current in FindObjectsOfType<Enemy>())
            current.enabled = _paused;

        foreach (Spawner current in FindObjectsOfType<Spawner>())
            current.enabled = _paused;

        foreach (EnemyPool current in FindObjectsOfType<EnemyPool>())
            current.enabled = _paused;

        FindObjectOfType<RessourceController>().enabled = _paused;

        FindObjectOfType<LevelController>().enabled = _paused;

        _paused = !_paused;
    }
}