using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Squad controller class used to manage squads.
/// </summary>
public class SquadController : MonoBehaviour
{
    /// <summary>
    /// Towers available.
    /// </summary>
    [SerializeField]
    private List<TowerData> _towers;
    public List<TowerData> Towers { get => _towers; }
}
