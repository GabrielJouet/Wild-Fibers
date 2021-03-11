using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store enemy group data.
/// </summary>
[Serializable]
public class EnemyGroup
{
    [Header("Parameters")]

    /// <summary>
    /// Enemy groups in this pattern.
    /// </summary>
    [SerializeField]
    private List<EnemyPattern> _enemyPatterns;
    public List<EnemyPattern> Patterns { get => _enemyPatterns; }

    [Space(15)]

    /// <summary>
    /// Enemy used in this group.
    /// </summary>
    [SerializeField]
    private GameObject _enemyUsed;
    public GameObject Enemy { get => _enemyUsed; }

    [Min(0.1f)]

    /// <summary>
    /// Time between each pattern.
    /// </summary>
    [SerializeField]
    private float _timeBetweenPattern;
    public float TimeBetweenPattern { get => _timeBetweenPattern; }

    /// <summary>
    /// Path used by the enemy.
    /// </summary>
    [SerializeField]
    private int _pathIndex;
    public int Path { get => _pathIndex; }
}