using System.Collections;
using Levels;
using Miscellanious.Enums;
using UnityEngine;

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
    private LevelSelection _levelSelection;

    /// <summary>
    /// Hover displayer object.
    /// </summary>
    [SerializeField]
    private SpriteRenderer _hoverDisplayer;

    /// <summary>
    /// Level data associated with this button.
    /// </summary>
    [SerializeField]
    private LevelData _levelData;

    /// <summary>
    /// Level data associated with this button.
    /// </summary>
    public LevelData LevelData { get => _levelData; }


    [Header("Sprites")]

    /// <summary>
    /// Activated sprite.
    /// </summary>
    [SerializeField]
    private Sprite _activatedSprite;

    /// <summary>
    /// Completed sprite.
    /// </summary>
    [SerializeField]
    private Sprite _completedSprite;

    /// <summary>
    /// Sided sprite.
    /// </summary>
    [SerializeField]
    private Sprite _sidedSprite;

    /// <summary>
    /// Challenged sprite.
    /// </summary>
    [SerializeField]
    private Sprite _challengedSprite;


    [Header("Selection")]

    /// <summary>
    /// Unlocked hover sprite.
    /// </summary>
    [SerializeField]
    private Sprite _unlockedHover;

    /// <summary>
    /// Finished hover sprite.
    /// </summary>
    [SerializeField]
    private Sprite _finishedHover;

    /// <summary>
    /// Asided hover sprite.
    /// </summary>
    [SerializeField]
    private Sprite _asideHover;

    /// <summary>
    /// Challenged hover sprite.
    /// </summary>
    [SerializeField]
    private Sprite _challengedHover;


    /// <summary>
    /// Does the button is locked?
    /// </summary>
    private bool _isLocked = false;

    /// <summary>
    /// Button display object.
    /// </summary>
    private SpriteRenderer _buttonDisplay;



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

        switch(Controller.Instance.SaveController.SaveFile.CurrentSave[index].State)
        {
            case LevelState.LOCKED:
                _isLocked = true;
                break;

            case LevelState.UNLOCKED:
                _hoverDisplayer.sprite = _unlockedHover;
                _buttonDisplay.sprite = _activatedSprite;
                break;

            case LevelState.COMPLETED:
                _buttonDisplay.sprite = _completedSprite;
                _hoverDisplayer.sprite = _finishedHover;
                break;

            case LevelState.SIDED:
                _buttonDisplay.sprite = _sidedSprite;
                _hoverDisplayer.sprite = _asideHover;
                break;

            case LevelState.CHALLENGED:
                _buttonDisplay.sprite = _challengedSprite;
                _hoverDisplayer.sprite = _challengedHover;
                break;
        }
    }


    /// <summary>
    /// Method used to highlight the level button if not locked.
    /// </summary>
    private void OnMouseEnter()
    {
        _hoverDisplayer.enabled = !_isLocked;
    }


    /// <summary>
    /// Method used to desactivate the highlight for the level button if not locked.
    /// </summary>
    private void OnMouseExit()
    {
        _hoverDisplayer.enabled = false;
    }


    /// <summary>
    /// Method used to activate the level selection menu.
    /// </summary>
    private void OnMouseUp()
    {
        if (!_isLocked)
            _levelSelection.ActivateLevelSelectionMenu(this);
    }
}