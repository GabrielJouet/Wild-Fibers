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
    public List<EnemyGroup> EnemyGroups { get => _availableEnemyGroups; }

    /// <summary>
    /// Time between wave.
    /// </summary>
    [Min(0.1f)]
    [SerializeField]
    private float _timeBeforeNextWave;
    public float TimeWave { get => _timeBeforeNextWave; }

    /// <summary>
    /// Gold bonus by calling early wave.
    /// </summary>
    [SerializeField]
    private int _goldBonus;
    public int BonusGold { get => _goldBonus; }
}