using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shielded enemy, from time to time this enemy will stop and protects itself.
/// </summary>
public class Shielded : Enemy
{
    [Header("Shield related")]

    /// <summary>
    /// Time between every shield.
    /// </summary>
    [SerializeField]
    protected float _timeBetweenShield;

    /// <summary>
    /// Shield value when protecting.
    /// </summary>
    [SerializeField]
    protected float _newShieldValue;

    [SerializeField]
    protected bool _stopWhileShielding;


    protected Shield _shield;


    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newPath">New path used</param>
    /// <param name="newPool">Pool used for the current enemy</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public override void Initialize(List<Vector2> newPath, PoolController newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);

        if(_shield == null)
            _shield = new Shield(_armorMax, _stopWhileShielding, this);

        StartCoroutine(DelayShield());
    }


    /// <summary>
    /// Coroutine used to delay every shield use.
    /// </summary>
    public IEnumerator DelayShield()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenShield + Random.Range(-_timeBetweenShield / 20, _timeBetweenShield / 20));

            _shield.ActivateShield(_newShieldValue, _dotApplied);
            _animator.SetTrigger("shield");
            yield return new WaitForSeconds(_animator.runtimeAnimatorController.animationClips[1].length / 0.5f);

            _shield.ResetShield(_dotApplied);
        }
    }
}
