using UnityEngine;

[CreateAssetMenu(fileName = "LevelParameters0", menuName = "Levels/Parameters")]
public class LevelParameters : ScriptableObject
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private int _number;

    [SerializeField]
    private Sprite _picture;

    [SerializeField]
    private string _description;

    [SerializeField]
    private string _playSceneName;


    public string GetName() { return _name; }

    public int GetNumber() { return _number; }

    public Sprite GetPicture() { return _picture; }

    public string GetDescription() { return _description; }

    public string GetPlaySceneName() { return _playSceneName; }
}