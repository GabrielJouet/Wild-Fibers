using UnityEngine;

[CreateAssetMenu(menuName = "Towers/Specs", fileName = "NewSpec")]
public class TowerSpec : ScriptableObject
{
    /// <summary>
    /// Icon of the tower spec.
    /// </summary>
    [SerializeField]
    private Sprite _icon;

    /// <summary>
    /// Icon of the tower spec.
    /// </summary>
    public Sprite Icon { get => _icon; }


    /// <summary>
    /// Price of the tower spec.
    /// </summary>
    [SerializeField]
    private int _price;

    /// <summary>
    /// Price of the tower spec.
    /// </summary>
    public int Price { get => _price; }


    /// <summary>
    /// Name of the tower spec.
    /// </summary>
    [SerializeField]
    private string _name;

    /// <summary>
    /// Name of the tower spec.
    /// </summary>
    public string Name { get => _name; }


    /// <summary>
    /// Description of the tower spec.
    /// </summary>
    [SerializeField]
    private string _description;

    /// <summary>
    /// Description of the tower spec.
    /// </summary>
    public string Description { get => _description; }
}
