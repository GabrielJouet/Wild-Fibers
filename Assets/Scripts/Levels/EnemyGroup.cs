using System;
using UnityEngine;

[Serializable]
public class EnemyGroup
{
    [Space(5)]
    [SerializeField]
    [Min(1)]
    private int _enemyNumber;

    [SerializeField]
    private Enemy _enemyUsed;

    [SerializeField]
    [Min(0.1f)]
    private float _timeBetweenEnemies;


    public int GetEnemyNumber() { return _enemyNumber; }

    public Enemy GetEnemyUsed() { return _enemyUsed; }

    public float GetTimeBetweenEnemies() { return _timeBetweenEnemies; }
}
