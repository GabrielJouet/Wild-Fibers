using UnityEngine;

public class TowerCollider : MonoBehaviour
{
    [SerializeField]
    private Tower _parentTower;


    public void EnemyCollide(Enemy enemy)
    {
        _parentTower.AddEnemy(enemy);
    }

    public void EnemyExit(Enemy enemy)
    {
        _parentTower.RemoveEnemy(enemy);
    }
}