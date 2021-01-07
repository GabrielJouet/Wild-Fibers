using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    [SerializeField]
    private List<TowerData> _towers;
    public List<TowerData> Towers { get => _towers; }
}
