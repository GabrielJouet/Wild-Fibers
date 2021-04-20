using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shielded enemy, from time to time this enemy will stop and protects itself.
/// </summary>
public class Shielded : Enemy, IShieldable
{
    [Header("Shield related")]

    /// <summary>
    /// Time between every shield.
    /// </summary>
    [SerializeField]
    protected float _timeBetweenShield;

    /// <summary>
    /// Stop while shielding?
    /// </summary>
    [SerializeField]
    protected bool _stopWhileShielding;
    public bool StopWhileShielding { get => _stopWhileShielding; set => _stopWhileShielding = value; }

    /// <summary>
    /// Shield value when not shielding.
    /// </summary>
    [SerializeField]
    protected int _baseShieldValue;
    public int BaseShieldValue { get => _baseShieldValue; set => _baseShieldValue = value; }



    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newPath">New path used</param>
    /// <param name="newPool">Pool used for the current enemy</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public override void Initialize(List<Vector2> newPath, PoolController newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);
        Armor = BaseShieldValue;
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

            ActivateShield(ArmorMax, _dots.Count > 0);
            _animator.SetTrigger("shield");
            yield return new WaitForSeconds(_animator.runtimeAnimatorController.animationClips[1].length / 0.5f);

            ResetShield(_dots.Count > 0);
        }
    }


    /// <summary>
    /// Method used to change shield value.
    /// </summary>
    /// <param name="shieldValue">New shield value</param>
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
    public void ResetShield(bool dotApplied)
    {
        if (dotApplied)
            Armor = BaseShieldValue - (ArmorMax - Armor);
        else
            Armor = BaseShieldValue;

        Moving = true;
    }
}
