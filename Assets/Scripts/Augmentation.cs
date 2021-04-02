using UnityEngine;

[CreateAssetMenu(fileName = "NewAugmentation", menuName = "Towers/Augmentation")]
public class Augmentation : ScriptableObject
{
    [SerializeField]
    private Sprite _icon;
    public Sprite Icon { get => _icon; }


    [SerializeField]
    private int _price;
    public int Price { get => _price; }


    [SerializeField]
    private string _description;
    public string Description { get => _description; }
}