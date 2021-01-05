using UnityEngine;

/// <summary>
/// Tower collider, used to recover enemies in range.
/// </summary>
public class TowerCollider : MonoBehaviour
{
    /// <summary>
    /// Parent tower to notify.
    /// </summary>
    private GameObject _parentTower;



    private void Awake()
    {
        _parentTower = transform.parent.gameObject;
    }


    /// <summary>
    /// Method called when an enemy enters the range.
    /// </summary>
    /// <param name="enemy">The collided enemy</param>
    public void EnemyCollide(Enemy enemy)
    {
        _parentTower.GetComponent<Tower>().AddEnemy(enemy);
    }


    /// <summary>
    /// Method called when an enemy exited the range.
    /// </summary>
    /// <param name="enemy">The collided enemy</param>
    public void EnemyExit(Enemy enemy)
    {
        _parentTower.GetComponent<Tower>().RemoveEnemy(enemy);
    }
}