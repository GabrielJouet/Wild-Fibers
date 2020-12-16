using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store desactivated particles.
/// </summary>
public class ParticlePool : MonoBehaviour
{
    /// <summary>
    /// The particle stack.
    /// </summary>
    private readonly Stack<Particle> _pool = new Stack<Particle>();

    /// <summary>
    /// The generated particle.
    /// </summary>
    public Particle Prefab { get; set; }



    /// <summary>
    /// Method used to recover one particle from the pool.
    /// </summary>
    /// <returns>Particle wanted</returns>
    public Particle GetOneParticle()
    {
        Particle buffer;

        if (_pool.Count > 0)
        {
            buffer = _pool.Pop();
            buffer.gameObject.SetActive(true);
        }
        else
        {
            buffer = Instantiate(Prefab, transform);
            buffer.SetPool(this);
        }

        return buffer;
    }


    /// <summary>
    /// Method used to store a new particle in the pool.
    /// </summary>
    /// <param name="newParticle">The particle to desactivate</param>
    public void StoreParticle(Particle newParticle)
    {
        newParticle.gameObject.SetActive(false);
        _pool.Push(newParticle);
    }
}