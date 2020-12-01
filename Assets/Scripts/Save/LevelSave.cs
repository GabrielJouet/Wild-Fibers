using System;

/*
 * Class used to save data of one level
 */
[Serializable]
public class LevelSave
{
    //Number of lives lost on this level
    public int LivesLost { get; private set; }

    public LevelState State { get; private set; }



    //Constructor
    public LevelSave(int newLivesLost, LevelState newLevelState)
    {
        LivesLost = newLivesLost;
        State = newLevelState;
    }
}