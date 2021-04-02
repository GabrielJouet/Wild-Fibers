using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSquad", menuName = "Towers/Squad")]
public class Squad : ScriptableObject
{
    [SerializeField]
    private List<TowerData> _towers;

    public List<TowerData> Towers { get => _towers; }
}