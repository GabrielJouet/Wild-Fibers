using System.Collections;
using UnityEngine;

public class Vines : Tower
{
    protected override void LevelOneAugmentation()
    {
        Data.Damage++;
    }


    protected override void LevelFiveAugmentation()
    {
        Data.Range *= 1.15f;
    }


    protected override Attack ChangeNextAttack(Enemy enemy)
    {
        Attack newAttack = new Attack(_attack);

        if (Data.AugmentationLevel > 3 && enemy.Flying)
            newAttack.Damage += 2;

        return newAttack;
    }


    /// <summary>
    /// Coroutine used to delay attacks.
    /// </summary>
    protected override IEnumerator SummonProjectile()
    {
        _coroutineStarted = true;

        //TO FIX, SHOULD NOT WORK LIKE THIS
        _projectilePool.GetOneProjectile().GetComponent<ShockWave>().Initialize(new Attack(Data.Damage, Data.ArmorThrough, 0, 0, 0), _projectilePool, transform, Data.Range);
        yield return new WaitForSeconds(_towerData.TimeShots);

        _coroutineStarted = false;
    }
}