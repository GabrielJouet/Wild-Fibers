using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to store and manage level resources.
/// </summary>
[RequireComponent(typeof(RessourceController))]
public class LevelController : MonoBehaviour
{
    [Header("UI related")]

    /// <summary>
    /// Text component that displays wave number.
    /// </summary>
    [SerializeField]
    private Text _waveText;

    /// <summary>
    /// Game over object that is used when the player finish the level.
    /// </summary>
    [SerializeField]
    private GameOverScreen _gameOverScreen;


    [Header("Components")]

    /// <summary>
    /// Entity spawner prefab.
    /// </summary>
    [SerializeField]
    private Spawner _spawnerPrefab;
    private readonly List<Spawner> _spawners = new List<Spawner>();

    /// <summary>
    /// Next wave button component.
    /// </summary>
    [SerializeField]
    private NextWaveButton _nextWaveButton;

    /// <summary>
    /// Bezier curve paths.
    /// </summary>
    [SerializeField]
    private List<RandomPath> _availablePath;


    /// <summary>
    /// Level loaded.
    /// </summary>
    public Level LoadedLevel { get; private set; }

    /// <summary>
    /// Does the level is ended?
    /// </summary>
    public bool Ended { get; set; }


    /// <summary>
    /// Resource controller used to track lives and gold.
    /// </summary>
    private RessourceController _ressourceController;

    /// <summary>
    /// Save controller used to track advancements and saves.
    /// </summary>
    private SaveController _saveController;

    /// <summary>
    /// Pool controller component, used to store pools
    /// </summary>
    private PoolController _poolController;

    /// <summary>
    /// Time between each next wave button display
    /// </summary>
    private readonly float _timeBetweenNextWaveButtonDisplay = 5f;

    /// <summary>
    /// Current wave index
    /// </summary>
    private int _waveIndex = 0;



    /// <summary>
    /// Awake method used for initialization.
    /// </summary>
    private void Awake()
    {
        _saveController = Controller.Instance.SaveControl;
        LoadedLevel = _saveController.LoadedLevel;

        _ressourceController = GetComponent<RessourceController>();
        _poolController = Controller.Instance.PoolControl;
        _poolController.ReInitialize();

        //If we unlock new towers in this level.
        if (LoadedLevel.TowerLevel > _saveController.SaveFile.CurrentSquad.TowerLevelMax)
        {
            //TO DO : DISPLAY A SCREEN WITH NEW TOWERS
            _saveController.SaveTowerLevel(LoadedLevel.TowerLevel);
        }

        _waveText.text = 0 + " / " + LoadedLevel.Waves.Count;
    }


    /// <summary>
    /// Method called when the new wave button is used.
    /// </summary>
    /// <param name="timeLeft">How much time left is available?</param>
    public void StartWaveViaButton(float timeLeft)
    {
        StartWave();

        //If there is time left, we give money to player based on time left.
        if (timeLeft > 0)
            _ressourceController.AddGold(Mathf.FloorToInt(LoadedLevel.Waves[_waveIndex].BonusGold * (timeLeft / LoadedLevel.Waves[_waveIndex].TimeWave)), false);
    }


    /// <summary>
    /// Method called when we start a new wave.
    /// </summary>
    private void StartWave()
    {
        _waveText.text = (_waveIndex + 1) + " / " + LoadedLevel.Waves.Count;

        int spawnerLeft = LoadedLevel.Waves[_waveIndex].EnemyGroups.Count - _spawners.Count;

        for(int i = 0; i < spawnerLeft; i ++)
            _spawners.Add(Instantiate(_spawnerPrefab, transform));

        int j = 0;
        foreach(EnemyGroup current in LoadedLevel.Waves[_waveIndex].EnemyGroups)
        {
            int index = Controller.Instance.EnemyControl.Enemies.IndexOf(current.Enemy.GetComponent<Enemy>());

            //If in this wave we uncounter new enemies.
            if (!_saveController.SaveFile.EnemiesUnlocked[index])
            {
                //TO DO : DISPLAY ENEMY VIGNETTE
                _saveController.SaveNewEnemyFound(index);
            }

            _spawners[j].SetNewGroup(_availablePath[current.Path], current, this, _poolController);
            j++;
        }
    }


    /// <summary>
    /// Method called when the wave is finished.
    /// </summary>
    public void EndWave()
    {
        bool result = true;

        foreach (Spawner current in _spawners)
            if (!current.WaveFinished)
                result = false;

        Debug.Log(result);

        if (result)
            StartCoroutine(DelayWave());
    }


    /// <summary>
    /// Coroutine used to delay the next wave.
    /// </summary>
    /// <returns>Yield the time between next wave display</returns>
    private IEnumerator DelayWave()
    {
        //If there is another wave after that one
        if (_waveIndex + 1 < LoadedLevel.Waves.Count)
        {
            _waveIndex++;
            yield return new WaitForSeconds(_timeBetweenNextWaveButtonDisplay);

            _nextWaveButton.ActivateNewWaveButton(LoadedLevel.Waves[_waveIndex].TimeWave);
        }
        else
            foreach (Spawner current in _spawners)
                current.NotifyPool();
    }


    /// <summary>
    /// Method used when the level is finished.
    /// </summary>
    public void EndLevel(bool lose)
    {
        bool result = true;

        if(!lose)
            foreach (Spawner current in _spawners)
                if (!current.EnemiesKilled)
                    result = false;

        Debug.Log(result);

        if (result)
            StartCoroutine(DelayGameScreen(lose));

        Ended = result;
    }


    /// <summary>
    /// Coroutine used to delay game over screen display.
    /// </summary>
    private IEnumerator DelayGameScreen(bool lose)
    {
        yield return new WaitForSeconds(1f);
        _gameOverScreen.Activate(!lose);
    }
}