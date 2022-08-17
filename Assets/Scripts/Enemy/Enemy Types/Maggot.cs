using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maggot enemy type, will cocoon itself to revive as another enemy.
/// </summary>
public class Maggot : Enemy
{
    [Header("Hatchling related")]

    /// <summary>
    /// The newborn enemy.
    /// </summary>
    [SerializeField]
    protected Enemy _hatchling;

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



    /// <summary>
    /// Method called for initialization.
    /// </summary>
    /// <param name="newPath">The new path used</param>
    /// <param name="newPool">The pool used when dying or finishing path</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public override void Initialize(List<Vector2> newPath, PoolController newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);
        Armor = _baseShieldValue;

        StartCoroutine(DelaySpawn());
    }


    /// <summary>
    /// Coroutine used to delay the hatch of the maggot.
    /// </summary>
    /// <returns>Yield the hatchling time and cocooning time</returns>
    protected IEnumerator DelaySpawn()
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
    protected void ActivateShield(float shieldValue, bool dotApplied)
    {
        _moving = false;

        if (dotApplied)
            Armor = shieldValue - (ArmorMax - Armor);
        else
            Armor = shieldValue;
    }
}