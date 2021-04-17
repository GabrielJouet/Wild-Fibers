using System.Collections;
using UnityEngine;

public class Vines : Tower
{
    protected override void LevelOneAugmentation()
    {
        _towerData.Damage++;
    }


    protected override void LevelFourAugmentation()
    {

    }


    protected override void LevelFiveAugmentation()
    {
        _towerData.Range *= 1.15f;
    }

    /// <summary>
    /// Coroutine used to delay attacks.
    /// </summary>
    protected override IEnumerator SummonProjectile()
    {
        _coroutineStarted = true;

        _projectilePool.GetOneProjectile().GetComponent<ShockWave>().Initialize(_towerData, null, _projectilePool, transform);
        yield return new WaitForSeconds(_towerData.TimeShots);

        _coroutineStarted = false;
    }
}