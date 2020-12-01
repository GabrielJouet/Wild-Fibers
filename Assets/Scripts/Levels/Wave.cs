using System.Collections.Generic;
using UnityEngine;

/*
 * Class used to save wave data
 */
[CreateAssetMenu(menuName = "Levels/Wave")]
public class Wave : ScriptableObject
{
    //List of all enemy groups in a wave
    [SerializeField]
    private List<EnemyGroup> _availableEnemyGroups;
    public List<EnemyGroup> EnemyGroups { get => _availableEnemyGroups; }

    //How much time between two waves
    [Min(0.1f)]
    [SerializeField]
    private float _timeBeforeNextWave;
    public float TimeWave { get => _timeBeforeNextWave; }

    //How much money do the player earns by calling an early wave
    [SerializeField]
    private int _goldBonus;
    public int BonusGold { get => _goldBonus; }
}