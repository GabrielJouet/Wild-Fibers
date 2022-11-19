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
    public List<Enemy> Enemies { get => _allEnemies; }
    [SerializeField]
    private List<Enemy> _allEnemies;



    /// <summary>
    /// 
    /// </summary>
    /// <param name="livesLost"></param>
    public void ReachEnd(int livesLost)
    {
        FindObjectOfType<RessourceController>().RemoveLives(livesLost);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="goldGained"></param>
    public void Die(int goldGained)
    {
        FindObjectOfType<RessourceController>().AddGold(goldGained, true);
    }
}