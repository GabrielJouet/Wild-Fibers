using UnityEngine;

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



    //Method used to activate level selection screen
    public void Activate()
    {
        _levelSelection.ActivateLevelSelectionMenu(_levelParameters);
    }
}