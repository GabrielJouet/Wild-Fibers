using System.Collections;
using UnityEngine;

/// <summary>
/// Choc spikes is used by Roots towers.
/// </summary>
public class ChocSpikes : Projectile
{
    /// <summary>
    /// Does the projectile is following an enemy?
    /// </summary>
    protected bool _following;

    /// <summary>
    /// Does the projectile is attacking an enemy?
    /// </summary>
    protected bool _attacking;

    /// <summary>
    /// Current animator.
    /// </summary>
    protected Animator _animator;


    /// <summary>
    /// Can the spike destroy some armor on hit?
    /// </summary>
    private bool _canDestroyArmor;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="newPosition">New position of this projectile</param>
    public void Initialize(Vector2 newPosition)
    {
        _following = false;
        _attacking = false;

        StopAllCoroutines();
        GetComponent<SpriteRenderer>().sprite = null;

        _animator = GetComponent<Animator>();
        _animator.enabled = false;

        transform.position = newPosition;
    }


    /// <summary>
    /// Update method, called each frame.
    /// </summary>
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


    /// <summary>
    /// Method used by tower when attacking an enemy.
    /// </summary>
    /// <param name="newEnemy">Enemy aimed</param>
    /// <param name="newData">New attack for this spike</param>
    /// <param name="canDestroyArmor">Can this projectile destroys armor?</param>
    public void StartFollowing(Enemy newEnemy, Attack newData, bool canDestroyArmor)
    {
        if (!_following)
        {
            _canDestroyArmor = canDestroyArmor;

            _attack = newData;
            _following = true;
            _enemyTracked = newEnemy;
        }
    }


    /// <summary>
    /// Method used to track and follow an enemy.
    /// </summary>
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


    /// <summary>
    /// Method called to strike an enemy.
    /// </summary>
    /// <remarks>Needs to be a coroutine because animation</remarks>
    private IEnumerator Strike()
    {
        _attacking = true;
        _animator.enabled = true;

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

            enemy.TakeDamage(_attack);
        }
    }
}