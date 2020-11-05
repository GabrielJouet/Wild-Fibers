using System.Collections;
using UnityEngine;

public class ShieldedBlusim : Enemy
{
    [Header("Special Behavior related")]
    [SerializeField]
    protected float _timeBetweenShield;

    [SerializeField]
    protected float _shieldTime;
    [SerializeField]
    protected float _newShieldValue;


    protected float _baseShieldValue;



    public override void Initialize(Path newPath, EnemyPool newPool, int pathIndex)
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

            yield return new WaitForSeconds(_shieldTime);

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
