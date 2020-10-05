using UnityEngine;

/*
 * Class used alongside Mark Tower, it is the mark tower dot projectile
 * TO CHANGE, SHOULD BE DOT INSTEAD
 */
public class MarkDot : MonoBehaviour
{
    //Speed of movement of projectile
    [SerializeField]
    private float _speed;

    //Which icon will be displayed in the enemy?
    [SerializeField]
    private Sprite _dotIcon;


    //Variables given by the mark tower
    private float _armorThroughMalus;
    private float _damageOverTime;
    private float _dotDuration;

    private float _damage;
    private float _armorThrough;


    //Enemy tracked
    private Enemy _enemyToTrack;

    //Tower that created this projectile
    private MarkTower _parentTower;

    private Vector3 _goalPosition;


    //Does the projectile is paused? (by PauseController)
    private bool _paused = false;



    //Method used to initialize class (like a constructor)
    //
    //Parameters => newDamage, Amount of damage done on attack
    //              newArmorThrough, armor malus done on attack
    //              newEnemy, new enemy to track
    //              newParent, new parent tower
    //              newMalus, new amor malus done on dot
    //              newOverTimeDamage, new damage done on dot
    //              newDuration, new dot duration
    public void Initialize(float newDamage, float newArmorThrough, Enemy newEnemy, MarkTower newParent, float newMalus, float newOverTimeDamage, float newDuration)
    {
        transform.position = newParent.transform.position;
        _enemyToTrack = newEnemy;
        _damage = newDamage;
        _parentTower = newParent;
        _armorThrough = newArmorThrough;

        _armorThroughMalus = newMalus;
        _damageOverTime = newOverTimeDamage;
        _dotDuration = newDuration;
    }


    //Update method, called every frame
    private void Update()
    {
        if(!_paused)
        {
            if (_enemyToTrack != null || _enemyToTrack.gameObject.activeSelf)
            {
                if (FollowPoint(_enemyToTrack.transform.position))
                {
                    _enemyToTrack.TakeDamage(_damage, _armorThrough);
                    _enemyToTrack.ApplyDot(_armorThroughMalus, _damageOverTime, _dotDuration, _dotIcon);
                    StopDot();
                }
            }
            else
            {
                _enemyToTrack = null;
                if (_goalPosition == Vector3.zero)
                    _goalPosition = _enemyToTrack.transform.position;
                else
                {
                    if (FollowPoint(_goalPosition))
                        StopDot();
                }
            }
        }
    }


    private bool FollowPoint(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);

        Vector3 vectorToTarget = position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        return (transform.position - position).magnitude < 0.05f;
    }


    //Method used to recover dot to parent tower
    private void StopDot()
    {
        gameObject.SetActive(false);
        _parentTower.RecoverDot(this);
    }


    //Method used to stop projectile behavior
    public void StopBehavior()
    {
        _paused = !_paused;
    }
}