using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Squad controller class used to manage squads.
/// </summary>
public class SquadController : MonoBehaviour
{
    /// <summary>
    /// Squads available.
    /// </summary>
    [SerializeField]
    private List<Squad> _squads;
    public List<Squad> Squads { get => _squads; }
}
