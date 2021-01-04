using UnityEngine;

[CreateAssetMenu(menuName = "Towers/Specs", fileName = "NewSpec")]
public class TowerSpec : ScriptableObject
{
    [SerializeField]
    private Sprite _icon;
    public Sprite Icon { get => _icon; }

    [SerializeField]
    private int _price;
    public int Price { get => _price; }

    [SerializeField]
    private string _name;
    public string Name { get => _name; }

    [SerializeField]
    private string _description;
    public string Description { get => _description; }
}
