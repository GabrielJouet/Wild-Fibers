using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielded : Enemy
{
    [Header("Special Behavior related")]
    [SerializeField]
    protected float _timeBetweenShield;

    [SerializeField]
    protected float _newShieldValue;


    protected float _baseShieldValue;



    public override void Initialize(List<Vector2> newPath, EnemyPool newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);

        _baseShieldValue = _armorMax;
        StartCoroutine(DelayShield());
    }



    protected IEnumerator DelayShield()
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


    protected void ChangeShieldValue(float shieldValue)
    {
        if (_dotApplied)
            _armor = shieldValue - (_armorMax - _armor);
        else
            _armor = shieldValue;

        _armorMax = shieldValue;
    }
}
