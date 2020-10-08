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

    //Level number
    [SerializeField]
    private int _number;

    //Little thumbnail used in level selection
    [SerializeField]
    private Sprite _picture;

    //Little description used in level selection
    [SerializeField]
    private string _description;

    //Name of the Scene in Assets directory
    [SerializeField]
    private string _playSceneName;


    [Header("In play")]
    //List of available waves in a level
    [SerializeField]
    private List<Wave> _availableWaves;



    /*Getters*/
    #region
    public Wave GetWave(int index) { return _availableWaves[index]; }

    public int GetWaveCount() { return _availableWaves.Count; }

    public string GetName() { return _name; }

    public int GetNumber() { return _number; }

    public Sprite GetPicture() { return _picture; }

    public string GetDescription() { return _description; }

    public string GetPlaySceneName() { return _playSceneName; }
    #endregion
}