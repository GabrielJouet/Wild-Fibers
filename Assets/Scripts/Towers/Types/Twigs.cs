using UnityEngine;

public class Twigs : Tower
{
    protected override void LevelOneAugmentation()
    {
        Data.Range *= 1.1f;
    }


    protected override void LevelTwoAugmentation() 
    {
        Data.ArmorThrough += 5;
    }


    protected override void LevelFourAugmentation() 
    {
        Data.TimeShots *= 0.95f;
    }


    protected override Attack ChangeNextAttack(Enemy enemy)
    {
        Attack newAttack = new Attack(_attack);

        if (Data.AugmentationLevel > 2)
        {
            newAttack.ArmorThroughMalus = 0;
            newAttack.DotDamage = 1;
            newAttack.DotDuration = 3;
        }

        newAttack.ArmorThrough = (Data.AugmentationLevel > 3 && Random.Range(0, 100) < 10) ? 100 : _towerData.ArmorThrough;

        return newAttack;
    }
}