using System;

/*
 * Class used to save data of one level
 */
[Serializable]
public class LevelSave
{
    //Number of lives lost on this level
    private readonly int _livesLostCount;

    private readonly LevelState _levelState;



    //Constructor
    public LevelSave(int newLivesLost, LevelState newLevelState)
    {
        _livesLostCount = newLivesLost;
        _levelState = newLevelState;
    }



    //Getters
    public int GetLivesLostCount() { return _livesLostCount; }

    public LevelState GetState() { return _levelState; }
}