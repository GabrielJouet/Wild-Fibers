using System.Collections;
using Levels;
using Miscellanious.Enums;
using UnityEngine;

namespace UI.LevelSelection
{
    /// <summary>
    /// Level button used to select a level.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class LevelButton : MonoBehaviour
    {
        [Header("Display")]

        /// <summary>
        /// Level selection screen.
        /// </summary>
        [SerializeField]
        private LevelSelection levelSelection;

        /// <summary>
        /// Hover displayer object.
        /// </summary>
        [SerializeField]
        private SpriteRenderer hoverDisplayer;

        /// <summary>
        /// Level data associated with this button.
        /// </summary>
        [field: SerializeField]
        public LevelData LevelData { get; private set; }


        [Header("Sprites")]

        /// <summary>
        /// Activated sprite.
        /// </summary>
        [SerializeField]
        private Sprite activatedSprite;

        /// <summary>
        /// Completed sprite.
        /// </summary>
        [SerializeField]
        private Sprite completedSprite;

        /// <summary>
        /// Sided sprite.
        /// </summary>
        [SerializeField]
        private Sprite sidedSprite;

        /// <summary>
        /// Challenged sprite.
        /// </summary>
        [SerializeField]
        private Sprite challengedSprite;


        [Header("Selection")]

        /// <summary>
        /// Unlocked hover sprite.
        /// </summary>
        [SerializeField]
        private Sprite unlockedHover;

        /// <summary>
        /// Finished hover sprite.
        /// </summary>
        [SerializeField]
        private Sprite finishedHover;

        /// <summary>
        /// Asided hover sprite.
        /// </summary>
        [SerializeField]
        private Sprite asideHover;

        /// <summary>
        /// Challenged hover sprite.
        /// </summary>
        [SerializeField]
        private Sprite challengedHover;

        /// <summary>
        /// Display controller shortcut.
        /// </summary>
        [SerializeField] 
        private DisplayController displayController;


        /// <summary>
        /// Does the button is locked?
        /// </summary>
        private bool _isLocked;

        /// <summary>
        /// Button display object.
        /// </summary>
        private SpriteRenderer _buttonDisplay;

        /// <summary>
        /// Does this component has been updated?
        /// </summary>
        private bool _hasBeenUpdated;



        /// <summary>
        /// Awake method, called at first.
        /// </summary>
        private void Awake()
        {
            _buttonDisplay = GetComponent<SpriteRenderer>();
        }


        /// <summary>
        /// Start method, changed as a coroutine to wait Controller initialization.
        /// </summary>
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => Controller.Instance);
            int index = Controller.Instance.SaveController.Levels.IndexOf(LevelData);
            _hasBeenUpdated = true;

            switch(Controller.Instance.SaveController.SaveFile.CurrentSave[index].State)
            {
                case LevelState.LOCKED:
                    _isLocked = true;
                    break;

                case LevelState.UNLOCKED:
                    hoverDisplayer.sprite = unlockedHover;
                    _buttonDisplay.sprite = activatedSprite;
                    break;

                case LevelState.COMPLETED:
                    _buttonDisplay.sprite = completedSprite;
                    hoverDisplayer.sprite = finishedHover;
                    break;

                case LevelState.SIDED:
                    _buttonDisplay.sprite = sidedSprite;
                    hoverDisplayer.sprite = asideHover;
                    break;

                case LevelState.CHALLENGED:
                    _buttonDisplay.sprite = challengedSprite;
                    hoverDisplayer.sprite = challengedHover;
                    break;
            }
        }


        /// <summary>
        /// Method used to highlight the level button if not locked.
        /// </summary>
        private void OnMouseEnter()
        {
            if (_hasBeenUpdated && !displayController.ObjectDisplay)
                hoverDisplayer.enabled = !_isLocked;
        }


        /// <summary>
        /// Method used to deactivate the highlight for the level button if not locked.
        /// </summary>
        private void OnMouseExit()
        {
            hoverDisplayer.enabled = false;
        }


        /// <summary>
        /// Method used to activate the level selection menu.
        /// </summary>
        private void OnMouseUp()
        {
            if (!_isLocked && _hasBeenUpdated)
                levelSelection.ActivateLevelSelectionMenu(this);
        }
    }
}