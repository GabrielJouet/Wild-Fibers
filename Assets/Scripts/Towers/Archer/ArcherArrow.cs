using UnityEngine;

public class ArcherArrow : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private float _damage;
    private float _armorThrough;

    private Enemy _enemyToTrack;
    private ArcherTower _parentTower;



    public void Initialize(float newDamage, float newArmorThrough, Enemy newEnemy, ArcherTower newParent)
    {
        transform.position = newParent.transform.position;
        _enemyToTrack = newEnemy;
        _damage = newDamage;
        _parentTower = newParent;
        _armorThrough = newArmorThrough;
    }


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


    private void StopArrow()
    {
        gameObject.SetActive(false);
        _parentTower.RecoverArrow(this);
    }
}