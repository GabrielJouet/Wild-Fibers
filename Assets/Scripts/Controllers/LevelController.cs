using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to store and manage level resources.
/// </summary>
[RequireComponent(typeof(RessourceController))]
[RequireComponent(typeof(PoolController))]
public class LevelController : MonoBehaviour
{
    /// <summary>
    /// Level loaded.
    /// </summary>
    public Level LoadedLevel { get; private set; }

    /// <summary>
    /// Does the level is ended?
    /// </summary>
    public bool Ended { get; set; }


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
    /// Resource controller used to track lives and gold.
    /// </summary>
    private RessourceController _ressourceController;

    /// <summary>
    /// Time between each next wave button display
    /// </summary>
    private readonly float _timeBetweenNextWaveButtonDisplay = 5f;

    private PoolController _poolController;

    /// <summary>
    /// Current wave index
    /// </summary>
    private int _waveIndex = 0;



    /// <summary>
    /// Awake method used for initialization.
    /// </summary>
    private void Awake()
    {
        SaveController buffer = FindObjectOfType<SaveController>();

        switch(buffer.LoadedLevel)
        {
            case LevelType.CLASSIC:
                LoadedLevel = buffer.Levels[buffer.LevelIndex].Classic;
                break;

            case LevelType.SIDE:
                LoadedLevel = buffer.Levels[buffer.LevelIndex].Side;
                break;

            case LevelType.CHALLENGE:
                LoadedLevel = buffer.Levels[buffer.LevelIndex].Challenge;
                break;
        }

        _ressourceController = GetComponent<RessourceController>();
        _poolController = GetComponent<PoolController>();
    }


    /// <summary>
    /// Start method called after Awake.
    /// </summary>
    private void Start()
    {
        _waveText.text = 0 + " / " + LoadedLevel.Waves.Count;
    }


    /// <summary>
    /// Method called when the new wave button is used.
    /// </summary>
    /// <param name="timeLeft">How much time left is available?</param>
    public void StartWaveViaButton(float timeLeft)
    {
        StartWave();

        //If there is time left, we gie money to player based on time left
        if (timeLeft > 0)
            _ressourceController.AddGold(Mathf.FloorToInt(LoadedLevel.Waves[_waveIndex].BonusGold * (timeLeft / LoadedLevel.Waves[_waveIndex].TimeWave)));
    }


    /// <summary>
    /// Method called when we start a new wave.
    /// </summary>
    private void StartWave()
    {
        _waveText.text = (_waveIndex + 1) + " / " + LoadedLevel.Waves.Count;

        int spawnerLeft = LoadedLevel.Waves[_waveIndex].EnemyGroups.Count - _spawners.Count;

        int i;
        for(i = 0; i < spawnerLeft; i ++)
            _spawners.Add(Instantiate(_spawnerPrefab, transform));

        i = 0;
        foreach(EnemyGroup current in LoadedLevel.Waves[_waveIndex].EnemyGroups)
        {
            _spawners[i].SetNewGroup(_availablePath[current.Path], current, this, _poolController);
            i++;
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

        if (result)
            StartCoroutine(DelayWave());
    }


    /// <summary>
    /// Method used when the level is finished.
    /// </summary>
    public void EndLevel()
    {
        bool result = true;

        foreach (Spawner current in _spawners)
            if (!current.EnemiesKilled)
                result = false;

        if (result)
            StartCoroutine(DelayGameScreen());

        Ended = result;
    }


    private IEnumerator DelayGameScreen()
    {
        yield return new WaitForSeconds(1f);
        _gameOverScreen.Activate(true);
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
}