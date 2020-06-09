using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyGroup
{
    [Space(5)]
    [SerializeField]
    private List<EnemyPattern> _enemyPatterns;

    [SerializeField]
    private Enemy _enemyUsed;

    [SerializeField]
    [Min(0.1f)]
    private float _timeBetweenPattern;



    public List<EnemyPattern> GetEnemyPatterns() { return _enemyPatterns; }

    public EnemyPattern GetEnemyPattern(int index) { return _enemyPatterns[index]; }

    public int GetEnemyPatternCount() { return _enemyPatterns.Count; }

    public Enemy GetEnemyUsed() { return _enemyUsed; }

    public float GetTimeBetweenPattern() { return _timeBetweenPattern; }
}