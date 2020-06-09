using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private EnemyGroup _enemyGroup;

    private int _patternIndex = 0;
    private int _enemyIndex = 0;

    private bool _waveFinished = false;


    public void SetNewGroup(EnemyGroup newGroup)
    {
        _enemyGroup = newGroup;
        _waveFinished = false;

        StartCoroutine(SpawnEnemies());
    }


    private IEnumerator SpawnEnemies()
    {
        while(!_waveFinished)
        {
            if (_enemyIndex < _enemyGroup.GetEnemyPattern(_patternIndex).GetNumberOfEnemies())
            {
                _enemyIndex ++;
                Instantiate(_enemyGroup.GetEnemyUsed());
                yield return new WaitForSeconds(_enemyGroup.GetEnemyPattern(_patternIndex).GetTimeBetweenEnemies());
            }
            else
            {
                if (_patternIndex + 1 < _enemyGroup.GetEnemyPatternCount())
                {
                    _patternIndex++;
                    _enemyIndex = 0;

                    yield return new WaitForSeconds(_enemyGroup.GetTimeBetweenPattern());
                }
                else
                    EndSpawn();
            }
        }
    }


    private void EndSpawn()
    {
        _patternIndex = 0;
        _enemyIndex = 0;
        _enemyGroup = null;
        _waveFinished = true;
        StopAllCoroutines();

        FindObjectOfType<LevelController>().EndWave();
    }


    public bool GetWaveFinished() { return _waveFinished; }
}