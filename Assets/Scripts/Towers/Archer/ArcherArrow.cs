using UnityEngine;

/*
 * Projectile of the archer tower
 */
public class ArcherArrow : MonoBehaviour
{
    //Speed of movement
    [SerializeField]
    private float _speed;


    //Attack variables
    private float _damage;
    private float _armorThrough;


    //Enemy to attack
    private Enemy _enemyToTrack;

    //Parent tower that creates this projectile
    private ArcherTower _parentTower;



    //Method used to initialize class (like a constructor)
    //
    //Parameters => newDamage, Amount of damage done on attack
    //              newArmorThrough, armor malus done on attack
    //              newEnemy, new enemy to track
    //              newParent, new parent tower
    public void Initialize(float newDamage, float newArmorThrough, Enemy newEnemy, ArcherTower newParent)
    {
        transform.position = newParent.transform.position;
        _enemyToTrack = newEnemy;
        _damage = newDamage;
        _parentTower = newParent;
        _armorThrough = newArmorThrough;
    }


    //Update method, called every frame
    private void Update()
    {
        if (_enemyToTrack.gameObject.activeSelf)
        {
            if ((transform.position - _enemyToTrack.transform.position).magnitude > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _enemyToTrack.transform.position, _speed * Time.deltaTime);

                Vector3 vectorToTarget = _enemyToTrack.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                _enemyToTrack.TakeDamage(_damage, _armorThrough);
                StopArrow();
            }
        }
        else
            StopArrow();
    }


    //Method used to get back to parent tower
    private void StopArrow()
    {
        gameObject.SetActive(false);
        _parentTower.RecoverArrow(this);
    }
}