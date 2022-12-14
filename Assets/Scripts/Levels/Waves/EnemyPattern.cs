using System;
using UnityEngine;

/// <summary>
/// Class used to store enemy information.
/// </summary>
[Serializable]
public class EnemyPattern
{
    [Space(10)]

    /// <summary>
    /// Number of enemies in this group.
    /// </summary>
    [Min(1)]
    [SerializeField]
    private int _numberOfEnemies;

    /// <summary>
    /// Number of enemies in this group.
    /// </summary>
    public int EnemiesCount { get => _numberOfEnemies; }


    /// <summary>
    /// Time between each enemy.
    /// </summary>
    [Min(0.05f)]
    [SerializeField]
    private float _timeBetweenEnemies;

    /// <summary>
    /// Time between each enemy.
    /// </summary>
    public float EnemiesTime { get => _timeBetweenEnemies; }
}