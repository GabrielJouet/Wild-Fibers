using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    private Stack<Particle> _pool = new Stack<Particle>();

    public Particle Prefab { get; set; }


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


    public void RecoverParticle(Particle newParticle)
    {
        newParticle.gameObject.SetActive(false);
        _pool.Push(newParticle);
    }


    public void SetPrefab(Particle newPrefab)
    {
        Prefab = newPrefab;
    }
}