﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicIvy : Tower
{
    /// <summary>
    /// All leaf projectiles available.
    /// </summary>
    protected Stack<ToxicLeaf> _availableLeaves = new Stack<ToxicLeaf>();


    /// <summary>
    /// Special behavior method used to improve tower method Initialize.
    /// </summary>
    protected override void SpecialBehavior()
    {
        for (int i = 0; i < _towerData.Shots; i++)
            StartCoroutine(SummonLeaf(0.25f));
    }


    /// <summary>
    /// FixedUpdate, called 50 times a second.
    /// </summary>
    protected override void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && _availableLeaves.Count > 0)
        {
            List<Enemy> enemies = RecoverAvailableEnemies(_availableLeaves.Count);

            for (int i = 0; i < enemies.Count; i++)
            {
                StartCoroutine(SummonLeaf(1));

                _availableLeaves.Pop().StartFollowing(enemies[i], _towerData);
            }
        }
    }

    /// <summary>
    /// Upgrade special behavior method used to improve tower method Upgrade.
    /// </summary>
    protected override void UpgradeSpecialBehavior()
    {
        StopAllCoroutines();

        foreach (ToxicLeaf current in _availableLeaves)
            current.StopProjectile();

        _availableLeaves.Clear();

        for (int i = 0; i < _towerData.Shots; i ++) 
            StartCoroutine(SummonLeaf(0.5f));
    }

    /// <summary>
    /// Method used to summon projectile.
    /// </summary>
    /// <param name="multiplier">Coroutine execution time multiplier</param>
    protected virtual IEnumerator SummonLeaf(float multiplier)
    {
        yield return new WaitForSeconds(_towerData.TimeShots * multiplier);

        ToxicLeaf buffer = _projectilePool.GetOneProjectile().GetComponent<ToxicLeaf>();
        buffer.Initialize(_towerData, _projectilePool, transform.localPosition);

        _availableLeaves.Push(buffer);
    }
}
