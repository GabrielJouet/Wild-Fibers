using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private LevelParameters _levelParameters;

    [SerializeField]
    private LevelSelection _levelSelection;

    private void OnMouseDown()
    {
        _levelSelection.ActivateLevelSelectionMenu(_levelParameters);
    }
}