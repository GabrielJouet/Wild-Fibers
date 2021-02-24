using System.Collections;
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

    /// <summary>
    /// Shield value when protecting.
    /// </summary>
    [SerializeField]
    protected float _newShieldValue;
    public float NewShieldValue { get => _newShieldValue; }

    public float BaseShieldValue { get; set; }
    public bool StopWhileShielding { get; set; }

    public float TimeBetweenSpawn { get; }

    public float SpawnTime { get; }

    public bool StopWhileSpawning { get; }

    public int NumberOfEnemiesPerSpawn { get; }

    public int PathWanted { get; } = 0;

    public List<List<Vector2>> AvailablePaths { get; set; }



    /// <summary>
    /// Method called for initialization.
    /// </summary>
    /// <param name="newPath">The new path used</param>
    /// <param name="newPool">The pool used when dying or finishing path</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public override void Initialize(List<Vector2> newPath, PoolController newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);

        BaseShieldValue = _armorMax;
        StopWhileShielding = true;

        StartCoroutine(DelaySpawn());
    }


    public void ActivateShield(float shieldValue, bool dotApplied)
    {
        Moving = !StopWhileShielding;

        if (dotApplied)
            Armor = shieldValue - (ArmorMax - Armor);
        else
            Armor = shieldValue;

        ArmorMax = shieldValue;
    }


    public void ResetShield(bool dotApplied)
    {
        if (dotApplied)
            Armor = BaseShieldValue - (ArmorMax - Armor);
        else
            Armor = BaseShieldValue;

        ArmorMax = BaseShieldValue;

        Moving = true;
    }


    /// <summary>
    /// Coroutine used to delay the hatch of the maggot.
    /// </summary>
    /// <returns>Yield the hatchling time and cocooning time</returns>
    public IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(_hatchingTime + Random.Range(-_hatchingTime / 20, _hatchingTime / 20));
        _animator.SetTrigger("cocoon");
        ActivateShield(NewShieldValue, _dotApplied);

        yield return new WaitForSeconds((_animator.runtimeAnimatorController.animationClips[1].length / 0.3f) + 0.05f);

        _poolController.RecoverEnemyPool(_hatchling).GetOneEnemy().Initialize(_path, _poolController, _pathIndex);

        _goldGained = 0;
        Die(false);
    }
}
