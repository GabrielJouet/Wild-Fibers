using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsStump : Tower
{
    protected List<Vector2> _spikePositions = new List<Vector2>();

    protected Stack<ChocSpikes> _availableSpikes = new Stack<ChocSpikes>();


    /// <summary>
    /// Method used to initialize.
    /// </summary>
    /// <param name="newSlot">The parent slot</param>
    /// <param name="newRessourceController">The resource controller</param>
    /// <param name="newBackgroundSelecter">The background selecter component</param>
    /// <param name="newPool">The new projectile pool</param>
    /// <param name="newTowerPool">The new tower pool</param>
    public override void Initialize(TowerSlot newSlot, RessourceController newRessourceController, BackgroudSelecter newBackgroundSelecter, ProjectilePool newPool, TowerPool newTowerPool, TowerData newData)
    {
        _availableSpikes.Clear();
        _spikePositions.Clear();

        transform.position = newSlot.transform.position;

        _spikePositions.Add(new Vector2(transform.localPosition.x - 0.2f, transform.localPosition.y + 0.1f));
        _spikePositions.Add(new Vector2(transform.localPosition.x + 0.2f, transform.localPosition.y + 0.1f));
        _spikePositions.Add(new Vector2(transform.localPosition.x, transform.localPosition.y - 0.15f));

        _towerData = newData;
        CumulativeGold += _towerData.Price;

        _spriteRenderer.sprite = _towerData.Sprite;
        _shadowSpriteRenderer.sprite = _towerData.Shadow;

        _backgroundSelecter = newBackgroundSelecter;
        _ressourceController = newRessourceController;
        _projectilePool = newPool;
        _currentSlot = newSlot;
        _towerPool = newTowerPool;

        _transformRange.localScale *= _towerData.Range;
        _collider.localScale *= (0.9f * _towerData.Range);

        StartCoroutine(SummonSpike());
    }


    /// <summary>
    /// FixedUpdate, called 50 times a second.
    /// </summary>
    protected override void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && _availableSpikes.Count > 0)
        {
            int numberOfStrikes = _availableEnemies.Count < _availableSpikes.Count ? _availableEnemies.Count : _availableSpikes.Count;

            SortEnemies();

            List<Enemy> enemies = RecoverAvailableEnemies(numberOfStrikes);

            for (int i = 0; i < enemies.Count; i++)
                _availableSpikes.Pop().StartFollowing(enemies[i], _towerData);
        }
    }


    protected virtual IEnumerator SummonSpike()
    {
        while (true)
        {
            int numberOfSpikesNeeded = _towerData.Shots - _availableSpikes.Count;

            for (int i = 0; i < numberOfSpikesNeeded; i ++)
            {
                ChocSpikes buffer = _projectilePool.GetOneProjectile().GetComponent<ChocSpikes>();
                buffer.Initialize(_towerData, _projectilePool, _spikePositions[_availableSpikes.Count]);

                _availableSpikes.Push(buffer);
            }
            
            yield return new WaitForSeconds(_towerData.TimeShots);
        }
    }
}