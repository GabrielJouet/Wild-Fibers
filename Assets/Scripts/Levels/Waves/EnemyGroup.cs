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

    /// <summary>
    /// Enemy groups in this pattern.
    /// </summary>
    public List<EnemyPattern> Patterns { get => _enemyPatterns; }

    [Space(15)]

    /// <summary>
    /// Enemy used in this group.
    /// </summary>
    [SerializeField]
    private GameObject _enemyUsed;

    /// <summary>
    /// Enemy used in this group.
    /// </summary>
    public GameObject Enemy { get => _enemyUsed; }


    /// <summary>
    /// Time between each pattern.
    /// </summary>
    [SerializeField, Min(0.1f)]
    private float _timeBetweenPattern;

    /// <summary>
    /// Time between each pattern.
    /// </summary>
    public float TimeBetweenPattern { get => _timeBetweenPattern; }


    /// <summary>
    /// Path used by the enemy.
    /// </summary>
    [SerializeField]
    private int _pathIndex;

    /// <summary>
    /// Path used by the enemy.
    /// </summary>
    public int Path { get => _pathIndex; }
}