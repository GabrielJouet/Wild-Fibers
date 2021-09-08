using UnityEngine;

/// <summary>
/// Tower collider, used to recover enemies in range.
/// </summary>
public class TowerCollider : MonoBehaviour
{
    /// <summary>
    /// Parent tower to notify.
    /// </summary>
    public Tower ParentTower { get; set; }


    /// <summary>
    /// Method called when an enemy enters the range.
    /// </summary>
    /// <param name="enemy">The collided enemy</param>
    public void EnemyCollide(Enemy enemy)
    {
        ParentTower.AddEnemy(enemy);
    }


    /// <summary>
    /// Method called when an enemy exited the range.
    /// </summary>
    /// <param name="enemy">The collided enemy</param>
    public void EnemyExit(Enemy enemy)
    {
        ParentTower.RemoveEnemy(enemy);
    }
}