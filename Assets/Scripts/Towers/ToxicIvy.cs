using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicIvy : Tower
{
    protected Stack<ToxicLeaf> _availableLeafs = new Stack<ToxicLeaf>();


    protected override void SpecialBehavior()
    {
        _availableLeafs.Clear();

        StartCoroutine(SummonLeaf());
    }


    /// <summary>
    /// FixedUpdate, called 50 times a second.
    /// </summary>
    protected override void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && _availableLeafs.Count > 0)
        {
            StopAllCoroutines();
            SortEnemies();

            List<Enemy> enemies = RecoverRandomEnemies(_availableEnemies.Count < _availableLeafs.Count ? _availableEnemies.Count : _availableLeafs.Count);

            for (int i = 0; i < enemies.Count; i++)
                _availableLeafs.Pop().StartFollowing(enemies[i], _towerData);

            StartCoroutine(SummonLeaf());
        }
    }

    protected override void UpgradeSpecialBehavior()
    {
        if ( _availableLeafs.Count > 0)
            SpawnLeaf(_towerData.Shots - _availableLeafs.Count);
    }


    protected virtual IEnumerator SummonLeaf()
    {
        while (true)
        {
            yield return new WaitForSeconds(_towerData.TimeShots);
            SpawnLeaf(_towerData.Shots - _availableLeafs.Count);
        }
    }


    protected void SpawnLeaf(int number)
    {
        for (int i = 0; i < number; i++)
        {
            ToxicLeaf buffer = _projectilePool.GetOneProjectile().GetComponent<ToxicLeaf>();
            buffer.Initialize(_towerData, _projectilePool, transform.position);
            buffer.transform.parent = transform;

            _availableLeafs.Push(buffer);
        }
    }
}
