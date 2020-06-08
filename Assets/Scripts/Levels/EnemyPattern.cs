using System;
using UnityEngine;

[Serializable]
public class EnemyPattern
{
    [Space(2)]
    [SerializeField]
    [Min(1)]
    private int _numberOfEnemies;

    [SerializeField]
    [Min(0.05f)]
    private float _timeBetweenEnemies;


    public int GetNumberOfEnemies() { return _numberOfEnemies; }

    public float GetTimeBetweenEnemies() { return _timeBetweenEnemies; }
}