using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private EnemyGroup _enemyGroup;

    private int _patternIndex = 0;
    private int _enemyIndex = 0;

    private bool _canSpawn = true;

    public void SetNewGroup(EnemyGroup newGroup)
    {
        _enemyGroup = newGroup;

        StartCoroutine(SpawnEnemies());
    }


    private IEnumerator SpawnEnemies()
    {
        while(_canSpawn)
        {
            if(_enemyIndex < _enemyGroup.GetEnemyPattern(_patternIndex).GetNumberOfEnemies())
            {
                _enemyIndex ++;
                Instantiate(_enemyGroup.GetEnemyUsed());
                yield return new WaitForSeconds(_enemyGroup.GetEnemyPattern(_patternIndex).GetTimeBetweenEnemies());
            }
            else
            {
                if (_patternIndex < _enemyGroup.GetEnemyPatternCount())
                    _patternIndex++;
                else
                    EndSpawn();

                yield return new WaitForSeconds(_enemyGroup.GetTimeBetweenPattern());
            }
        }
    }


    private void EndSpawn()
    {
        _patternIndex = 0;
        _enemyGroup = null;
        _canSpawn = false;
        StopAllCoroutines();

        FindObjectOfType<LevelController>().EndWave();
    }


    public bool GetCanSpawn() { return _canSpawn; }
}