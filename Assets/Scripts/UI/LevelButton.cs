using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Level Button is used in level selection scene where you can interact with them to select a level
 */
public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Components")]
    //Loaded level parameters
    [SerializeField]
    private Level _levelParameters;

    //Level selection object used to show level selection screen
    [SerializeField]
    private LevelSelection _levelSelection;

    
    [Header("Display")]
    //Sprite of the button
    [SerializeField]
    private Image _buttonDisplay;

    //Desactivated sprite if not unlocked
    [SerializeField]
    private Sprite _desactivatedSprite;

    //Completed sprite
    [SerializeField]
    private Sprite _completedSprite;

    [SerializeField]
    private Sprite _sidedSprite;

    //Challenged sprite if level is completed
    [SerializeField]
    private Sprite _challengedSprite;


    [Header("Selection")]
    [SerializeField]
    private Image _hoverDisplayer;

    [SerializeField]
    private Sprite _unlockedHover;

    [SerializeField]
    private Sprite _finishedHover;

    [SerializeField]
    private Sprite _asideHover;

    [SerializeField]
    private Sprite _challengedHover;
    

    private bool _isLocked;


    private Sprite _loadedHover;


    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverDisplayer.gameObject.SetActive(true);
        _hoverDisplayer.sprite = _loadedHover;
        _hoverDisplayer.SetNativeSize();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverDisplayer.gameObject.SetActive(false);
    }


    //Method used to activate level selection screen
    public void Activate()
    {
        if(!_isLocked)
            _levelSelection.ActivateLevelSelectionMenu(_levelParameters);
    }


    //Method used when the level is not yet unlocked
    public void LockLevel()
    {
        _isLocked = true;
        _buttonDisplay.sprite = _desactivatedSprite;
        _loadedHover = null;
    }


    //Method used when the level is not yet unlocked
    public void UnlockLevel()
    {
        _loadedHover = _unlockedHover;
    }


    //Method used when the level is completed
    public void SetCompleted()
    {
        _buttonDisplay.sprite = _completedSprite;
        _loadedHover = _finishedHover;
    }


    //Method used when the side level is completed
    public void SetSided()
    {
        _buttonDisplay.sprite = _sidedSprite;
        _loadedHover = _asideHover;
    }


    //Method used when the level is challenged
    public void SetChallenged()
    {
        _buttonDisplay.sprite = _challengedSprite;
        _loadedHover = _challengedHover;
    }
}