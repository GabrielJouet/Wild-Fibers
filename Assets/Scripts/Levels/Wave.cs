using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Wave")]
public class Wave : ScriptableObject
{
    [SerializeField]
    private List<EnemyGroup> _availableEnemyGroups;

    [Space(10)]
    [SerializeField]
    [Min(0.1f)]
    private float _timeBeforeNextWave;


    public float GetTimeBeforeNextWave() { return _timeBeforeNextWave; }

    public List<EnemyGroup> GetEnemyGroups() { return _availableEnemyGroups; }

    public EnemyGroup GetEnemyGroup(int index) { return _availableEnemyGroups[index]; }

    public int GetNumberOfEnemyGroup() { return _availableEnemyGroups.Count; }
}