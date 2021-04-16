public class Twigs : Tower
{
    protected override void LevelOneAugmentation()
    {
        _towerData.Range *= 1.1f;
    }


    protected override void LevelTwoAugmentation() 
    {
        _towerData.ArmorThrough += 5;
    }


    protected override void LevelThreeAugmentation() 
    {
        _towerData.Dot = 1;
        _towerData.DotDuration = 3;
        _towerData.ArmorThroughMalus = 0;
    }


    protected override void LevelFourAugmentation() 
    {
        _towerData.TimeShots *= 0.95f;
    }


    protected override void LevelFiveAugmentation()
    {

    }
}