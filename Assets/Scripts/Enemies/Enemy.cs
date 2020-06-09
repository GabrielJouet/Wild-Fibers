using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Description")]
    [SerializeField]
    protected string _displayName;

    [SerializeField]
    protected string _displayDescription;


    [Header("Behaviour Variables")]
    [SerializeField]
    protected float _healthMax;
    protected float _health;

    [SerializeField]
    protected float _armorMax;
    protected float _armor;

    [SerializeField]
    protected float _speedMax;
    protected float _speed;

    [SerializeField]
    protected int _numberOfLivesTaken;



    public void Initialize()
    {
        //TO DO
    }


    public void TakeDamage(float damage)
    {
        //TO DO
    }


    public void ApplySlowDown(/*TO DO*/)
    {
        //TO DO
    }


    public void ApplyDot(/*TO DO*/)
    {
        //TO DO
    }


    public void ApplyMark(/*TO DO*/)
    {
        //TO DO
    }


    public void TeleportBack(/*TO DO*/)
    {
        //TO DO
    }


    protected void Die()
    {
        //TO DO
    }


    public string GetName() { return _displayName; }

    public string GetDescription() { return _displayDescription; }

    public float GetHealth() { return _health; }

    public float GetMaxHealth() { return _healthMax; }

    public float GetArmor() { return _armor; }

    public float GetMaxArmor() { return _armorMax; }

    public float GetSpeed() { return _speed; }

    public float GetMaxSpeed() { return _speedMax; }

    public int GetNumberOfLivesTaken() { return _numberOfLivesTaken; }
}