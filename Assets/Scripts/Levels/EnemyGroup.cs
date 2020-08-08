using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyGroup
{
    [Header("Parameters")]
    [SerializeField]
    private List<EnemyPattern> _enemyPatterns;
    [Space(15)]
    [SerializeField]
    private Enemy _enemyUsed;
    [SerializeField]
    [Min(0.1f)]
    private float _timeBetweenPattern;
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