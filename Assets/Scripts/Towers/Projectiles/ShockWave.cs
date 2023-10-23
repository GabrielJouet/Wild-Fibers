using Enemies.Enemy_Types;
using UnityEngine;

/// <summary>
/// ShockWave is used by vines like towers as a projectile.
/// </summary>
public class ShockWave : Projectile
{
    /// <summary>
    /// Max range of the wave.
    /// </summary>
    private float _range;

    /// <summary>
    /// Does the wave hit flying enemies harder?
    /// </summary>
    private bool _hitFlyingHarder;



    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="newData">Attack related</param>
    /// <param name="newTransform">Tower position</param>
    /// <param name="range">Max range of this wave</param>
    /// <param name="hitFlyingHarder">Does the wave hit flying enemies harder?</param>
    public void Initialize(Attack newData, Transform newTransform, float range, bool hitFlyingHarder)
    {
        _hitFlyingHarder = hitFlyingHarder;
        transform.position = newTransform.position;
        _attack = newData;

        _range = range;
        transform.localScale = Vector3.forward;
    }


    /// <summary>
    /// Update method, called each frame.
    /// </summary>
    protected override void Update()
    {
        if (transform.localScale.x < _range)
            transform.localScale += Vector3.one * _projectileSpeed * Time.deltaTime;
        else
            StopProjectile();
    }


    /// <summary>
    /// Trigger method, called when a trigger enter the wave.
    /// </summary>
    /// <param name="collision">The trigger collided</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy newEnemy))
        {
            if (_hitFlyingHarder && newEnemy.Flying)
                newEnemy.TakeDamage(new Attack(_attack.Damage + 2, _attack.ArmorThrough, 0, 0, 0));
            else
                newEnemy.TakeDamage(_attack);
        }
    }
}