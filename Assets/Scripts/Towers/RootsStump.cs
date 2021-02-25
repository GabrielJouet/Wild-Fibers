using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsStump : Tower
{
    /// <summary>
    /// All roots projectiles available.
    /// </summary>
    protected Stack<ChocSpikes> _availableSpikes = new Stack<ChocSpikes>();

    private int _spawnedRoot = 0;



    /// <summary>
    /// Method used to update special behavior.
    /// </summary>
    protected override void SpecialBehavior()
    {
        _availableSpikes.Clear();

        for(int i = 0; i < _towerData.Shots; i ++)
            StartCoroutine(SummonSpike());
    }


    protected override void ResellSpecialBehavior() 
    { 
        foreach(ChocSpikes current in _availableSpikes)
            current.StopProjectile();
    }


    /// <summary>
    /// FixedUpdate, called 50 times a second.
    /// </summary>
    protected override void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && _availableSpikes.Count > 0)
        {
            List<Enemy> enemies = RecoverAvailableEnemies(_availableSpikes.Count);

            for (int i = 0; i < enemies.Count; i++)
            {
                StartCoroutine(SummonSpike());

                _availableSpikes.Pop().StartFollowing(enemies[i], _towerData);
            }
        }
    }

    /// <summary>
    /// Method used to update upgrade special behavior.
    /// </summary>
    protected override void UpgradeSpecialBehavior()
    {
        StopAllCoroutines();

        if (_towerData.Shots == 4)
        {
            _availableSpikes.Peek().transform.position = GetSpikePosition(2);
            _spawnedRoot = _towerData.Shots - 1;
            StartCoroutine(SummonSpike());
        }
    }


    /// <summary>
    /// Method used to summon projectile needed.
    /// </summary>
    protected virtual IEnumerator SummonSpike()
    {
        yield return new WaitForSeconds(_towerData.TimeShots);
        ChocSpikes buffer = _projectilePool.GetOneProjectile().GetComponent<ChocSpikes>();
        buffer.Initialize(_towerData, _projectilePool, GetSpikePosition(_spawnedRoot));
        buffer.transform.parent = transform;

        if (_spawnedRoot == _towerData.Shots - 1)
            _spawnedRoot = 0;
        else
            _spawnedRoot++;

        _availableSpikes.Push(buffer);
    }


    /// <summary>
    /// Recover spike positions.
    /// </summary>
    /// <param name="index">The root index needed</param>
    /// <returns>Return the position of the root needed</returns>
    protected Vector2 GetSpikePosition(int index)
    {
        switch (index)
        {
            case 0:
                return new Vector2(transform.localPosition.x - 0.2f, transform.localPosition.y + 0.1f);

            case 1:
                return new Vector2(transform.localPosition.x + 0.2f, transform.localPosition.y + 0.1f);

            case 2:
                {
                    if (_towerData.Shots == 3)
                        return new Vector2(transform.localPosition.x, transform.localPosition.y - 0.15f);
                    else if (_towerData.Shots == 4)
                        return new Vector2(transform.localPosition.x + 0.2f, transform.localPosition.y - 0.1f);
                    break;
                }

            case 3:
                return new Vector2(transform.localPosition.x - 0.2f, transform.localPosition.y - 0.1f);

            default:
                return new Vector2();
        }

        return new Vector2();
    }
}