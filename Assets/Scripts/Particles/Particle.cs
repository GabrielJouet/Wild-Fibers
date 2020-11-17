using System.Collections;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField]
    private float _liveTime;

    [SerializeField]
    private float _speed;

    private ParticlePool _pool;


    public void Initialize(float newLiveTime, float newSpeed, Vector3 newPosition)
    {
        transform.position = newPosition;
        _liveTime = newLiveTime;
        _speed = newSpeed;
        StartCoroutine(Fade());
    }

    public void Initialize(Vector3 newPosition)
    {
        transform.position = newPosition;
        StartCoroutine(Fade());
    }


    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(_liveTime);
        _pool.RecoverParticle(this);
    }


    public void SetPool(ParticlePool newPool)
    {
        _pool = newPool;
    }
}
