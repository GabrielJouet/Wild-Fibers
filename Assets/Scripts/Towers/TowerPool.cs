using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store towers.
/// </summary>
public class TowerPool : MonoBehaviour
{
    /// <summary>
    /// The base prefab for this pool.
    /// </summary>
    public Tower Tower { get; set; }

    /// <summary>
    /// Pool of towers.
    /// </summary>
    private readonly Stack<Tower> _towerPool = new Stack<Tower>();



    /// <summary>
    /// Method used to recover one tower from the pool.
    /// </summary>
    /// <returns>A tower from the pool</returns>
    public Tower GetOneTower()
    {
        Tower newTower = _towerPool.Count > 0 ? _towerPool.Pop() : Instantiate(Tower, transform);
        newTower.gameObject.SetActive(true);

        return newTower;
    }


    /// <summary>
    /// Method used to add a tower to the pool.
    /// </summary>
    /// <param name="newTower">The tower to add</param>
    public void AddOneTower(Tower newTower)
    {
        newTower.gameObject.SetActive(false);
        _towerPool.Push(newTower);
    }
}
