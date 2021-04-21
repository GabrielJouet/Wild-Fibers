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

    private bool _canDestroyArmor = false;



    //Method used to initialize class (like a constructor)
    public void Initialize(ProjectilePool newPool, Vector2 newPosition)
    {
        _following = false;
        _attacking = false;

        StopAllCoroutines();
        GetComponent<SpriteRenderer>().sprite = null;

        _projectilePool = newPool;

        _animator = GetComponent<Animator>();
        _animator.enabled = false;

        transform.position = newPosition;
    }


    public void StartFollowing(Enemy newEnemy, Attack newData, bool canDestroyArmor)
    {
        if (!_following)
        {
            _canDestroyArmor = canDestroyArmor;

            _data = newData;
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


    /// <summary>
    /// Method called to hurt an enemy.
    /// </summary>
    /// <param name="enemy">The related enemy</param>
    protected override void AttackEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            if (_canDestroyArmor)
                enemy.DestroyArmor(2);

            enemy.TakeDamage(_data);
        }
    }
}