using System.Collections.Generic;
using Levels;
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

    /// <summary>
    /// All enemies in a list format, accessor.
    /// </summary>
    public List<Enemy> Enemies { get => _allEnemies; }



    /// <summary>
    /// Method called when an enemy reaches the end of the path.
    /// </summary>
    /// <param name="livesLost">How many lives are lost?</param>
    public void ReachEnd(int livesLost)
    {
        FindObjectOfType<RessourceController>().RemoveLives(livesLost);
    }


    /// <summary>
    /// Method called when an enemy dies.
    /// </summary>
    /// <param name="goldGained">How much gold is gained?</param>
    public void Die(int goldGained)
    {
        FindObjectOfType<RessourceController>().AddGold(goldGained, true);
    }
}