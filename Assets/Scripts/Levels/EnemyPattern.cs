using System;
using UnityEngine;

/*
 * Class used to save enemy pattern data
 */
[Serializable]
public class EnemyPattern
{
    [Space(10)]
    [Min(1)]
    //Number of enemies in a pattern
    [SerializeField]
    private int _numberOfEnemies;
    public int EnemiesCount { get => _numberOfEnemies; }

    [Min(0.05f)]
    //Time between each enemy in the pattern
    [SerializeField]
    private float _timeBetweenEnemies;
    public float EnemiesTime { get => _timeBetweenEnemies; }
}