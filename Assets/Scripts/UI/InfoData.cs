using UnityEngine;

public class InfoData : ScriptableObject
{
    [SerializeField]
    protected Sprite _sprite;
    public Sprite Sprite { get => _sprite; }

    [SerializeField]
    protected string _description;
    public string Description { get => _description; }

    [SerializeField]
    protected string _special;
    public string Special { get => _special; }
}
