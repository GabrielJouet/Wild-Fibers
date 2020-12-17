using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to control the particle and particle pools.
/// </summary>
public class ParticleController : MonoBehaviour
{
    /// <summary>
    /// The pool prefab.
    /// </summary>
    [SerializeField]
    private ParticlePool _poolPrefab;

    /// <summary>
    /// Every particle pools.
    /// </summary>
    private readonly List<ParticlePool> _particlePools = new List<ParticlePool>();



    /// <summary>
    /// Method used to recover particles.
    /// </summary>
    /// <param name="particleWanted">The type of particle wanted</param>
    /// <param name="numberOfParticle">How many particle are wanted?</param>
    /// <returns>The list of wanted particle</returns>
    public List<Particle> GetParticle(Particle particleWanted, int numberOfParticle)
    {
        bool found = false;
        List<Particle> particles = new List<Particle>();

        foreach (ParticlePool current in _particlePools)
        {
            if (current.Prefab == particleWanted)
            {
                found = true;
                for (int i = 0; i < numberOfParticle; i++)
                    particles.Add(current.GetOneParticle());

                break;
            }
        }

        if (!found)
        {
            ParticlePool newPool = Instantiate(_poolPrefab, transform);
            newPool.Prefab = particleWanted;
            _particlePools.Add(newPool);

            for (int i = 0; i < numberOfParticle; i++)
                particles.Add(newPool.GetOneParticle());
        }

        return particles;
    }
}
