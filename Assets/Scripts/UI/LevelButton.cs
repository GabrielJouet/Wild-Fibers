using UnityEngine;
using UnityEngine.UI;

/*
 * Level Button is used in level selection scene where you can interact with them to select a level
 */
public class LevelButton : MonoBehaviour
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

    //Challenged sprite if level is completed
    [SerializeField]
    private Sprite _challengedSprite;


    private bool _isLocked;


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
    }


    //Method used when the level is completed
    public void SetCompleted()
    {
        _buttonDisplay.sprite = _completedSprite;
    }


    //Method used when the level is challenged
    public void SetChallenged()
    {
        _buttonDisplay.sprite = _challengedSprite;
    }
}