using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Wave")]
public class Wave : ScriptableObject
{
    [SerializeField]
    private List<EnemyGroup> _availableEnemyGroups;

    [Min(0.1f)]
    [SerializeField]
    private float _timeBeforeNextWave;


    /*Getters*/
    #region
    public float GetTimeBeforeNextWave() { return _timeBeforeNextWave; }

    public EnemyGroup GetEnemyGroup(int index) { return _availableEnemyGroups[index]; }

    public int GetNumberOfEnemyGroup() { return _availableEnemyGroups.Count; }
    #endregion
}