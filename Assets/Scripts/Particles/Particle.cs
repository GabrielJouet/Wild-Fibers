using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [Header("Graphical")]
    [SerializeField]
    protected List<Sprite> _particleSprites;

    [SerializeField]
    protected SpriteRenderer _spriteRenderer;


    [Header("Behavior")]
    [SerializeField]
    protected float _liveTime;

    [SerializeField]
    protected float _horizontalMove;

    [SerializeField]
    protected float _verticalMove;


    protected ParticlePool _pool;

    protected float _horizontalSpeed;

    protected float _verticalSpeed;


    public void Initialize(Vector3 newPosition)
    {
        _spriteRenderer.sprite = _particleSprites[Random.Range(0, _particleSprites.Count)];
        transform.position = newPosition;

        _horizontalSpeed = Random.Range(-_horizontalMove, _horizontalMove) * Time.deltaTime;
        _verticalSpeed = Random.Range(-_verticalMove, _verticalMove) * Time.deltaTime;

        StartCoroutine(Fade());
    }


    protected void Update()
    {
        transform.position = new Vector2(transform.position.x + _horizontalSpeed, transform.position.y + _verticalSpeed);
    }


    protected IEnumerator Fade()
    {
        yield return new WaitForSeconds(_liveTime);
        _pool.RecoverParticle(this);
    }


    public void SetPool(ParticlePool newPool)
    {
        _pool = newPool;
    }
}
