using System.Collections;
using UnityEngine;

/// <summary>
/// Class used by vines-like tower for behavior purpose.
/// </summary>
public class Vines : Tower
{
    /// <summary>
    /// Level one augmentation special behavior.
    /// </summary>
    protected override void LevelOneAugmentation()
    {
        Data.Damage++;
    }


    /// <summary>
    /// Level five augmentation special behavior.
    /// </summary>
    protected override void LevelFiveAugmentation()
    {
        Data.Range *= 1.15f;
    }


    /// <summary>
    /// Method used to override special behavior.
    /// </summary>
    /// <remarks>Called after initialization and before first update</remarks>
    protected override void SpecialBehavior() 
    {
        _attack = new Attack(Data.Damage, Data.ArmorThrough, 0, 0, 0);
    }


    /// <summary>
    /// Coroutine used to delay attacks.
    /// </summary>
    protected override IEnumerator SummonProjectile()
    {
        _coroutineStarted = true;

        _projectilePool.GetOneProjectile().GetComponent<ShockWave>().Initialize(_attack, _projectilePool, transform, Data.Range, Data.AugmentationLevel > 3);
        yield return new WaitForSeconds(_towerData.TimeShots);

        _coroutineStarted = false;
    }
}