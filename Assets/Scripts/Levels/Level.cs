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

    /// <summary>
    /// Level type: classic, side or challenge.
    /// </summary>
    [SerializeField]
    private LevelType _levelType;
    public LevelType Type { get => _levelType; }

    /// <summary>
    /// Level type: classic, side or challenge.
    /// </summary>
    [SerializeField]
    private int _index;
    public int Index { get => _index; }


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

    /// <summary>
    /// Max tower level available.
    /// </summary>
    [SerializeField]
    [Range(0, 3)]
    private int _towerLevel;
    public int TowerLevel { get => _towerLevel; }

    /// <summary>
    /// Enemies type in the level.
    /// </summary>
    public List<Enemy> Enemies 
    { 
        get
        {
            List<Enemy> availableEnemies = new List<Enemy>();

            foreach (Wave current in _availableWaves)
                foreach (EnemyGroup buffer in current.EnemyGroups)
                    if (!availableEnemies.Contains(buffer.Enemy))
                        availableEnemies.Add(buffer.Enemy);

            return availableEnemies;
        } 
    }
}