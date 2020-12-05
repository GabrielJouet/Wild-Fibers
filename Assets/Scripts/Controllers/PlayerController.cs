using System.Collections.Generic;
using UnityEngine;

/*
 * Class used to handle player inputs and parameters 
 */
public class PlayerController : MonoBehaviour
{
    //List of every available tower 
    //TO REWORK
    [SerializeField]
    private List<Tower> _towerAvailables;



    private void Start()
    {
        Application.targetFrameRate = 60;
    }



    //Getter
    //
    //Return => Every tower the player is currently using
    public List<Tower> GetTowers() { return _towerAvailables; }


    //Getter
    //
    //Parameter => The chosen tower index
    //Return => A tower the player is currently using
    public Tower GetTower(int index) { return _towerAvailables[index]; }
}