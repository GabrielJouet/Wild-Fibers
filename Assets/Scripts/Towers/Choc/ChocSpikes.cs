using System.Collections;
using UnityEngine;

/*
 * CHoc spikes is the projectile of choc tower
 */
public class ChocSpikes : Projectile
{
    //How much time the root take to dug out?
    [SerializeField]
    private float _timeToStrike;


    //Attack variables
    private float _damage;
    private float _armorThrough;

    //Enemy to attack
    private Enemy _enemyToTrack;


    //Does the choc spike stopped moving and attacking?
    private bool _stopped = false;



    //Method used to initialize class (like a constructor)
    //
    //Parameters => newDamage, Amount of damage done on attack
    //              newArmorThrough, armor malus done on attack
    //              newEnemy, new enemy to track
    //              newParent, new parent tower
    //              newPool, new projectile pool
    public void Initialize(float newDamage, float newArmorThrough, Enemy newEnemy, ProjectilePool newPool)
    {
        _stopped = false;
        transform.position = newEnemy.GetDamagePosition();
        _enemyToTrack = newEnemy;
        _damage = newDamage;
        _projectilePool = newPool;
        _armorThrough = newArmorThrough;

        StartCoroutine(Strike());
    }


    //Update method, called every frame
    private void Update()
    {
        if(_enemyToTrack)
        {
            if (_enemyToTrack.gameObject.activeSelf)
            {
                if (!_stopped)
                    transform.position = _enemyToTrack.GetDamagePosition();
            }
            else
            {
                _stopped = true;
                _enemyToTrack = null;
            }
        }
    }


    //Couroutine used to attack
    private IEnumerator Strike()
    {
        if (!_stopped)
        {
            //Time to attack
            yield return new WaitForSeconds(_timeToStrike);
            if (_enemyToTrack)
                _enemyToTrack.TakeDamage(_damage, _armorThrough);
            _stopped = true;
        }

        //Time to stay a little longer, visibility purpose
        yield return new WaitForSeconds(_timeToStrike / 2f);
        StopProjectile();
    }
}