using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> _allEnemies;
    public List<Enemy> Enemies { get => _allEnemies; }


    public int FindEnemyIndex(Enemy enemyToFound)
    {
        int result = 0;
        for(int i = 0; i < _allEnemies.Count; i ++)
        {
            if (_allEnemies[i] == enemyToFound)
            {
                result = i;
                break;
            }
        }

        return result;
    }
}
