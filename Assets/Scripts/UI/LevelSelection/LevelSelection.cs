using System.Collections.Generic;
using Levels;
using Miscellanious;
using Miscellanious.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelSelection
{
    /// <summary>
    /// Class used to handles level selection menu.
    /// </summary>
    public class LevelSelection : MonoBehaviour
    {
        [Header("Menu Elements")]

        /// <summary>
        /// Level name text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI levelName;

        /// <summary>
        /// Level Thumbnail.
        /// </summary>
        [SerializeField]
        private Image levelPicture;

        /// <summary>
        /// Level description text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI levelDescription;

        /// <summary>
        /// Launch battle menu button.
        /// </summary>
        [SerializeField]
        private Button launchBattleMenu;

        /// <summary>
        /// Scores objects displayed.
        /// </summary>
        [SerializeField]
        private List<Image> scores;

        /// <summary>
        /// Classic layout parent object.
        /// </summary>
        [SerializeField]
        private GameObject classicArrow;

        /// <summary>
        /// Classic layout parent object.
        /// </summary>
        [SerializeField]
        private GameObject classicLayout;

        /// <summary>
        /// Activated sprite of score items.
        /// </summary>
        [SerializeField]
        private Sprite activatedSprite;

        /// <summary>
        /// Deactivated sprite of score items.
        /// </summary>
        [SerializeField]
        private Sprite deactivatedSprite;


        [Header("Side Panel")]

        /// <summary>
        /// Side and challenge score icon.
        /// </summary>
        [SerializeField]
        private Image sideScore;

        /// <summary>
        /// Classic layout parent object.
        /// </summary>
        [SerializeField]
        private GameObject sideArrow;

        /// <summary>
        /// Side button component.
        /// </summary>
        [SerializeField]
        private Button sideButton;

        /// <summary>
        /// Side and challenge layout parent object.
        /// </summary>
        [SerializeField]
        private GameObject sideLayout;

        /// <summary>
        /// Sprite of the side activated.
        /// </summary>
        [SerializeField]
        private Sprite sideActivated;

        /// <summary>
        /// Sprite of the side deactivated.
        /// </summary>
        [SerializeField]
        private Sprite sideDeactivated;

        /// <summary>
        /// Side icon component.
        /// </summary>
        [SerializeField]
        private Image sideIcon;


        [Header("Challenge Panel")]

        /// <summary>
        /// Classic layout parent object.
        /// </summary>
        [SerializeField]
        private GameObject challengeArrow;

        /// <summary>
        /// Challenge button component.
        /// </summary>
        [SerializeField]
        private Button challengeButton;

        /// <summary>
        /// Sprite of the challenge activated.
        /// </summary>
        [SerializeField]
        private Sprite challengeActivated;

        /// <summary>
        /// Sprite of the challenge deactivated.
        /// </summary>
        [SerializeField]
        private Sprite challengeDeactivated;

        /// <summary>
        /// Challenge icon component.
        /// </summary>
        [SerializeField]
        private Image challengeIcon;



        [Header("Component")]

        /// <summary>
        /// Scene changer component.
        /// </summary>
        [SerializeField]
        private ChangeScene sceneChanger;

        /// <summary>
        /// Display controller component.
        /// </summary>
        [SerializeField]
        private DisplayController displayController;

        
        /// <summary>
        /// Level data buffered.
        /// </summary>
        private LevelData _level;

        /// <summary>
        /// Previous arrow displayed when changing menu.
        /// </summary>
        private GameObject _previousDisplayedObject;


        /// <summary>
        /// Method used to open level selection menu.
        /// </summary>
        /// <param name="buttonUsed">Which button was used</param>
        public void ActivateLevelSelectionMenu(LevelButton buttonUsed)
        {
            _level = buttonUsed.LevelData;

            LevelState buffer = Controller.Instance.SaveController.RecoverLevelSave(_level.Classic).State;
            bool sidedOrChallenged = buffer is LevelState.SIDED or LevelState.CHALLENGED;
            bool notLocked = buffer == LevelState.COMPLETED || sidedOrChallenged;

            sideButton.interactable = notLocked;
            sideIcon.sprite = notLocked ? sideActivated : sideDeactivated;

            challengeButton.interactable = sidedOrChallenged;
            challengeIcon.sprite = sidedOrChallenged ? challengeActivated : challengeDeactivated;

            displayController.DisplayObject(gameObject);

            _previousDisplayedObject = classicArrow;
            classicArrow.SetActive(true);
            sideArrow.SetActive(false);
            challengeArrow.SetActive(false);

            ActivateBaseMenu();
        }


        /// <summary>
        /// Method called to activate the base menu.
        /// </summary>
        public void ActivateBaseMenu()
        {
            classicLayout.SetActive(true);
            sideLayout.SetActive(false);

            levelName.text = _level.Classic.Name;
            levelPicture.sprite = _level.Classic.Picture;
            levelDescription.text = _level.Classic.Description;

            int seedGainedBuffer = Controller.Instance.SaveController.RecoverLevelSave(_level.Classic).SeedsGained;
            for (int i = 0; i < scores.Count; i++)
                scores[i].sprite = seedGainedBuffer > i ? activatedSprite : deactivatedSprite;

            launchBattleMenu.onClick.RemoveAllListeners();
            launchBattleMenu.onClick.AddListener(() =>
            {
                Controller.Instance.SaveController.LoadedLevelData = _level;
                Controller.Instance.SaveController.LoadedLevelType = LevelType.CLASSIC;
                
                sceneChanger.LoadScene("CombatScene");
            });
        }


        /// <summary>
        /// Method called to activate alternate menu (side or challenge).
        /// </summary>
        /// <param name="levelType">The type of level activated, side or challenge</param>
        public void ActivateAlternateMenu(string levelType)
        {
            LevelType enumBuffer = (LevelType)System.Enum.Parse(typeof(LevelType), levelType);

            Level buffer = enumBuffer == LevelType.SIDE ? _level.Side : _level.Challenge;

            classicLayout.SetActive(false);
            sideLayout.SetActive(true);

            levelName.text = buffer.Name;
            levelPicture.sprite = buffer.Picture;
            levelDescription.text = buffer.Description;

            sideScore.sprite = deactivatedSprite;

            LevelState bufferState = Controller.Instance.SaveController.RecoverLevelSave(buffer).State;

            if (enumBuffer == LevelType.SIDE && bufferState is LevelState.SIDED or LevelState.CHALLENGED || enumBuffer == LevelType.CHALLENGE && bufferState == LevelState.CHALLENGED)
                sideScore.sprite = activatedSprite;

            launchBattleMenu.onClick.RemoveAllListeners();
            launchBattleMenu.onClick.AddListener(() =>
            {
                Controller.Instance.SaveController.LoadedLevelData = _level;
                Controller.Instance.SaveController.LoadedLevelType = enumBuffer;
                
                sceneChanger.LoadScene("CombatScene");
            });
        }


        /// <summary>
        /// Display a new arrow based on the button pressed.
        /// </summary>
        /// <param name="displayedObject">The arrow to display</param>
        public void DisplayChoice(GameObject displayedObject)
        {
            if (_previousDisplayedObject != null)
                _previousDisplayedObject.SetActive(false);

            _previousDisplayedObject = displayedObject;
            displayedObject.SetActive(true);
        }
    }
}