using UnityEngine;

/// <summary>
/// Class used to handle skill augmentation in skill tree.
/// </summary>
[CreateAssetMenu(fileName = "NewAugmentation", menuName = "Towers/Augmentation")]
public class Augmentation : ScriptableObject
{
    /// <summary>
    /// Icon displayed.
    /// </summary>
    [SerializeField]
    private Sprite _icon;
    public Sprite Icon { get => _icon; }


    /// <summary>
    /// Price in skill points.
    /// </summary>
    [SerializeField]
    private int _price;
    public int Price { get => _price; }


    /// <summary>
    /// Short description.
    /// </summary>
    [SerializeField]
    private string _description;
    public string Description { get => _description; }
}