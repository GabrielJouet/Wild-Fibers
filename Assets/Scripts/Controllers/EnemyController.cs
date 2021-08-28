using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle enemies.
/// </summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// All enemies in a list format.
    /// </summary>
    [SerializeField]
    private List<Enemy> _allEnemies;
    public List<Enemy> Enemies { get => _allEnemies; }
}
