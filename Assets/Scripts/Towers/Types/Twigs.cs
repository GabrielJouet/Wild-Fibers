using UnityEngine;

/// <summary>
/// Class used by twigs-like towers for behavior modification purposes.
/// </summary>
public class Twigs : Tower
{
    /// <summary>
    /// Level one augmentation special behavior.
    /// </summary>
    protected override void LevelOneAugmentation()
    {
        Data.Range *= 1.1f;
    }


    /// <summary>
    /// Level two augmentation special behavior.
    /// </summary>
    protected override void LevelTwoAugmentation() 
    {
        Data.ArmorThrough += 5;
    }


    /// <summary>
    /// Level four augmentation special behavior.
    /// </summary>
    protected override void LevelFourAugmentation() 
    {
        Data.TimeShots *= 0.95f;
    }


    /// <summary>
    /// Method called before an attack occurs to apply modifications on the current attack.
    /// </summary>
    /// <param name="enemy">The enemy targeted</param>
    /// <returns>The new attack with new parameters</returns>
    protected override Attack ChangeNextAttack(Enemy enemy)
    {
        Attack newAttack = new Attack(_attack);

        Debug.Log(Data.AugmentationLevel);
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