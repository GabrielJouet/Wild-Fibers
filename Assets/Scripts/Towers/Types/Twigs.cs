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
        Range *= 1.1f;
    }


    /// <summary>
    /// Level two augmentation special behavior.
    /// </summary>
    protected override void LevelTwoAugmentation() 
    {
        ArmorThrough += 5;
    }


    /// <summary>
    /// Level four augmentation special behavior.
    /// </summary>
    protected override void LevelFourAugmentation() 
    {
        TimeShots *= 0.95f;
    }


    /// <summary>
    /// Method called before an attack occurs to apply modifications on the current attack.
    /// </summary>
    /// <param name="enemy">The enemy targeted</param>
    /// <returns>The new attack with new parameters</returns>
    protected override Attack ChangeNextAttack(Enemy enemy)
    {
        Attack newAttack = new Attack(_attack);

        if (AugmentationLevel > 2)
        {
            newAttack.ArmorThroughMalus = 0;
            newAttack.DotDamage = 1;
            newAttack.DotDuration = 1.5f;
        }

        newAttack.ArmorThrough = (AugmentationLevel > 3 && Random.Range(0, 100) < 10) ? 100 : ArmorThrough;

        return newAttack;
    }
}