using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[Serializable]
public abstract class PoolableObject : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get => _name; }
    [SerializeField]
    private string _name;



    /// <summary>
    /// 
    /// </summary>
    public virtual void OnOutPool() { }


    /// <summary>
    /// 
    /// </summary>
    public virtual void OnInPool() { }
}