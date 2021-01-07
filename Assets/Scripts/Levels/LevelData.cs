using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Data")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private Level _classic;
    public Level Classic { get => _classic; }


    [SerializeField]
    private Level _side;
    public Level Side { get => _side; }


    [SerializeField]
    private Level _challenge;
    public Level Challenge { get => _challenge; }
}
