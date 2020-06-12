using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Description")]
    [SerializeField]
    protected string _displayName;

    [SerializeField]
    protected string _displayDescription;

    [SerializeField]
    protected int _price;



    [Header("Damage Related")]
    [SerializeField]
    protected float _timeBetweenShots;

    [SerializeField]
    protected int _damage;

    [SerializeField]
    protected float _armorThrough;

    [SerializeField]
    protected int _numberOfShots;

    [SerializeField]
    protected GameObject _projectileUsed;

    [SerializeField]
    protected float _range;


    protected List<Enemy> _availableEnemies = new List<Enemy>();

    protected CapsuleCollider2D _collider;


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            _availableEnemies.Add(enemy);
    }


    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (_availableEnemies.Contains(enemy))
                _availableEnemies.Remove(enemy);
        }
    }


    protected void Shoot()
    {
        //TO DO
    }



    public string GetName() { return _displayName; }

    public string GetDescription() { return _displayDescription; }

    public int GetPrice() { return _price; }

    public float GetTimeBetweenShots() { return _timeBetweenShots; }

    public int GetDamage() { return _damage; }

    public float GetArmorThrough() { return _armorThrough; }

    public int GetNumberOfShots() { return _numberOfShots; }

    public GameObject GetProjectileUsed() { return _projectileUsed; }

    public float GetRange() { return _range; }
}