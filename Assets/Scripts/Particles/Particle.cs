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


    protected float _timeLeft;
    protected ParticlePool _pool;

    protected float _horizontalSpeed;

    protected float _verticalSpeed;


    public void Initialize(Vector3 newPosition)
    {
        _spriteRenderer.sprite = _particleSprites[Random.Range(0, _particleSprites.Count)];
        transform.position = newPosition;
        _timeLeft = _liveTime;
        _horizontalSpeed = Random.Range(-_horizontalMove, _horizontalMove) * Time.deltaTime;
        _verticalSpeed = Random.Range(-_verticalMove, _verticalMove) * Time.deltaTime;

    }


    protected void Update()
    {
        transform.position = new Vector2(transform.position.x + _horizontalSpeed, transform.position.y + _verticalSpeed);

        if (_timeLeft > 0)
            _timeLeft -= Time.deltaTime;
        else
            _pool.RecoverParticle(this);

        _spriteRenderer.color = new Color(1, 1, 1, (_timeLeft / _liveTime));
    }


    public void SetPool(ParticlePool newPool)
    {
        _pool = newPool;
    }
}
