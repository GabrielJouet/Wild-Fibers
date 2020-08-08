using UnityEngine;

public class MarkDot : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Sprite _dotIcon;

    private float _armorThroughMalus;
    private float _damageOverTime;
    private float _dotDuration;

    private float _damage;
    private float _armorThrough;

    private Enemy _enemyToTrack;
    private MarkTower _parentTower;



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


    private void Update()
    {
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
        else
            StopDot();
    }


    private void StopDot()
    {
        gameObject.SetActive(false);
        _parentTower.RecoverDot(this);
    }
}