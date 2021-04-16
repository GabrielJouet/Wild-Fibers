using System.Collections;
using UnityEngine;

public class Vines : Tower
{
    /// <summary>
    /// Method used to update special behavior.
    /// </summary>
    protected override void SpecialBehavior()
    {

    }

    /// <summary>
    /// Coroutine used to delay attacks.
    /// </summary>
    protected override IEnumerator SummonProjectile()
    {
        _coroutineStarted = true;

        _projectilePool.GetOneProjectile().Initialize(_towerData, null, _projectilePool, transform);
        yield return new WaitForSeconds(_towerData.TimeShots);

        _coroutineStarted = false;
    }
}