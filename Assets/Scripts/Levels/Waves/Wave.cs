using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store wave data.
/// </summary>
[CreateAssetMenu(menuName = "Levels/Wave")]
public class Wave : ScriptableObject
{
    /// <summary>
    /// All groups in current wave.
    /// </summary>
    [SerializeField]
    private List<EnemyGroup> _availableEnemyGroups;

    /// <summary>
    /// All groups in current wave.
    /// </summary>
    public List<EnemyGroup> EnemyGroups { get => _availableEnemyGroups; }

    [Space(15)]

    /// <summary>
    /// Time between wave.
    /// </summary>
    [SerializeField, Min(0.1f)]
    private float _timeBeforeNextWave;

    /// <summary>
    /// Time between wave.
    /// </summary>
    public float TimeWave { get => _timeBeforeNextWave; }


    /// <summary>
    /// Gold bonus by calling early wave.
    /// </summary>
    [SerializeField]
    private int _goldBonus;
    
    /// <summary>
    /// Gold bonus by calling early wave.
    /// </summary>
    public int BonusGold { get => _goldBonus; }
}