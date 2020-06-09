﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Level", fileName = "NewLevel")]
public class Level : ScriptableObject
{
    [SerializeField]
    private List<Wave> _availableWaves;



    public List<Wave> GetWaves() { return _availableWaves; }

    public Wave GetWave(int index) { return _availableWaves[index]; }

    public int GetWaveCount() { return _availableWaves.Count; }
}