using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class related to particles.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class Particle : MonoBehaviour
{
    [Header("Graphical")]

    /// <summary>
    /// List of sprite used.
    /// </summary>
    [SerializeField]
    protected List<Sprite> _particleSprites;


    [Header("Behavior")]

    /// <summary>
    /// Time to live.
    /// </summary>
    [SerializeField]
    protected float _liveTime;

    /// <summary>
    /// Quantity of horizontal move.
    /// </summary>
    [SerializeField]
    protected float _horizontalMove;

    /// <summary>
    /// Quantity of vertical move.
    /// </summary>
    [SerializeField]
    protected float _verticalMove;

    /// <summary>
    /// Sprite renderer component.
    /// </summary>
    protected SpriteRenderer _spriteRenderer;

    /// <summary>
    /// List of sprite used.
    /// </summary>
    protected float _timeLeft;

    /// <summary>
    /// Pool used in recover.
    /// </summary>
    protected ParticlePool _pool;

    /// <summary>
    /// Horizontal speed (related to horizontal move).
    /// </summary>
    protected float _horizontalSpeed;

    /// <summary>
    /// Vertical speed (related to vertical move).
    /// </summary>
    protected float _verticalSpeed;


    /// <summary>
    /// Awake method used for initialization.
    /// </summary>
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    /// <summary>
    /// Initialize method called when desactivated.
    /// </summary>
    /// <param name="newPosition">New particle position</param>
    public void Initialize(Vector3 newPosition)
    {
        _spriteRenderer.sprite = _particleSprites[Random.Range(0, _particleSprites.Count)];
        transform.position = newPosition;
        _timeLeft = _liveTime;
        _horizontalSpeed = Random.Range(-_horizontalMove, _horizontalMove) * Time.deltaTime;
        _verticalSpeed = Random.Range(-_verticalMove, _verticalMove) * Time.deltaTime;

    }


    /// <summary>
    /// Update method (called every frame).
    /// </summary>
    protected void Update()
    {
        transform.position = new Vector2(transform.position.x + _horizontalSpeed, transform.position.y + _verticalSpeed);

        if (_timeLeft > 0)
            _timeLeft -= Time.deltaTime;
        else
            _pool.StoreParticle(this);

        _spriteRenderer.color = new Color(1, 1, 1, (_timeLeft / _liveTime));
    }


    /// <summary>
    /// Method used to set the pool
    /// </summary>
    /// <param name="newPool">The new pool to set</param>
    public void SetPool(ParticlePool newPool)
    {
        _pool = newPool;
    }
}
