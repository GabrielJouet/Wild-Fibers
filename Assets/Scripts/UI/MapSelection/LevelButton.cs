using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private Level _levelParameters;

    [SerializeField]
    private LevelSelection _levelSelection;

    private void OnMouseDown()
    {
        _levelSelection.ActivateLevelSelectionMenu(_levelParameters);
    }
}