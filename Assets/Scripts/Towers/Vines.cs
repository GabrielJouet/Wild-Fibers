using System.Collections;
using UnityEngine;

public class Vines : Tower
{
    /// <summary>
    /// FixedUpdate, called 50 times a second.
    /// </summary>
    protected override void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
            StartCoroutine(SummonProjectile());
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
