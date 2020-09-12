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


    //Update method, called each frame
    private void Update()
    {
        //If the enemy is still alive
        if (_enemyToTrack.gameObject.activeSelf)
        {
            if ((transform.position - _enemyToTrack.transform.position).magnitude > 0.1f)
                transform.position = Vector3.MoveTowards(transform.position, _enemyToTrack.transform.position, _speed * Time.deltaTime);
            else
            {
                _enemyToTrack.ApplyDot(_armorThroughMalus, _damageOverTime, _dotDuration, _dotIcon);
                _enemyToTrack.TakeDamage(_damage, _armorThrough);
                StopDot();
            }
        }
        //Else if the enemy dies before reaching it, we stop chasing it
        //TO CHANGE, SHOULD CONTINUE UNTIL REACHING THE LAST KNOWN POSITION
        else
            StopDot();
    }


    //Method used to recover dot to parent tower
    private void StopDot()
    {
        gameObject.SetActive(false);
        _parentTower.RecoverDot(this);
    }
}