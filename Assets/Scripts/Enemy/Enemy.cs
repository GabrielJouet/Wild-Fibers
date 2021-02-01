using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy base class.
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Description")]

    /// <summary>
    /// Display name.
    /// </summary>
    [SerializeField]
    protected string _displayName;
    public string Name { get => _displayName; }


    [Header("Display")]

    /// <summary>
    /// Selector object.
    /// </summary>
    [SerializeField]
    protected GameObject _selector;

    /// <summary>
    /// Health bar component.
    /// </summary>
    [SerializeField]
    protected HealthBar _healthBar;

    /// <summary>
    /// Dot display object.
    /// </summary>
    [SerializeField]
    protected SpriteRenderer _dotDisplay;

    /// <summary>
    /// Sprite renderer component.
    /// </summary>
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Animator component.
    /// </summary>
    [SerializeField]
    protected Animator _animator;


    [Header("Particle")]

    /// <summary>
    /// Damage particle used.
    /// </summary>
    [SerializeField]
    protected Particle _damageParticle;

    /// <summary>
    /// Emission points particle.
    /// </summary>
    [SerializeField]
    protected List<Transform> _particleEmissionsPoints;

    /// <summary>
    /// Number of particle per points.
    /// </summary>
    [SerializeField]
    protected int _numberOfParticles;


    [Header("Behaviour Variables")]

    /// <summary>
    /// Health max.
    /// </summary>
    [SerializeField]
    protected float _healthMax;
    public float HealthMax { get => _healthMax; private set => _healthMax = value; }

    /// <summary>
    /// Current health.
    /// </summary>
    protected float _health;
    public float Health { get => _health; }


    /// <summary>
    /// Armor max value.
    /// </summary>
    [SerializeField]
    protected float _armorMax;
    public float ArmorMax { get => _armorMax; set => _armorMax = value; }

    /// <summary>
    /// Current armor.
    /// </summary>
    protected float _armor;
    public float Armor { get => _armor; set => _armor = value; }

    /// <summary>
    /// Speed max.
    /// </summary>
    [SerializeField]
    protected float _speedMax;
    public float Speed { get => _speedMax; private set => _speedMax = value; }

    /// <summary>
    /// Current speed.
    /// </summary>
    protected float _speed;


    /// <summary>
    /// Number of lives removed if the enemy reaches the end of the path.
    /// </summary>
    [SerializeField]
    protected int _numberOfLivesTaken;
    public int LivesTaken { get => _numberOfLivesTaken; private set => _numberOfLivesTaken = value; }


    /// <summary>
    /// Gold value gained if the enemy is killed.
    /// </summary>
    [SerializeField]
    protected int _goldGained;
    public int GoldGained { get => _goldGained; private set => _goldGained = value; }


    /// <summary>
    /// Does the enemy actually flies?
    /// </summary>
    [SerializeField]
    protected bool _flying;
    public bool Flying { get => _flying; private set => _flying = value; }


    /// <summary>
    /// Damage position transform component.
    /// </summary>
    [SerializeField]
    protected Transform _damagePosition;
    public Vector2 DamagePosition { get => _damagePosition.position; }


    /// <summary>
    /// Dot duration in second.
    /// </summary>
    protected float _dotDuration;

    /// <summary>
    /// Dot health malus / ticks (0.5 sec).
    /// </summary>
    protected float _healthMalus = 0;


    /// <summary>
    /// Does the enemy is moving?
    /// </summary>
    public bool Moving { get; set; } = false;

    /// <summary>
    /// Does the enemy is slow down?
    /// </summary>
    protected bool _isSlowDown = false;

    /// <summary>
    /// Particle controller component.
    /// </summary>
    protected ParticleController _particleController;


    /// <summary>
    /// Does a dot is applied?
    /// </summary>
    protected bool _dotApplied = false;


    /// <summary>
    /// Loaded path.
    /// </summary>
    protected List<Vector2> _path;

    /// <summary>
    /// Progression on path.
    /// </summary>
    protected int _pathIndex;

    /// <summary>
    /// Path ratio.
    /// </summary>
    public float PathRatio { get => (float)_pathIndex / (float)_path.Count; }

    /// <summary>
    /// Pool Controller used to retrieve enemy.
    /// </summary>
    protected PoolController _poolController;

    /// <summary>
    /// Information UI component.
    /// </summary>
    public BackgroudSelecter InformationUI { get; set; }


    public float _preDamage = 0f;

    /// <summary>
    /// Does the enemy is already aimed?
    /// </summary>
    public bool AlreadyAimed { get; set; } = false;



    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newPath">New path used</param>
    /// <param name="newPool">Pool used for the current enemy</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public virtual void Initialize(List<Vector2> newPath, PoolController newPool, int pathIndex)
    {
        _preDamage = 0f;

        if (_particleController == null)
            _particleController = FindObjectOfType<ParticleController>();

        InformationUI = null;
        _dotApplied = false;
        _dotDisplay.sprite = null;
        _isSlowDown = false;

        _healthMalus = 0;
        gameObject.SetActive(true);

        _speed = _speedMax;
        _health = _healthMax;
        _armor = _armorMax;

        _healthBar.ResetSize();

        _pathIndex = pathIndex;

        transform.position = newPath[pathIndex];
        _path = newPath;
        _poolController = newPool;

        Moving = true;
        AlreadyAimed = false;
    }


    public void LoadData(float newHealth, float newArmor, float newSpeed, int newLives, bool newFlying, int newGoldGained)
    {
        HealthMax = newHealth;
        ArmorMax = newArmor;

        Speed = newSpeed;
        LivesTaken = newLives;

        Flying = newFlying;

        GoldGained = newGoldGained;
    }


    /// <summary>
    /// Update method, called each frame.
    /// </summary>
    protected void Update()
    {
        if(Moving)
            FollowPath();
    }


    /// <summary>
    /// Fixed Update method, called 50 times a second.
    /// </summary>
    protected void FixedUpdate()
    {
        if (InformationUI != null)
            InformationUI.UpdateEnemyInformation(this);
    }


    /// <summary>
    /// Method used to follow bezier path.
    /// </summary>
    protected void FollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, _path[_pathIndex], Time.deltaTime * _speed);

        if ((Vector2)transform.position == _path[_pathIndex] && _pathIndex + 1 < _path.Count)
            _pathIndex++;
        else if (_pathIndex + 1 == _path.Count)
            Die(true);

        _spriteRenderer.flipX = transform.position.x - _path[_pathIndex].x > 0;
    }


    /// <summary>
    /// Method to remove health to enemy.
    /// </summary>
    /// <param name="damage">How many damage the enemy is taking</param>
    /// <param name="armorThrough">The quantity of armor through the hit has</param>
    public void TakeDamage(float damage, float armorThrough)
    {
        int damageLeft = Mathf.FloorToInt(_armor - armorThrough < 0 ? damage + (damage * (armorThrough - _armor)/100)/2 : damage - ((_armor - armorThrough)/100 * damage));

        _preDamage -= damageLeft;
        TakeDamage(damageLeft);

        foreach (Transform current in _particleEmissionsPoints)
        {
            foreach (Particle particle in _particleController.GetParticle(_damageParticle, _numberOfParticles))
                particle.Initialize(current.position);
        }

        _healthBar.ChangeSize(_health / _healthMax);
    }


    /// <summary>
    /// Method used to remove health to an enemy without using shield.
    /// </summary>
    /// <param name="damage">The amount of damage the enemy is taking</param>
    public void TakeDamage(float damage)
    {
        if (_health - damage <= 0)
            Die(false);
        else
            _health -= damage;

        _healthBar.ChangeSize(_health / _healthMax);
    }


    /// <summary>
    /// Method used to apply a slowdown on the enemy.
    /// </summary>
    /// <param name="slowDownRatio">The percentage of speed loss</param>
    /// <param name="slowDownTime">Time before slowdown is reset</param>
    public void ApplySlowDown(float slowDownRatio, float slowDownTime)
    {
        if(!_isSlowDown)
        {
            _isSlowDown = true;
            StartCoroutine(ResetSlowDown(slowDownTime));
            _speed = _speedMax * (100 - slowDownRatio) / 100f;
        }
    }

    /// <summary>
    /// Coroutine used to delay the slow down reset.
    /// </summary>
    /// <returns>Yield the slow down reset time</returns>
    protected IEnumerator ResetSlowDown(float slowDownTime)
    {
        yield return new WaitForSeconds(slowDownTime);
        _isSlowDown = false;
        _speed = _speedMax;
    }


    /// <summary>
    /// Method used to apply a dot on the enemy.
    /// </summary>
    /// <param name="armorThroughMalus">The armor through malus applied</param>
    /// <param name="healthMalus">How much damage the enemy will take every 0.5 sec?</param>
    /// <param name="duration">Duration of the dot</param>
    /// <param name="newIcon">The new dot icon</param>
    public void ApplyDot(float armorThroughMalus, float healthMalus, float duration, Sprite newIcon)
    {
        _healthMalus = healthMalus;
        _dotDuration = duration;
        _dotDisplay.sprite = newIcon;

        if(!_dotApplied && isActiveAndEnabled)
        {
            StartCoroutine(TakePersistentDamage());
            _armor -= armorThroughMalus;
        }
    }


    /// <summary>
    /// Coroutine used take damage from dot.
    /// </summary>
    /// <returns>Yield 0.5 sec</returns>
    protected IEnumerator TakePersistentDamage()
    {
        _dotApplied = true;
        while(_dotDuration >= 0)
        {
            _dotDuration -= 0.5f;
            TakeDamage(_healthMalus);
            yield return new WaitForSeconds(0.5f);
        }

        _dotDisplay.sprite = null;
        _dotApplied = false;
        _armor = _armorMax;
    }


    /// <summary>
    /// Method used to teleport back enemy.
    /// </summary>
    public void TeleportBack(/*TO DO*/)
    {
        //TO DO
    }


    /// <summary>
    /// Method used when the life of an enemy drops below zero or other specific behavior.
    /// </summary>
    /// <param name="reachEnd">Does the enemy reaches the end?</param>
    protected void Die(bool reachEnd)
    {
        if (InformationUI)
            DesactivateUI();

        StopAllCoroutines();

        _poolController.RecoverEnemyPool(this).AddOneEnemy(this, reachEnd, reachEnd ? _numberOfLivesTaken : _goldGained);
    }


    /// <summary>
    /// Method used to desactivate enemy information UI.
    /// </summary>
    protected void DesactivateUI()
    {
        InformationUI.ErasePreviousEnemy();
        InformationUI.DisableEnemyInformation();
        _selector.SetActive(false);

        InformationUI = null;
    }


    /// <summary>
    /// Can the enemy survive the damage?
    /// </summary>
    /// <param name="damage">How much damage the enemy is taking?</param>
    /// <param name="armorThrough">The percentage of armor through the hit has</param>
    public bool CanSurvive(float damage, float armorThrough)
    {
        _preDamage += Mathf.FloorToInt(_armor - armorThrough < 0 ? damage + (damage * (armorThrough - _armor) / 100) / 2 : damage - ((_armor - armorThrough) / 100 * damage));

        return _health - _preDamage > 0;
    }

    /// <summary>
    /// Activate or desactivate selector state.
    /// </summary>
    public void SetSelector(bool state) 
    { 
        _selector.SetActive(state);
    }



    /// <summary>
    /// Collision enter on 2D objects.
    /// </summary>
    /// <param name="collision">Collision object</param>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TowerCollider towerCollider))
            towerCollider.EnemyCollide(this);
    }


    /// <summary>
    /// Collision exit on 2D objects.
    /// </summary>
    /// <param name="collision">Collision object</param>
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TowerCollider towerCollider))
            towerCollider.EnemyExit(this);
    }
}