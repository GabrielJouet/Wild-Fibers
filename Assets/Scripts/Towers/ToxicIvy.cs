using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicIvy : Tower
{
    /// <summary>
    /// All leaf projectiles available.
    /// </summary>
    protected Stack<ToxicLeaf> _availableLeafs = new Stack<ToxicLeaf>();


    /// <summary>
    /// Special behavior method used to improve tower method Initialize.
    /// </summary>
    protected override void SpecialBehavior()
    {
        _availableLeafs.Clear();

        for (int i = 0; i < _towerData.Shots; i++)
            StartCoroutine(SummonLeaf());
    }


    protected override void ResellSpecialBehavior()
    {
        foreach (ToxicLeaf current in _availableLeafs)
            current.StopProjectile();
    }


    /// <summary>
    /// FixedUpdate, called 50 times a second.
    /// </summary>
    protected override void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && _availableLeafs.Count > 0)
        {
            List<Enemy> enemies = RecoverAvailableEnemies(_availableEnemies.Count < _availableLeafs.Count ? _availableEnemies.Count : _availableLeafs.Count);

            for (int i = 0; i < enemies.Count; i++)
            {
                StartCoroutine(SummonLeaf());

                _availableLeafs.Pop().StartFollowing(enemies[i], _towerData);
            }
        }
    }

    /// <summary>
    /// Upgrade special behavior method used to improve tower method Upgrade.
    /// </summary>
    protected override void UpgradeSpecialBehavior()
    {
        StopAllCoroutines();

        for (int i = 0; i < _towerData.Shots - _availableLeafs.Count; i ++) 
            StartCoroutine(SummonLeaf());
    }

    /// <summary>
    /// Method used to summon projectile.
    /// </summary>
    protected virtual IEnumerator SummonLeaf()
    {
        yield return new WaitForSeconds(_towerData.TimeShots);
        ToxicLeaf buffer = _projectilePool.GetOneProjectile().GetComponent<ToxicLeaf>();
        buffer.Initialize(_towerData, _projectilePool, transform.position);
        buffer.transform.parent = transform;

        _availableLeafs.Push(buffer);
    }
}
