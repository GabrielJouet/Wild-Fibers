using System.Collections;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class used to handle map selection menus.
    /// </summary>
    public class DisplayController : MonoBehaviour
    {
        /// <summary>
        /// Hider used.
        /// </summary>
        [SerializeField]
        private GameObject hider;


        /// <summary>
        /// Previous object displayed.
        /// </summary>
        private GameObject _displayedObject;


        /// <summary>
        /// Did the game is paused?
        /// </summary>
        public bool Paused { get; private set; }



        /// <summary>
        /// Method used to pause the game.
        /// </summary>
        public void PauseGame()
        {
            Time.timeScale = 0f;
            Paused = true;
        }


        /// <summary>
        /// Method used to pause the game.
        /// </summary>
        public void UnPauseGame()
        {
            Time.timeScale = 1f;
            Paused = false;
        }


        /// <summary>
        /// Method used to toogle pause.
        /// </summary>
        public void TooglePause()
        {
            Paused = !Paused;
            Time.timeScale = Paused ? 0f : 1f;
        }


        /// <summary>
        /// Method used to display menu.
        /// </summary>
        /// <param name="displayedObject">The new displayed object</param>
        public void DisplayObject(GameObject displayedObject)
        {
            if (displayedObject == _displayedObject)
            {
                _displayedObject.SetActive(false);
                hider.SetActive(false);
                _displayedObject = null;
            }
            else
            {
                if (_displayedObject)
                    _displayedObject.SetActive(false);

                _displayedObject = displayedObject;

                _displayedObject.SetActive(true);
                StartCoroutine(DelayHiderSpawn());
            }
        }


        /// <summary>
        /// Coroutine used to delay the hider display.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelayHiderSpawn()
        {
            yield return new WaitForEndOfFrame();
            hider.SetActive(true);
        }


        /// <summary>
        /// Method used to reset displayed menu.
        /// </summary>
        public void ResetDisplay()
        {
            _displayedObject.SetActive(false);
            _displayedObject = null;

            hider.SetActive(false);

            UnPauseGame();
        }
    }
}