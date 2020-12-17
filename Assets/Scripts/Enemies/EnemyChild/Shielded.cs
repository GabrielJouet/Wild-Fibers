using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shielded enemy, from time to time this enemy will stop and protects itself.
/// </summary>
public class Shielded : Enemy, IShieldable
{
    [Header("Special Behavior related")]

    /// <summary>
    /// Time between every shield.
    /// </summary>
    [SerializeField]
    protected float _timeBetweenShield;

    public float TimeBetweenShield { get => _timeBetweenShield; }

    /// <summary>
    /// Shield value when protecting.
    /// </summary>
    [SerializeField]
    protected float _newShieldValue;
    public float NewShieldValue { get => _newShieldValue; }


    /// <summary>
    /// Default shield value.
    /// </summary>
    protected float _baseShieldValue;
    public float BaseShieldValue { get => _baseShieldValue; }





    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newPath">New path used</param>
    /// <param name="newPool">Pool used for the current enemy</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public override void Initialize(List<Vector2> newPath, EnemyPool newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);

        _baseShieldValue = _armorMax;
        StartCoroutine(DelayShield());
    }


    /// <summary>
    /// Coroutine used to delay every shield use.
    /// </summary>
    public IEnumerator DelayShield()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenShield);
            _moving = false;
            ChangeShieldValue(_newShieldValue);
            _animator.SetTrigger("shield");
            yield return new WaitForSeconds(_animator.runtimeAnimatorController.animationClips[1].length / 0.5f);

            _moving = true;
            ChangeShieldValue(_baseShieldValue);
        }
    }


    /// <summary>
    /// Method used to change shield value.
    /// </summary>
    /// <param name="shieldValue">New shield value</param>
    public void ChangeShieldValue(float shieldValue)
    {
        if (_dotApplied)
            _armor = shieldValue - (_armorMax - _armor);
        else
            _armor = shieldValue;

        _armorMax = shieldValue;
    }
}
