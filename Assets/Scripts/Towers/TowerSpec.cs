using UnityEngine;

[CreateAssetMenu(menuName = "Towers/Specs", fileName = "NewSpec")]
public class TowerSpec : ScriptableObject
{
    [SerializeField]
    private int _price;
    public int Price { get => _price; }
}
