using System.Collections.Generic;
using Miscellanious.Enums;
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

    /// <summary>
    /// Level name.
    /// </summary>
    public string Name { get => _name; }


    /// <summary>
    /// Level thumbnail.
    /// </summary>
    [SerializeField]
    private Sprite _picture;

    /// <summary>
    /// Level thumbnail.
    /// </summary>
    public Sprite Picture { get => _picture; }


    /// <summary>
    /// Level description.
    /// </summary>
    [SerializeField]
    private string _description;

    /// <summary>
    /// Level description.
    /// </summary>
    public string Description { get => _description; }


    /// <summary>
    /// Scene related.
    /// </summary>
    [SerializeField]
    private string _playSceneName;

    /// <summary>
    /// Scene related.
    /// </summary>
    public string Scene { get => _playSceneName; }


    /// <summary>
    /// Level type: classic, side or challenge.
    /// </summary>
    [SerializeField]
    private LevelType _levelType;

    /// <summary>
    /// Level type: classic, side or challenge.
    /// </summary>
    public LevelType Type { get => _levelType; }


    [Header("In play")]

    /// <summary>
    /// Number of waves.
    /// </summary>
    [SerializeField]
    private List<Wave> _availableWaves;

    /// <summary>
    /// Number of waves.
    /// </summary>
    public List<Wave> Waves { get => _availableWaves; }

    [Space(10)]

    /// <summary>
    /// How many lives in this level?
    /// </summary>
    [SerializeField]
    [Range(1,50)]
    private int _lifeCount;

    /// <summary>
    /// How many lives in this level?
    /// </summary>
    public int Lives { get => _lifeCount; }


    /// <summary>
    /// Gold count at the start of the level.
    /// </summary>
    [SerializeField]
    [Min(150)]
    private int _goldCount;

    /// <summary>
    /// Gold count at the start of the level.
    /// </summary>
    public int Gold { get => _goldCount; }


    /// <summary>
    /// Max tower level available.
    /// </summary>
    [SerializeField]
    [Range(0, 3)]
    private int _towerLevel;

    /// <summary>
    /// Max tower level available.
    /// </summary>
    public int TowerLevel { get => _towerLevel; }

    /// <summary>
    /// Gold multiplier for income gold.
    /// </summary>
    [SerializeField]
    [Range(0f, 1f)]
    private float _goldMultiplier;

    /// <summary>
    /// Gold multiplier for income gold.
    /// </summary>
    public float GoldMultiplier { get => _goldMultiplier; }


    /// <summary>
    /// Non-allowed towers in this level.
    /// </summary>
    [SerializeField]
    private List<Tower> _blockedTowers;

    /// <summary>
    /// Non-allowed towers in this level.
    /// </summary>
    public List<Tower> BlockedTowers { get => _blockedTowers; }
}