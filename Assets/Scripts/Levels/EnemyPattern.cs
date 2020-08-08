﻿using System;
using UnityEngine;

[Serializable]
public class EnemyPattern
{
    [Space(10)]
    [SerializeField]
    [Min(1)]
    private int _numberOfEnemies;

    [SerializeField]
    [Min(0.05f)]
    private float _timeBetweenEnemies;


    /*Getters*/
    #region
    public int GetNumberOfEnemies() { return _numberOfEnemies; }

    public float GetTimeBetweenEnemies() { return _timeBetweenEnemies; }
    #endregion
}