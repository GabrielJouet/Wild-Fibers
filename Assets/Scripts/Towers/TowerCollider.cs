using UnityEngine;

public class TowerCollider : MonoBehaviour
{
    [SerializeField]
    private Tower _parentTower;


    //Method used when an enemy collide with the range
    public void EnemyCollide(Enemy enemy)
    {
        _parentTower.AddEnemy(enemy);
    }


    //Method used when an enemy exits the range
    public void EnemyExit(Enemy enemy)
    {
        _parentTower.RemoveEnemy(enemy);
    }
}