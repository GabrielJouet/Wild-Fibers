using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store level related data.
/// </summary>
[CreateAssetMenu(menuName = "Levels/Level", fileName = "NewLevel")]
public class Level : ScriptableObject
{
    [Header("Description")]

    /// <summary>
    /// Level name.
    /// </summary>
    [SerializeField]
    private string _name;
    public string Name { get => _name; }

    /// <summary>
    /// Level number.
    /// </summary>
    [SerializeField]
    private int _number;
    public int Number { get => _number; }

    /// <summary>
    /// Level thumbnail.
    /// </summary>
    [SerializeField]
    private Sprite _picture;
    public Sprite Picture { get => _picture; }

    /// <summary>
    /// Level description.
    /// </summary>
    [SerializeField]
    private string _description;
    public string Description { get => _description; }

    /// <summary>
    /// Scene related.
    /// </summary>
    [SerializeField]
    private string _playSceneName;
    public string Scene { get => _playSceneName; }


    [Header("In play")]

    /// <summary>
    /// Number of waves.
    /// </summary>
    [SerializeField]
    private List<Wave> _availableWaves;
    public List<Wave> Waves { get => _availableWaves; }

    [Space(10)]

    /// <summary>
    /// How many lives in this level?
    /// </summary>
    [SerializeField]
    [Range(1,50)]
    private int _lifeCount;
    public int Lives { get => _lifeCount; }

    /// <summary>
    /// Gold count at the start of the level.
    /// </summary>
    [SerializeField]
    [Min(150)]
    private int _goldCount;
    public int Gold { get => _goldCount; }

    [Space(10)]

    /// <summary>
    /// Enemies type in the level.
    /// </summary>
    [SerializeField]
    private List<Enemy> _enemyAvailables;
    public List<Enemy> Enemies { get => _enemyAvailables; }
}