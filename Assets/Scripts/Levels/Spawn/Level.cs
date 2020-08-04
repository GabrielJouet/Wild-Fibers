﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Level", fileName = "NewLevel")]
public class Level : ScriptableObject
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private int _number;

    [SerializeField]
    private Sprite _picture;

    [SerializeField]
    private string _description;

    [SerializeField]
    private string _playSceneName;


    [SerializeField]
    private List<Wave> _availableWaves;



    public List<Wave> GetWaves() { return _availableWaves; }

    public Wave GetWave(int index) { return _availableWaves[index]; }

    public int GetWaveCount() { return _availableWaves.Count; }

    public string GetName() { return _name; }

    public int GetNumber() { return _number; }

    public Sprite GetPicture() { return _picture; }

    public string GetDescription() { return _description; }

    public string GetPlaySceneName() { return _playSceneName; }
}