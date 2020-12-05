using System.Collections.Generic;
using UnityEngine;

/*
 * Class used to save level data
 */
[CreateAssetMenu(menuName = "Levels/Level", fileName = "NewLevel")]
public class Level : ScriptableObject
{
    [Header("Description")]
    //Name of the level
    [SerializeField]
    private string _name;
    public string Name { get => _name; }

    //Level number
    [SerializeField]
    private int _number;
    public int Number { get => _number; }

    //Little thumbnail used in level selection
    [SerializeField]
    private Sprite _picture;
    public Sprite Picture { get => _picture; }

    //Little description used in level selection
    [SerializeField]
    private string _description;
    public string Description { get => _description; }

    //Name of the Scene in Assets directory
    [SerializeField]
    private string _playSceneName;
    public string Scene { get => _playSceneName; }


    [Header("In play")]
    //List of available waves in a level
    [SerializeField]
    private List<Wave> _availableWaves;
    public List<Wave> Waves { get => _availableWaves; }

    [Space(10)]

    //Lives allowed at the start of the level
    [SerializeField]
    [Range(1,50)]
    private int _lifeCount;
    public int Lives { get => _lifeCount; }

    //Gold amount allowed at the start of the level
    [SerializeField]
    [Min(150)]
    private int _goldCount;
    public int Gold { get => _goldCount; }

    [Space(10)]

    //Enemies available in this level
    [SerializeField]
    private List<Enemy> _enemyAvailables;
    public List<Enemy> Enemies { get => _enemyAvailables; }
}