using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

        [Header("Pause button")]
        /// <summary>
        /// Pause button sprite.
        /// </summary>
        [SerializeField] 
        private Sprite pauseButtonSprite;
        
        /// <summary>
        /// Unpause button sprite.
        /// </summary>
        [SerializeField] 
        private Sprite unPauseButtonSprite;
        
        /// <summary>
        /// Pause button display component.
        /// </summary>
        [SerializeField] 
        private Image pauseButton;


        /// <summary>
        /// Previous object displayed.
        /// </summary>
        private GameObject _displayedObject;


        /// <summary>
        /// Did the game is paused?
        /// </summary>
        public bool Paused { get; private set; }
        
        /// <summary>
        /// Does an object is displayed?
        /// </summary>
        public bool ObjectDisplay => _displayedObject != null;


        /// <summary>
        /// Method used to pause the game.
        /// </summary>
        public void PauseGame()
        {
            Time.timeScale = 0f;
            Paused = true;
            UpdatePauseButton();
        }


        /// <summary>
        /// Method used to pause the game.
        /// </summary>
        public void UnPauseGame()
        {
            Time.timeScale = 1f;
            Paused = false;
            UpdatePauseButton();
        }


        /// <summary>
        /// Method used to toogle pause.
        /// </summary>
        public void TooglePause()
        {
            Paused = !Paused;
            Time.timeScale = Paused ? 0f : 1f;
            UpdatePauseButton();
        }


        /// <summary>
        /// Method used to update the pause button.
        /// </summary>
        private void UpdatePauseButton()
        {
            if (pauseButton)
                pauseButton.sprite = Paused ? unPauseButtonSprite : pauseButtonSprite;
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