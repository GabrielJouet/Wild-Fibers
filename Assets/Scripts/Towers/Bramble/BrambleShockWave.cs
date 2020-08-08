using UnityEngine;

public class BrambleShockWave : MonoBehaviour
{
    [SerializeField]
    private float _expandTime;

    private float _maxRange;

    private float _damage;
    private float _armorThrough;

    private BrambleTower _parentTower;

    private float _expansionRate;

    private Vector3 _previousScale;


    public void Initialize(float newDamage, float newArmorThrough, BrambleTower newParent, float newRange)
    {
        _previousScale = new Vector3(0, 0, 1);

        transform.localScale = _previousScale;
        transform.position = newParent.transform.position;

        _damage = newDamage;
        _parentTower = newParent;
        _armorThrough = newArmorThrough;
        _maxRange = newRange;

        _expansionRate = _maxRange / _expandTime;
    }


    private void Update()
    {
        if (transform.localScale.x < _maxRange)
        {
            _previousScale.x += _expansionRate * Time.deltaTime;
            _previousScale.y += _expansionRate * Time.deltaTime;

            transform.localScale = _previousScale;
        }
        else
            StopWave();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy newEnemy))
            newEnemy.TakeDamage(_damage, _armorThrough);
    }


    private void StopWave()
    {
        gameObject.SetActive(false);
        _parentTower.RecoverWave(this);
    }
}