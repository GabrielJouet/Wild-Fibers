using UnityEngine;

/*
 * Tower collider is used to recover enemies
 */
public class TowerCollider : MonoBehaviour
{
    //Tower related
    [SerializeField]
    private Tower _parentTower;


    //Method used when an enemy collide with the range
    //
    //Parameter => enemy, the new enemy to enters tower range
    public void EnemyCollide(Enemy enemy)
    {
        _parentTower.AddEnemy(enemy);
    }


    //Method used when an enemy exits the range
    //
    //Parameter => enemy, the new enemy to exits tower range
    public void EnemyExit(Enemy enemy)
    {
        _parentTower.RemoveEnemy(enemy);
    }
}