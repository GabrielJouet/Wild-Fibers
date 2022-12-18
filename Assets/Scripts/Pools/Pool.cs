using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to instantiate poolable object and store them.
/// </summary>
public class Pool : MonoBehaviour
{
    /// <summary>
    /// The unique object handled by this pool.
    /// </summary>
    public PoolableObject Class { get; set; }


    /// <summary>
    /// All items inside this pool, desactivated and waiting.
    /// </summary>
    private readonly Stack<GameObject> _itemPool = new Stack<GameObject>();

    /// <summary>
    /// All activated objects in the scene right now.
    /// </summary>
    private readonly List<GameObject> _activeObjects = new List<GameObject>();



    /// <summary>
    /// Method called to recover one object from the pool.
    /// </summary>
    /// <returns>The game object retrieved</returns>
    public GameObject Out()
    {
        GameObject buffer;

        if (_itemPool.Count > 0)
        {
            buffer = _itemPool.Pop();
            buffer.gameObject.SetActive(true);
        }
        else
            buffer = Instantiate(Class.gameObject, transform);
        
        _activeObjects.Add(buffer);

        buffer.GetComponent<PoolableObject>().OnOutPool();
        return buffer;
    }


    /// <summary>
    /// Method called to store an object inside a pool.
    /// </summary>
    /// <param name="newObject">The stored object</param>
    public void In(GameObject newObject)
    {
        newObject.gameObject.SetActive(false);
        newObject.transform.parent = transform;
        _itemPool.Push(newObject);

        newObject.GetComponent<PoolableObject>().OnInPool();
        _activeObjects.Remove(newObject);
    }


    /// <summary>
    /// Method called to disactivate every active object.
    /// </summary>
    public void DisactivateObjects()
    {
        List<GameObject> bufferList = new List<GameObject>(_activeObjects);

        foreach (GameObject activeObject in bufferList)
            In(activeObject);
    }
}