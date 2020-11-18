using System.Collections;
using UnityEngine;

/*
 * Mark tower is a tower that shoots dot (Damage Over Time) projectile
 */
public class MarkTower : Tower
{
    [Header("Dot related")]
    //Armor through negative effect on enemy
    [SerializeField]
    private float _armorThroughMalus;

    //How much damage the dot will do?
    [SerializeField]
    private float _damageOverTime;

    //Duration of the dot in seconds
    [SerializeField]
    private float _dotDuration;



    //Fixed Update method, called  times a second
    private void FixedUpdate()
    {
        //If enemies are in range and we can start shooting, we shoot
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
            StartCoroutine(SummonMarks());
    }



    //Coroutine used to summon dot projectiles
    private IEnumerator SummonMarks()
    {
        _coroutineStarted = true;

        int numberOfStrikes = _availableEnemies.Count < _numberOfShots ? _availableEnemies.Count : _numberOfShots;

        _availableEnemies.Shuffle();
        foreach (Enemy current in RecoverAvailableEnemies(numberOfStrikes))
            _projectilePool.GetOneProjectile().GetComponent<MarkDot>().Initialize(_damage, _armorThrough, current, transform, _armorThroughMalus, _damageOverTime, _dotDuration, _projectilePool);

        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }
}