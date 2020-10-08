using System;
using System.Collections;
using UnityEngine;

/*
 * CHoc spikes is the projectile of choc tower
 */
public class ChocSpikes : MonoBehaviour
{
    //How much time the root take to dug out?
    [SerializeField]
    private float _timeToStrike;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Animator _baseAnimator;


    //Attack variables
    private float _damage;
    private float _armorThrough;

    //Enemy to attack
    private Enemy _enemyToTrack;

    //Parent tower
    private ChocTower _parentTower;


    //Does the choc spike stopped moving and attacking?
    private bool _stopped = false;


    private bool _paused = false;

    private DateTime _coroutineStartTime;

    private float _coroutineTimeNeeded = 0f;



    //Method used to initialize class (like a constructor)
    //
    //Parameters => newDamage, Amount of damage done on attack
    //              newArmorThrough, armor malus done on attack
    //              newEnemy, new enemy to track
    //              newParent, new parent tower
    public void Initialize(float newDamage, float newArmorThrough, Enemy newEnemy, ChocTower newParent)
    {
        _stopped = false;
        transform.position = newEnemy.transform.position;
        _enemyToTrack = newEnemy;
        _damage = newDamage;
        _parentTower = newParent;
        _armorThrough = newArmorThrough;

        StartCoroutine(Strike());
    }


    //Update method, called every frame
    private void Update()
    {
        if(_enemyToTrack && !_paused)
        {
            if (_enemyToTrack.gameObject.activeSelf)
            {
                if (!_stopped)
                    transform.position = _enemyToTrack.transform.position;
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
            _coroutineStartTime = DateTime.Now;
            _coroutineTimeNeeded = _timeToStrike;
            yield return new WaitForSeconds(_timeToStrike);
            DamageEnemy();
        }

        //Time to stay a little longer, visibility purpose
        _coroutineStartTime = DateTime.Now;
        _coroutineTimeNeeded = _timeToStrike / 2f;
        yield return new WaitForSeconds(_timeToStrike / 2f);
        StopSpike();
    }


    //Method used to damage enemy and stop spike (used to handle pause correctly too)
    private void DamageEnemy()
    {
        if (_enemyToTrack)
            _enemyToTrack.TakeDamage(_damage, _armorThrough);
        _stopped = true;
    }


    //Method used to get back to parent tower
    private void StopSpike()
    {
        gameObject.SetActive(false);
        _parentTower.RecoverSpike(this);
        _coroutineTimeNeeded = 0;
    }


    //Method used to pause projectile behavior when pause button is hit
    public void StopBehavior()
    {
        if (!_paused)
        {
            StopAllCoroutines();
            _coroutineTimeNeeded -= (float)(DateTime.Now - _coroutineStartTime).TotalSeconds;
        }
        else if (_coroutineTimeNeeded > 0f)
            StartCoroutine(UnPauseDelay());

        _animator.enabled = _paused;
        _baseAnimator.enabled = _paused;
        _paused = !_paused;
    }


    //Method used when unpausing the game
    private IEnumerator UnPauseDelay()
    {
        _coroutineStartTime = DateTime.Now;
        yield return new WaitForSeconds(_coroutineTimeNeeded);
        _coroutineTimeNeeded = 0;

        if (!_stopped)
            DamageEnemy();
        else
            StopSpike();
    }
}