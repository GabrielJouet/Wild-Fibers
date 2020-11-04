using System.Collections.Generic;
using UnityEngine;

public class TowerPool : MonoBehaviour
{
    //Base enemy prefab
    private Tower _towerPrefab;

    //List of non used enemies but yet instanciated and desactivated
    private readonly Stack<Tower> _towerPool = new Stack<Tower>();



    //Method used instead of using start (kinda a constructor)
    //
    //Parameters => newPrefab, the new projectile prefab used in spawn
    public void Initialize(Tower newPrefab)
    {
        _towerPrefab = newPrefab;
    }


    //Method used to recover one tower
    //
    //Return a new enemy either already instanciated or newly created
    public Tower GetOneTower()
    {
        Tower newTower = _towerPool.Count > 0 ? _towerPool.Pop() : Instantiate(_towerPrefab, transform);
        newTower.gameObject.SetActive(true);

        return newTower;
    }



    //Method used to add one tower to the pool
    //
    //Parameters => newTower, game object of the tower
    public void AddOneTower(Tower newTower)
    {
        //We desactivate it
        newTower.gameObject.SetActive(false);
        _towerPool.Push(newTower);
    }


    //Getter
    public Tower GetPrefab() { return _towerPrefab; }
}
