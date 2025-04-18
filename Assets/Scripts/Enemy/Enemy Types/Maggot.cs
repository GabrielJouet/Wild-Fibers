﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maggot enemy type, will cocoon itself to revive as another enemy.
/// </summary>
public class Maggot : Enemy, IShieldable, ISpawnable
{
    [Header("Hatchling related")]

    /// <summary>
    /// The newborn enemy.
    /// </summary>
    [SerializeField]
    protected Enemy _hatchling;
    public Enemy Spawnling { get => _hatchling; }

    /// <summary>
    /// Time before the maggot cocoon itself.
    /// </summary>
    [SerializeField]
    protected float _hatchingTime;


    [Header("Shield related")]
    
    /// <summary>
    /// Shield value when not protecting.
    /// </summary>
    [SerializeField]
    protected int _baseShieldValue;
    public int BaseShieldValue { get => _baseShieldValue; set => _baseShieldValue = value; }



    #region Unused
    public bool StopWhileShielding { get; set; }

    public float TimeBetweenSpawn { get; }

    public float SpawnTime { get; }

    public bool StopWhileSpawning { get; }

    public int NumberOfEnemiesPerSpawn { get; }
    #endregion



    /// <summary>
    /// Method called for initialization.
    /// </summary>
    /// <param name="newPath">The new path used</param>
    /// <param name="newPool">The pool used when dying or finishing path</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public override void Initialize(List<Vector2> newPath, PoolController newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);

        StopWhileShielding = true;
        Armor = BaseShieldValue;

        StartCoroutine(DelaySpawn());
    }


    /// <summary>
    /// Coroutine used to delay the hatch of the maggot.
    /// </summary>
    /// <returns>Yield the hatchling time and cocooning time</returns>
    public IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(_hatchingTime + Random.Range(-_hatchingTime / 20, _hatchingTime / 20));
        _animator.SetTrigger("cocoon");
        ActivateShield(_armorMax, _dots.Count > 0);

        yield return new WaitForSeconds((_animator.runtimeAnimatorController.animationClips[1].length / 0.3f) + 0.05f);

        _poolController.RecoverEnemyPool(_hatchling).GetOneEnemy().Initialize(_path, _poolController, _pathIndex);

        _goldGained = 0;
        Die(false);
    }


    /// <summary>
    /// Method used to change shield value.
    /// </summary>
    /// <param name="shieldValue">New shield value</param>
    /// <param name="dotApplied">Does a dot is applied to the shield?</param>
    public void ActivateShield(float shieldValue, bool dotApplied)
    {
        Moving = !StopWhileShielding;

        if (dotApplied)
            Armor = shieldValue - (ArmorMax - Armor);
        else
            Armor = shieldValue;
    }


    /// <summary>
    /// Default shield value.
    /// </summary>
    /// <param name="dotApplied">Does a dot is applied to the shield?</param>
    public void ResetShield(bool dotApplied)
    {
        if (dotApplied)
            Armor = BaseShieldValue - (ArmorMax - Armor);
        else
            Armor = BaseShieldValue;

        Moving = true;
    }
}
