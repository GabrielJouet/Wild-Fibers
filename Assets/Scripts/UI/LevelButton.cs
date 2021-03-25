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

    [SerializeField]
    private LevelData _levelData;
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



    private void Awake()
    {
        _buttonDisplay = GetComponent<SpriteRenderer>();
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


    /// <summary>
    /// Method used to lock the level.
    /// </summary>
    public void LockLevel()
    {
        _isLocked = true;
    }


    /// <summary>
    /// Method used to unlock the level.
    /// </summary>
    public void UnlockLevel()
    {
        _hoverDisplayer.sprite = _unlockedHover;
        _buttonDisplay.sprite = _activatedSprite;
    }


    /// <summary>
    /// Method used to set the level as complete.
    /// </summary>
    public void SetCompleted()
    {
        _buttonDisplay.sprite = _completedSprite;
        _hoverDisplayer.sprite = _finishedHover;
    }


    /// <summary>
    /// Method used to set the level as sided.
    /// </summary>
    public void SetSided()
    {
        _buttonDisplay.sprite = _sidedSprite;
        _hoverDisplayer.sprite = _asideHover;
    }


    /// <summary>
    /// Method used to set the level as challenged.
    /// </summary>
    public void SetChallenged()
    {
        _buttonDisplay.sprite = _challengedSprite;
        _hoverDisplayer.sprite = _challengedHover;
    }
}