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
        Damage++;
    }


    /// <summary>
    /// Level five augmentation special behavior.
    /// </summary>
    protected override void LevelFiveAugmentation()
    {
        Range *= 1.15f;
    }


    /// <summary>
    /// Method used to override special behavior.
    /// </summary>
    /// <remarks>Called after initialization and before first update</remarks>
    protected override void SpecialBehavior() 
    {
        _attack = new Attack(Damage, ArmorThrough, 0, 0, 0);
    }


    /// <summary>
    /// Coroutine used to delay attacks.
    /// </summary>
    protected override IEnumerator SummonProjectile()
    {
        _coroutineStarted = true;

        Controller.Instance.PoolController.Out(Projectile.GetComponent<PoolableObject>()).GetComponent<ShockWave>().Initialize(_attack, transform, Range, AugmentationLevel > 3);
        yield return new WaitForSeconds(TimeShots);

        _coroutineStarted = false;
    }
}