using System.Collections;
using UnityEngine;

/*
 * CHoc spikes is the projectile of choc tower
 */
public class ChocSpikes : Projectile
{
    //Does the choc spike stopped moving and attacking?
    private bool _stopped = false;



    //Method used to initialize class (like a constructor)
    public override void Initialize(TowerData newData, Enemy newEnemy, ProjectilePool newPool, Transform newPosition)
    {
        _data = newData;
        _projectilePool = newPool;

        _enemyTracked = newEnemy;

        _stopped = false;
        transform.position = newEnemy.DamagePosition;

        StartCoroutine(Strike());
    }


    //Update method, called every frame
    protected override void Update()
    {
        if(_enemyTracked)
        {
            if (_enemyTracked.gameObject.activeSelf)
            {
                if (!_stopped)
                    transform.position = _enemyTracked.DamagePosition;
            }
            else
            {
                _stopped = true;
                _enemyTracked = null;
            }
        }
    }


    //Couroutine used to attack
    private IEnumerator Strike()
    {
        if (!_stopped)
        {
            //Time to attack
            yield return new WaitForSeconds(1);
            AttackEnemy(_enemyTracked);
            _stopped = true;
        }

        //Time to stay a little longer, visibility purpose
        yield return new WaitForSeconds(0.5f);
        StopProjectile();
    }
}