using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private List<Tower> _towerAvailables;


    public List<Tower> GetTowers() { return _towerAvailables; }

    public Tower GetTower(int index) { return _towerAvailables[index]; }
}