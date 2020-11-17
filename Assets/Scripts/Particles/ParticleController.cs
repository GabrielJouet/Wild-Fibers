using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField]
    private ParticlePool _poolPrefab;

    private readonly List<ParticlePool> _particlePools = new List<ParticlePool>();

    
    public List<Particle> GetParticle(Particle particleWanted, int numberOfParticle)
    {
        bool finished = false;
        List<Particle> particles = new List<Particle>();

        foreach (ParticlePool current in _particlePools)
        {
            if (current.Prefab == particleWanted)
            {
                finished = true;
                for (int i = 0; i < numberOfParticle; i++)
                    particles.Add(current.GetOneParticle());

                break;
            }
        }

        if (!finished)
        {
            ParticlePool newPool = Instantiate(_poolPrefab, transform);
            newPool.SetPrefab(particleWanted);
            _particlePools.Add(newPool);

            for (int i = 0; i < numberOfParticle; i++)
                particles.Add(newPool.GetOneParticle());
        }

        return particles;
    }
}
