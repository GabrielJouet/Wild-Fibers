using System.Collections;
using UnityEngine;

/*
 * CHoc spikes is the projectile of choc tower
 */
public class ChocSpikes : Projectile
{
    protected bool _following = false;

    protected bool _attacking = false;

    protected Animator _animator;



    //Method used to initialize class (like a constructor)
    public void Initialize(TowerData newData, ProjectilePool newPool, Vector2 newPosition)
    {
        _following = false;
        _attacking = false;

        StopAllCoroutines();
        GetComponent<SpriteRenderer>().sprite = null;

        _data = newData;
        _projectilePool = newPool;

        _animator = GetComponent<Animator>();
        _animator.enabled = false;

        transform.position = newPosition;
    }


    public void StartFollowing(Enemy newEnemy)
    {
        if (!_following)
        {
            _following = true;
            _enemyTracked = newEnemy;
        }
    }


    protected override void Update()
    {
        if (_following)
        {
            if (_enemyTracked != null)
                TrackEnemy();
            else if (FollowPoint(_goalPosition, false))
                StopProjectile();
        }
    }


    //Method used to track an enemy moving 
    protected override void TrackEnemy()
    {
        if (_enemyTracked.gameObject.activeSelf)
        {
            if (FollowPoint(_enemyTracked.DamagePosition, false) && ! _attacking)
                StartCoroutine(Strike());
        }
        else
        {
            _goalPosition = _enemyTracked.DamagePosition;
            _enemyTracked = null;
        }
    }


    //Coroutine used to attack
    private IEnumerator Strike()
    {
        _attacking = true;

        //Time to attack
        _animator.enabled = true;

        //Time to stay a little longer, visibility purpose
        AttackEnemy(_enemyTracked);
        _following = false;
        yield return new WaitForSeconds(_animator.runtimeAnimatorController.animationClips[0].length);
        StopProjectile();
    }
}