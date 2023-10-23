using System.Collections;
using System.Collections.Generic;
using Enemies;
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
    protected GameObject _hatchling;

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
    public override void Initialize(List<Vector2> newPath, int pathIndex, Spawner spawner)
    {
        base.Initialize(newPath, pathIndex, spawner);
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

        _moving = false;

        if (_dots.Count > 0)
            Armor = _armorMax - (ArmorMax - Armor);
        else
            Armor = _armorMax;

        yield return new WaitForSeconds((_animator.runtimeAnimatorController.animationClips[1].length / 0.3f) + 0.05f);

        Instantiate(_hatchling).GetComponent<Enemy>().Initialize(_path, _pathIndex, _spawner);

        _goldGained = 0;
        Die(false);
    }
}