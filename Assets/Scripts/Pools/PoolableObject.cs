using System;
using UnityEngine;

/// <summary>
/// Class used to define every poolable object.
/// </summary>
[Serializable]
public abstract class PoolableObject : MonoBehaviour
{
    /// <summary>
    /// Name of the poolable object used to find in the pool. 
    /// </summary>
    public string Name { get => _name; }

    /// <summary>
    /// Name of the poolable object used to find in the pool. 
    /// </summary>
    [SerializeField]
    private string _name;



    /// <summary>
    /// Method called when the object is retrieved from the pool.
    /// </summary>
    public virtual void OnOutPool() { }


    /// <summary>
    /// Method called when the object is stored in the pool.
    /// </summary>
    public virtual void OnInPool() { }
}