using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "Info/Enemy")]
public class EnemyInfo : InfoData
{
    [SerializeField]
    protected string _zones;
    public string Zones { get => _zones; }

    [SerializeField]
    protected int _health;
    public string Health { get => _health.ToString(); }

    [SerializeField]
    protected string _speed;
    public string Speed { get => _speed; }

    [SerializeField]
    protected string _armor;
    public string Armor { get => _armor; }

    [SerializeField]
    protected string _resistance;
    public string Resistance { get => _resistance; }

    [SerializeField]
    protected int _livesLost;
    public string LivesLost { get => _livesLost.ToString(); }
}
