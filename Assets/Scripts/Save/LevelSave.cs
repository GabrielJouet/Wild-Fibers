using System;

/*
 * Class used to save data of one level
 */
[Serializable]
public class LevelSave
{
    //Number of lives lost on this level
    private readonly int _livesLostCount;

    //Does the level is finished?
    private readonly bool _isCompleted;

    //Does the side level is finished?
    private readonly bool _sideLevelCompleted;

    //Does the challenge level is finished?
    private readonly bool _challengeLevelCompleted;

    //Does this level is unlocked?
    private readonly bool _isUnlocked;



    //Constructor
    public LevelSave(int newLivesLost, bool isCompleted, bool sideLevel, bool challengeLevel, bool isUnlocked)
    {
        _livesLostCount = newLivesLost;
        _isCompleted = isCompleted;
        _sideLevelCompleted = sideLevel;
        _challengeLevelCompleted = challengeLevel;
        _isUnlocked = isUnlocked;
    }



    //Getters
    public int GetLivesLostCount() { return _livesLostCount; }

    public bool GetIsCompleted() { return _isCompleted; }

    public bool GetSideLevelCompleted() { return _sideLevelCompleted; }

    public bool GetChallengeLevelCompleted() { return _challengeLevelCompleted; }

    public bool GetIsUnlocked() { return _isUnlocked; }
}