using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store available towers.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// List of available Towers for this level.
    /// </summary>
    [SerializeField]
    private List<TowerData> _towerAvailables;
    public List<TowerData> Towers { get => _towerAvailables; }



    /// <summary>
    /// Start method called after Awake.
    /// </summary>
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}