using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "Info/Tower")]
public class TowerInfo : InfoData
{
    [SerializeField]
    protected int _price;
    public string Price { get => _price.ToString(); }

    [SerializeField]
    protected int _damage;
    public string Damage { get => _damage.ToString(); }

    [SerializeField]
    protected string _fireRate;
    public string FireRate { get => _fireRate; }

    [SerializeField]
    protected string _armorThrough;
    public string ArmorThrough { get => _armorThrough; }

    [SerializeField]
    protected string _dot;
    public string Dot { get => _dot; }

    [SerializeField]
    protected int _shots;
    public string Shots { get => _shots.ToString(); }
}
