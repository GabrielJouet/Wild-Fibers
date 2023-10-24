using System.Collections.Generic;
using Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    /// <summary>
    /// Class used for game over screen and handling end of level.
    /// </summary>
    public class GameOverScreen : MonoBehaviour
    {
        [Header("UI elements")]

        /// <summary>
        /// Main text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI mainText;

        /// <summary>
        /// Game screen image component.
        /// </summary>
        [SerializeField]
        private Image gameScreen;

        /// <summary>
        /// Sprite for win.
        /// </summary>
        [SerializeField]
        private Sprite winScreen;

        /// <summary>
        /// Sprite for loose.
        /// </summary>
        [SerializeField]
        private Sprite loseScreen;

        /// <summary>
        /// List of all seeds at the end of the level.
        /// </summary>
        [SerializeField]
        private List<Image> seedScores;

        /// <summary>
        /// Activated seed sprite.
        /// </summary>
        [SerializeField]
        private Sprite activatedSprite;


        [Header("Components")]

        /// <summary>
        /// Display controller object.
        /// </summary>
        [SerializeField]
        private DisplayController displayController;



        /// <summary>
        /// Method used to activate and display the screen.
        /// </summary>
        /// <param name="win">Does the player wins this level?</param>
        /// <param name="sideOrChallenge">Does the level is a side or challenge one?</param>
        public void Activate(bool win, bool sideOrChallenge)
        {
            displayController.PauseGame();
            displayController.DisplayObject(gameObject);

            gameScreen.gameObject.SetActive(true);

            gameScreen.sprite = win ? winScreen : loseScreen;
            mainText.text = win ? "Win" : "Lose";

            if (sideOrChallenge)
            {
                seedScores[0].gameObject.SetActive(false);
                seedScores[2].gameObject.SetActive(false);
            }

            if (win)
            {
                int livesLost = RessourceController.Instance.LivesLost;

                seedScores[2].sprite = livesLost <= 15 ? activatedSprite : seedScores[2].sprite;
                seedScores[1].sprite = livesLost <= 10 ? activatedSprite : seedScores[1].sprite;
                seedScores[0].sprite = livesLost <= 3 ? activatedSprite : seedScores[0].sprite;

                Controller.Instance.SaveController.SaveLevelData(livesLost);
            }
        }
    }
}