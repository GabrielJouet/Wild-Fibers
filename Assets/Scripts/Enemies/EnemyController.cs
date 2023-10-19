using System.Collections.Generic;
using Levels;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class used to handle enemies.
/// </summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// All enemies in a list format.
    /// </summary>
    [field: SerializeField, FormerlySerializedAs("_allEnemies")]
    public List<Enemy> Enemies { get; private set; }



    /// <summary>
    /// Method called when an enemy reaches the end of the path.
    /// </summary>
    /// <param name="livesLost">How many lives are lost?</param>
    public static void ReachEnd(int livesLost)
    {
        FindObjectOfType<RessourceController>().RemoveLives(livesLost);
    }


    /// <summary>
    /// Method called when an enemy dies.
    /// </summary>
    /// <param name="goldGained">How much gold is gained?</param>
    public static void Die(int goldGained)
    {
        FindObjectOfType<RessourceController>().AddGold(goldGained, true);
    }
}