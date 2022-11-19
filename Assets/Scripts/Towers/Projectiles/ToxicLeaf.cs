using UnityEngine;

/// <summary>
/// Class used by Toxic like towers as a projectile.
/// </summary>
public class ToxicLeaf : Projectile
{
    /// <summary>
    /// Does the leaf is following an enemy?
    /// </summary>
    protected bool _following;

    /// <summary>
    /// Actual angle of the leaf.
    /// </summary>
    protected float _angle;

    /// <summary>
    /// Position of the tower when rotating around.
    /// </summary>
    protected Vector2 _initialPosition;


    /// <summary>
    /// Does the projectile apply slowDown?
    /// </summary>
    private bool _slowDown;

    /// <summary>
    /// Rotation speed of the projectile itself.
    /// </summary>
    private float _rotationSpeed;



    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="newPool">New pool attached</param>
    /// <param name="newPosition">New position of the projectile</param>
    /// <param name="slowDown">Does the projectile apply slowDown?</param>
    public void Initialize(Vector2 newPosition, bool slowDown)
    {
        _following = false;
        _angle = Random.Range(0, 360);

        StopAllCoroutines();

        _initialPosition = newPosition;
        transform.position = _initialPosition;
        transform.localScale = new Vector3(1, 1, 1);

        _slowDown = slowDown;

        _rotationSpeed = Random.Range(0.5f, 1.5f);
        _rotationSpeed *= Random.Range(0, 2) == 1 ? -1 : 1;
    }


    /// <summary>
    /// Update is called each frame.
    /// </summary>
    protected override void Update()
    {
        if (_following)
        {
            if (_enemyTracked != null)
                TrackEnemy();
            else if (FollowPoint(_goalPosition, true))
                StopProjectile();
        }
        else
        {
            _angle += Time.deltaTime * _rotationSpeed;
            transform.localPosition = _initialPosition + new Vector2(Mathf.Sin(_angle) * 0.15f, Mathf.Cos(_angle) * 0.15f);
            transform.RotateAround(transform.position, new Vector3(0, 0, 1), _projectileSpeed * 4 * Time.deltaTime);
        }
    }


    /// <summary>
    /// Method called by tower when starting to follow an enemy.
    /// </summary>
    /// <param name="newEnemy">The enemy followed</param>
    /// <param name="newData">The new attack</param>
    public void StartFollowing(Enemy newEnemy, Attack newData)
    {
        if (!_following)
        {
            _attack = newData;
            _following = true;
            _enemyTracked = newEnemy;
        }
    }


    /// <summary>
    /// Method called to hurt an enemy.
    /// </summary>
    /// <param name="enemy">The related enemy</param>
    protected override void AttackEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            if (_slowDown)
                enemy.ApplySlowDown(10, 2f);

            enemy.TakeDamage(_attack);
        }
    }
}
