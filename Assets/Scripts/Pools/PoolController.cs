using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private readonly Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();



    /// <summary>
    /// 
    /// </summary>
    /// <param name="stored"></param>
    public void In(PoolableObject stored)
    {
        _pools[stored.Name].In(stored.gameObject);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="wanted"></param>
    /// <returns></returns>
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