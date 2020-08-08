using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Level _levelParameters;
    [SerializeField]
    private LevelSelection _levelSelection;


    public void Activate()
    {
        _levelSelection.ActivateLevelSelectionMenu(_levelParameters);
    }
}