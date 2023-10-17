using System.Collections;
using System.Collections.Generic;
using Levels.Path;
using Levels.Waves;
using Miscellanious.Enums;
using Save;
using TMPro;
using UnityEngine;

namespace Levels
{
    /// <summary>
    /// Class used to store and manage level resources.
    /// </summary>
    public class LevelController : MonoBehaviour
    {
        [Header("UI related")]

        /// <summary>
        /// Text component that displays wave number.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI waveText;

        /// <summary>
        /// Game over object that is used when the player finish the level.
        /// </summary>
        [SerializeField]
        private GameOverScreen gameOverScreen;


        [Header("Components")]

        /// <summary>
        /// Next wave button component.
        /// </summary>
        [SerializeField]
        private NextWaveButton nextWaveButton;

        /// <summary>
        /// Bezier curve paths.
        /// </summary>
        [SerializeField]
        private List<RandomPath> availablePath;
        

        /// <summary>
        /// Entity spawner prefab.
        /// </summary>
        private readonly List<Spawner> _spawners = new();


        /// <summary>
        /// Level loaded.
        /// </summary>
        public Level LoadedLevel { get; private set; }

        /// <summary>
        /// Does the level is ended?
        /// </summary>
        public bool Ended { get; private set; }


        /// <summary>
        /// Resource controller used to track lives and gold.
        /// </summary>
        private RessourceController _resourceController;

        /// <summary>
        /// Save controller used to track advancements and saves.
        /// </summary>
        private SaveController _saveController;

        /// <summary>
        /// Time between each next wave button display
        /// </summary>
        private const float TimeBetweenNextWaveButtonDisplay = 5f;

        /// <summary>
        /// Current wave index
        /// </summary>
        private int _waveIndex;



        /// <summary>
        /// Awake method used for initialization.
        /// </summary>
        private void Awake()
        {
            _saveController = Controller.Instance.SaveController;
            LoadedLevel = _saveController.LoadedLevel;

            _resourceController = GetComponent<RessourceController>();

            //If we unlock new towers in this level.
            if (LoadedLevel.TowerLevel > _saveController.SaveFile.CurrentSquad.TowerLevelMax)
            {
                //TO DO : DISPLAY A SCREEN WITH NEW TOWERS
                _saveController.SaveTowerLevel(LoadedLevel.TowerLevel);
            }

            waveText.text = 0 + " / " + LoadedLevel.Waves.Count;
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
                _resourceController.AddGold(Mathf.FloorToInt(LoadedLevel.Waves[_waveIndex].GoldBonus * (timeLeft / LoadedLevel.Waves[_waveIndex].TimeBeforeNextWave)), false);
        }


        /// <summary>
        /// Method called when we start a new wave.
        /// </summary>
        private void StartWave()
        {
            waveText.text = _waveIndex + 1 + " / " + LoadedLevel.Waves.Count;

            int spawnerLeft = LoadedLevel.Waves[_waveIndex].EnemyGroups.Count - _spawners.Count;

            for (int i = 0; i < spawnerLeft; i ++)
            {
                Spawner spawner = new GameObject("Spawner").AddComponent<Spawner>();
                spawner.transform.parent = transform;
                _spawners.Add(spawner);
            }

            int j = 0;
            foreach(EnemyGroup enemyGroup in LoadedLevel.Waves[_waveIndex].EnemyGroups)
            {
                int index = Controller.Instance.EnemyController.Enemies.IndexOf(enemyGroup.Enemy.GetComponent<Enemy>());

                //If in this wave we uncounter new enemies.
                if (!_saveController.SaveFile.EnemiesUnlocked[index])
                {
                    //TO DO : DISPLAY ENEMY VIGNETTE
                    _saveController.SaveNewEnemyFound(index);
                }

                _spawners[j].SetNewGroup(availablePath[enemyGroup.PathIndex], enemyGroup, this);
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
                yield return new WaitForSeconds(TimeBetweenNextWaveButtonDisplay);

                nextWaveButton.ActivateNewWaveButton(LoadedLevel.Waves[_waveIndex].TimeBeforeNextWave);
            }
            else
                foreach (Spawner spawner in _spawners)
                    spawner.WaitEnd = true;
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
            gameOverScreen.Activate(!lose, LoadedLevel.Type != LevelType.CLASSIC);
        }
    }
}