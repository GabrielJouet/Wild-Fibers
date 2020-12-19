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

    /// <summary>
    /// Shield value when protecting.
    /// </summary>
    [SerializeField]
    protected float _newShieldValue;


    protected Shield _shield;


    /// <summary>
    /// Level controller component called when needing new hatchling.
    /// </summary>
    protected LevelController _levelController;



    /// <summary>
    /// Method called for initialization.
    /// </summary>
    /// <param name="newPath">The new path used</param>
    /// <param name="newPool">The pool used when dying or finishing path</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public override void Initialize(List<Vector2> newPath, EnemyPool newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);
        _levelController = FindObjectOfType<LevelController>();

        if (_shield == null)
            _shield = new Shield(_armorMax, true, this);

        StartCoroutine(DelayHatch());
    }


    /// <summary>
    /// Coroutine used to delay the hatch of the maggot.
    /// </summary>
    /// <returns>Yield the hatchling time and cocooning time</returns>
    protected IEnumerator DelayHatch()
    {
        yield return new WaitForSeconds(_hatchingTime + Random.Range(-_hatchingTime / 20, _hatchingTime / 20));
        _animator.SetTrigger("cocoon");
        _shield.ActivateShield(_newShieldValue, _dotApplied);

        yield return new WaitForSeconds(_animator.runtimeAnimatorController.animationClips[1].length / 0.3f);
        Enemy hatchling = _levelController.RecoverPool(_hatchling).GetOneEnemy();
        hatchling.Initialize(_path, _enemyPool, _pathIndex);

        _goldGained = 0;
        Die(false);
    }
}
