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

    private int _augmentationLevel;


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


    public void StartFollowing(Enemy newEnemy, TowerData newData, int augmentationLevel)
    {
        if (!_following)
        {
            _augmentationLevel = augmentationLevel;
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
            float damage = (_data.Damage + (enemy.IsDotted ? 1 : 0)) * (_augmentationLevel > 2 && Random.Range(0, 100) < 5 ? 2 : 1);
            enemy.TakeDamage(enemy.ArmorMax < 25 ? _data.ArmorThrough * 1.25f : _data.ArmorThrough, damage);

            if (_augmentationLevel > 1)
                enemy.DestroyArmor(2);

            if (_data.DotIcon != null)
                enemy.ApplyDot(_data.ArmorThroughMalus, _data.Dot, _data.DotDuration, _data.DotIcon);
        }
    }
}