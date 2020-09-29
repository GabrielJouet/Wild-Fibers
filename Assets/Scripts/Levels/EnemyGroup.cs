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

    [Space(15)]
    //Type of enemy used in the group
    [SerializeField]
    private Enemy _enemyUsed;

    [Min(0.1f)]
    //Time between each pattern
    [SerializeField]
    private float _timeBetweenPattern;

    //What path does the enemy will take?
    [SerializeField]
    private int _pathIndex;


    /*Getters*/
    #region
    public EnemyPattern GetEnemyPattern(int index) { return _enemyPatterns[index]; }

    public int GetEnemyPatternCount() { return _enemyPatterns.Count; }

    public Enemy GetEnemyUsed() { return _enemyUsed; }

    public float GetTimeBetweenPattern() { return _timeBetweenPattern; }

    public int GetPathIndex() { return _pathIndex; }
    #endregion
}