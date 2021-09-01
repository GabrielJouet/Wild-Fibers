using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store towers.
/// </summary>
public class TowerPool : MonoBehaviour
{
    /// <summary>
    /// Tower prefab.
    /// </summary>
    [SerializeField]
    private GameObject _towerPrefab;

    /// <summary>
    /// Pool of towers.
    /// </summary>
    private readonly Stack<GameObject> _towerPool = new Stack<GameObject>();

    /// <summary>
    /// Towers instantiated but activated.
    /// </summary>
    private readonly List<GameObject> _towerUsed = new List<GameObject>();



    /// <summary>
    /// Method used to recover one tower from the pool.
    /// </summary>
    /// <returns>A tower from the pool</returns>
    public GameObject GetOneTower()
    {
        GameObject newTower = _towerPool.Count > 0 ? _towerPool.Pop() : Instantiate(_towerPrefab, transform);
        newTower.gameObject.SetActive(true);

        _towerUsed.Add(newTower);

        return newTower;
    }


    /// <summary>
    /// Method used to add a tower to the pool.
    /// </summary>
    /// <param name="newTower">The tower to add</param>
    public void AddOneTower(GameObject newTower)
    {
        _towerUsed.Remove(newTower);

        Tower buffer = newTower.GetComponent<Tower>();
        buffer.DesactivateTower();
        Destroy(buffer);

        _towerPool.Push(newTower);
    }


    /// <summary>
    /// Method called by pool controller when the level is either loading or unloading.
    /// </summary>
    public void DesactivateTowers()
    {
        List<GameObject> bufferList = new List<GameObject>(_towerUsed);

        foreach (GameObject current in bufferList)
            AddOneTower(current);
    }
}
