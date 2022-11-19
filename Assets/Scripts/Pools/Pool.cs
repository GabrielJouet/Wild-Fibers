using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Pool : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public PoolableObject Class { get; set; }


    /// <summary>
    /// 
    /// </summary>
    private readonly Stack<GameObject> _itemPool = new Stack<GameObject>();

    /// <summary>
    /// 
    /// </summary>
    private readonly List<GameObject> _activeObjects = new List<GameObject>();



    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="newObject"></param>
    public void In(GameObject newObject)
    {
        newObject.gameObject.SetActive(false);
        newObject.transform.parent = transform;
        _itemPool.Push(newObject);

        newObject.GetComponent<PoolableObject>().OnInPool();
        _activeObjects.Remove(newObject);
    }


    /// <summary>
    /// 
    /// </summary>
    public void DisactivateObjects()
    {
        List<GameObject> bufferList = new List<GameObject>(_activeObjects);

        foreach (GameObject activeObject in bufferList)
            In(activeObject);
    }
}