using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsStump : Tower
{
    /// <summary>
    /// All roots projectiles available.
    /// </summary>
    protected List<ChocSpikes> _availableSpikes = new List<ChocSpikes>();

    protected int _rootsIndex = 0;

    /// <summary>
    /// Method used to update special behavior.
    /// </summary>
    protected override void SpecialBehavior()
    {
        for (int i = 0; i < _towerData.Shots; i ++)
            StartCoroutine(SummonSpike(0.25f));
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
                StartCoroutine(SummonSpike(1));

                _availableSpikes[0].StartFollowing(enemies[i], _towerData);
                _availableSpikes.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// Method used to update upgrade special behavior.
    /// </summary>
    protected override void UpgradeSpecialBehavior()
    {
        StopAllCoroutines();

        foreach (ChocSpikes current in _availableSpikes)
            current.StopProjectile();

        _availableSpikes.Clear();

        for (int i = 0; i < _towerData.Shots; i++)
            StartCoroutine(SummonSpike(0.5f));
    }


    /// <summary>
    /// Method used to summon projectile needed.
    /// </summary>
    /// <param name="multiplier">Coroutine execution time multiplier</param>
    protected virtual IEnumerator SummonSpike(float multiplier)
    {
        yield return new WaitForSeconds(_towerData.TimeShots * multiplier);

        ChocSpikes buffer = _projectilePool.GetOneProjectile().GetComponent<ChocSpikes>();
        buffer.Initialize(_towerData, _projectilePool, GetSpikePosition(_rootsIndex));

        _rootsIndex = _rootsIndex == _towerData.Shots - 1? 0 : _rootsIndex + 1 ;

        _availableSpikes.Add(buffer);
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