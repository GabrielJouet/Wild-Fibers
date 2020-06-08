using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Level", fileName = "NewLevel")]
public class Level : ScriptableObject
{
    [SerializeField]
    private List<Wave> _availableWaves;


    public List<Wave> GetWaves() { return _availableWaves; }
}