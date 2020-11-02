using UnityEngine;

/*
 * Class used to handle pause either when winning or pausing game
 */
public class GamePauseController : PauseController
{
    [Header("Component")]
    [SerializeField]
    private LevelController _levelController;
    [SerializeField]
    private GameOverScreen _gameOverScreen;
    [SerializeField]
    private NextWaveButton _nextWaveButton;


    //Method used to pause the game and freeze time
    public override void PauseGame()
    {
        _menuObject.SetActive(!_paused);
        _hider.SetActive(!_paused);

        foreach (Tower current in FindObjectsOfType<Tower>())
            current.PauseBehavior();

        foreach (Enemy current in FindObjectsOfType<Enemy>())
        {
            current.Pause(_paused);
            current.enabled = _paused;
        }

        foreach (TowerSlot current in FindObjectsOfType<TowerSlot>())
            current.PauseBehavior();

        foreach (Spawner current in _levelController.GetSpawners())
            current.PauseSpawn();

        _levelController.PauseBehavior();
        _gameOverScreen.PauseBehavior();
        _nextWaveButton.PauseBehavior();

        _paused = !_paused;
    }
}