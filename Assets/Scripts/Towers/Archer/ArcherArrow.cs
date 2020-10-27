﻿using UnityEngine;

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

    private Vector3 _goalPosition;



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
        if (_enemyToTrack != null)
            TrackEnemy();
        else if(FollowPoint(_goalPosition))
                StopArrow();
    }


    //Method used to track an enemy moving 
    private void TrackEnemy()
    {
        if (_enemyToTrack.gameObject.activeSelf)
        {
            if (FollowPoint(_enemyToTrack.GetDamagePosition()))
            {
                _enemyToTrack.TakeDamage(_damage, _armorThrough);
                StopArrow();
            }
        }
        else
        {
            _goalPosition = _enemyToTrack.GetDamagePosition();
            _enemyToTrack = null;
        }
    }

    
    //Method used to follow a point moving in space
    //
    //Parameter => the point position in a vector 3
    private bool FollowPoint(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);

        Vector3 vectorToTarget = position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        return (transform.position - position).magnitude < 0.025f;
    }


    //Method used to get back to parent tower
    private void StopArrow()
    {
        gameObject.SetActive(false);
        _parentTower.RecoverArrow(this);

        _goalPosition = Vector3.zero;
    }
}