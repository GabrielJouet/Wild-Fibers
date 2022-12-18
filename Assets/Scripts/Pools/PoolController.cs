using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle pools and pool related behavior.
/// </summary>
public class PoolController : MonoBehaviour
{
    /// <summary>
    /// All pools available.
    /// </summary>
    private readonly Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();



    /// <summary>
    /// Method called to put an object inside the pool.
    /// </summary>
    /// <param name="stored">The object stored in a poolable object format.</param>
    public void In(PoolableObject stored)
    {
        _pools[stored.Name].In(stored.gameObject);
    }


    /// <summary>
    /// Method called to retrieve an object from the pool.
    /// </summary>
    /// <param name="wanted">The object we want to retrieve</param>
    /// <returns>Returns the object retrieved or instantiated</returns>
    public GameObject Out(PoolableObject wanted)
    {
        if (_pools.ContainsKey(wanted.Name))
            return _pools[wanted.Name].Out();

        Pool newPool = new GameObject(wanted.name + " Pool").AddComponent<Pool>();
        newPool.transform.parent = transform;
        newPool.Class = wanted;
        _pools.Add(wanted.Name, newPool);

        return newPool.Out();
    }


    /// <summary>
    /// Method called by level controller or change scene when the level is either loading or unloading.
    /// </summary>
    public void DesactivateEntities()
    {
        foreach (Pool pool in _pools.Values)
            pool.DisactivateObjects();
    }
}