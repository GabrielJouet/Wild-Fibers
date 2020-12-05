using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class used to save enemy group data
 */
[Serializable]
public class EnemyGroup
{
    [Header("Parameters")]
    //List of enemy patterns in a group
    [SerializeField]
    private List<EnemyPattern> _enemyPatterns;
    public List<EnemyPattern> Patterns { get => _enemyPatterns; }

    [Space(15)]
    //Type of enemy used in the group
    [SerializeField]
    private Enemy _enemyUsed;
    public Enemy Enemy { get => _enemyUsed; }

    [Min(0.1f)]
    //Time between each pattern
    [SerializeField]
    private float _timeBetweenPattern;
    public float TimeBetweenPattern { get => _timeBetweenPattern; }

    //What path does the enemy will take?
    [SerializeField]
    private int _pathIndex;
    public int Path { get => _pathIndex; }
}