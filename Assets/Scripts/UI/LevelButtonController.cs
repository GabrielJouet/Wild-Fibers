using System.Collections.Generic;
using UnityEngine;

public class LevelButtonController : MonoBehaviour
{
    /// <summary>
    /// List of level buttons to handle.
    /// </summary>
    [SerializeField]
    private List<LevelButton> _levelButtons;



    /// <summary>
    /// Start method used to initialize.
    /// </summary>
    private void Start()
    {
        SetButtonStates();
    }


    /// <summary>
    /// Method used to set button states at startup.
    /// </summary>
    private void SetButtonStates()
    {
        List<LevelSave> levelSaves = Controller.Instance.SaveControl.SaveFile.Saves;

        for (int i = 0; i < levelSaves.Count && i < _levelButtons.Count; i++)
        {
            switch (levelSaves[i].State)
            {
                case LevelState.LOCKED:
                    _levelButtons[i].LockLevel();
                    break;

                case LevelState.UNLOCKED:
                    _levelButtons[i].UnlockLevel();
                    break;

                case LevelState.COMPLETED:
                    _levelButtons[i].SetCompleted();
                    break;

                case LevelState.SIDED:
                    _levelButtons[i].SetSided();
                    break;

                case LevelState.CHALLENGED:
                    _levelButtons[i].SetChallenged();
                    break;
            }
        }
    }
}
