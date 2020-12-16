using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Level button used to select a level.
/// </summary>
public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Components")]

    /// <summary>
    /// Loaded level.
    /// </summary>
    [SerializeField]
    private Level _levelParameters;

    /// <summary>
    /// Level selection screen.
    /// </summary>
    [SerializeField]
    private LevelSelection _levelSelection;

    /// <summary>
    /// Button display object.
    /// </summary>
    [SerializeField]
    private Image _buttonDisplay;

    /// <summary>
    /// Hover displayer object.
    /// </summary>
    [SerializeField]
    private Image _hoverDisplayer;



    [Header("Display")]

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
    /// Method used to highlight the level button if not locked.
    /// </summary>
    /// <param name="eventData">The pointer event</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverDisplayer.gameObject.SetActive(!_isLocked);
    }


    /// <summary>
    /// Method used to desactivate the highlight for the level button if not locked.
    /// </summary>
    /// <param name="eventData">The pointer event</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverDisplayer.gameObject.SetActive(false);
    }


    /// <summary>
    /// Method used to activate the level selection menu.
    /// </summary>
    public void Activate()
    {
        if(!_isLocked)
            _levelSelection.ActivateLevelSelectionMenu(_levelParameters);
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
    /// Method used to lock the level.
    /// </summary>
    public void LockLevel()
    {
        _isLocked = true;
    }


    /// <summary>
    /// Method used to set the level as complete.
    /// </summary>
    public void SetCompleted()
    {
        _buttonDisplay.sprite = _completedSprite;
        _hoverDisplayer.sprite = _finishedHover;
        _hoverDisplayer.SetNativeSize();
    }


    /// <summary>
    /// Method used to set the level as sided.
    /// </summary>
    public void SetSided()
    {
        _buttonDisplay.sprite = _sidedSprite;
        _hoverDisplayer.sprite = _asideHover;
        _hoverDisplayer.SetNativeSize();
    }


    /// <summary>
    /// Method used to set the level as challenged.
    /// </summary>
    public void SetChallenged()
    {
        _buttonDisplay.sprite = _challengedSprite;
        _hoverDisplayer.sprite = _challengedHover;
        _hoverDisplayer.SetNativeSize();
    }
}