 public class LevelSave
{
    private readonly int _livesLostCount;

    private readonly bool _isCompleted;

    private readonly bool _sideLevelCompleted;

    private readonly bool _challengeLevelCompleted;

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