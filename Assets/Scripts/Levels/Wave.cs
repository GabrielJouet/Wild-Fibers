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

    [SerializeField]
    private int _goldBonus;


    /*Getters*/
    #region
    public float GetTimeBeforeNextWave() { return _timeBeforeNextWave; }

    public EnemyGroup GetEnemyGroup(int index) { return _availableEnemyGroups[index]; }

    public int GetNumberOfEnemyGroup() { return _availableEnemyGroups.Count; }

    public int GetGoldBonus() { return _goldBonus; }
    #endregion
}