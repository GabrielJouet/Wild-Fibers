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


    [Header("Behaviour Variables")]

    /// <summary>
    /// Health max.
    /// </summary>
    [SerializeField]
    protected float _healthMax;
    public float HealthMax { get => _healthMax; protected set => _healthMax = value; }

    /// <summary>
    /// Current health.
    /// </summary>
    public float Health { get; protected set; }


    /// <summary>
    /// Armor max value.
    /// </summary>
    [SerializeField]
    protected float _armorMax;
    public float ArmorMax { get => _armorMax; protected set => _armorMax = value; }

    /// <summary>
    /// Current armor.
    /// </summary>
    public float Armor { get; protected set; }


    /// <summary>
    /// Resistance max value (resistance is used for dot and magic attacks).
    /// </summary>
    [SerializeField]
    protected float _resistance;
    public float ResistancePercentage { get => (100 - _resistance) / 100;  }


    /// <summary>
    /// Speed max.
    /// </summary>
    [SerializeField]
    protected float _speedMax;
    public float SpeedMax { get => _speedMax; protected set => _speedMax = value; }

    /// <summary>
    /// Current speed.
    /// </summary>
    protected float Speed { get; set; }


    /// <summary>
    /// Number of lives removed if the enemy reaches the end of the path.
    /// </summary>
    [SerializeField]
    protected int _numberOfLivesTaken;
    public int LivesTaken { get => _numberOfLivesTaken; protected set => _numberOfLivesTaken = value; }


    /// <summary>
    /// Gold value gained if the enemy is killed.
    /// </summary>
    [SerializeField]
    protected int _goldGained;


    /// <summary>
    /// Does the enemy actually flies?
    /// </summary>
    [SerializeField]
    protected bool _flying;
    public bool Flying { get => _flying; protected set => _flying = value; }


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


    public List<TowerData> Attacks { get; set; } = new List<TowerData>();

    /// <summary>
    /// Does the enemy is already dotted?
    /// </summary>
    public bool AlreadyDotted { get; set; } = false;


    protected List<TowerCollider> _towerColliders = new List<TowerCollider>();

    public bool CanBeTargeted { get; protected set; } = true;



    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newPath">New path used</param>
    /// <param name="newPool">Pool used for the current enemy</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public virtual void Initialize(List<Vector2> newPath, PoolController newPool, int pathIndex)
    {
        CanBeTargeted = true;
        _towerColliders.Clear();
        Attacks.Clear();
        AlreadyDotted = false;

        if (_particleController == null)
            _particleController = FindObjectOfType<ParticleController>();

        InformationUI = null;
        _dotApplied = false;
        _dotDisplay.sprite = null;
        _isSlowDown = false;

        _healthMalus = 0;
        gameObject.SetActive(true);

        Speed = SpeedMax;
        Health = HealthMax;
        Armor = ArmorMax;

        _healthBar.ResetSize();

        _pathIndex = pathIndex;

        transform.position = newPath[pathIndex];
        _path = newPath;
        _poolController = newPool;

        Moving = true;
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
        transform.position = Vector3.MoveTowards(transform.position, _path[_pathIndex], Time.deltaTime * Speed);

        if ((Vector2)transform.position == _path[_pathIndex] && _pathIndex + 1 < _path.Count)
            _pathIndex++;
        else if (_pathIndex + 1 == _path.Count)
            Die(true);

        _spriteRenderer.flipX = transform.position.x - _path[_pathIndex].x > 0;
    }


    /// <summary>
    /// Method to remove health to enemy.
    /// </summary>
    /// <param name="data">The tower data damaging enemy</param>
    public void TakeDamage(TowerData data)
    {
        float armorThrough = data.ArmorThrough;
        float damage = data.Damage;

        int damageLeft = Mathf.FloorToInt(Armor - armorThrough < 0 ? damage + (damage * (armorThrough - Armor)/100)/2 : damage - ((Armor - armorThrough)/100 * damage));

        if (Attacks.Count > 0 && Attacks.Contains(data))
            Attacks.Remove(data);

        TakeDamage(damageLeft);

        foreach (Transform current in _particleEmissionsPoints)
        {
            foreach (Particle particle in _particleController.GetParticle(_damageParticle, 3))
                particle.Initialize(current.position);
        }
    }


    /// <summary>
    /// Method used to remove health to an enemy without using shield.
    /// </summary>
    /// <param name="damage">The amount of damage the enemy is taking</param>
    public void TakeDamage(float damage)
    {
        if (Health - damage <= 0)
            Die(false);
        else
            Health -= damage;

        _healthBar.ChangeSize(Health / _healthMax);
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
            Speed = _speedMax * (100 - slowDownRatio * ResistancePercentage) / 100f;
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
        Speed = _speedMax;
    }


    /// <summary>
    /// Method used to apply a dot on the enemy.
    /// </summary>
    /// <param name="armorThroughMalus">The armor through malus applied</param>
    /// <param name="healthMalus">How much damage the enemy will take every 0.5 sec?</param>
    /// <param name="duration">Duration of the dot</param>
    /// <param name="newIcon">The new dot icon</param>
    public void ApplyDot(float armorThroughMalus, int healthMalus, float duration, Sprite newIcon)
    {
        _healthMalus = Mathf.Clamp(healthMalus * ResistancePercentage, 1, healthMalus);
        _dotDuration = duration;
        _dotDisplay.sprite = newIcon;

        if(!_dotApplied && isActiveAndEnabled)
        {
            StartCoroutine(TakePersistentDamage());
            Armor -= armorThroughMalus * ResistancePercentage;
        }
    }


    /// <summary>
    /// Coroutine used take damage from dot.
    /// </summary>
    /// <returns>Yield 0.5 sec</returns>
    protected IEnumerator TakePersistentDamage()
    {
        _dotApplied = true;
        while(_dotDuration > 0)
        {
            _dotDuration -= 0.5f;
            TakeDamage(_healthMalus);
            yield return new WaitForSeconds(0.5f);
        }

        AlreadyDotted = false;
        _dotDisplay.sprite = null;
        _dotApplied = false;
        Armor = _armorMax;
    }


    /// <summary>
    /// Method used to teleport back enemy.
    /// </summary>
    /// <param name="indexLosts">The number of steps lost</param>
    public void TeleportBack(int indexLosts)
    {
        _pathIndex -= indexLosts;
        transform.position =  _path[_pathIndex];
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
    /// Method used to add a new attack on enemy and check if it can survive.
    /// </summary>
    /// <param name="newAttack">The data of the attack</param>
    public void AddAttack(TowerData newAttack)
    {
        Attacks.Add(newAttack);

        if (newAttack.DotDuration != 0)
            AlreadyDotted = true;

        float total = 0;
        foreach (TowerData current in Attacks)
            total += Mathf.FloorToInt(_armorMax - current.ArmorThrough < 0 ? current.Damage + (current.Damage * (current.ArmorThrough - _armorMax) / 100) / 2 : current.Damage - ((_armorMax - current.ArmorThrough) / 100 * current.Damage));

        if (Health - (_dotDuration * 2 * _healthMalus + total) <= 0)
            CanBeTargeted = false;

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
        {
            towerCollider.EnemyCollide(this);
            _towerColliders.Add(towerCollider);
        }
    }


    /// <summary>
    /// Collision exit on 2D objects.
    /// </summary>
    /// <param name="collision">Collision object</param>
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TowerCollider towerCollider))
        {
            towerCollider.EnemyExit(this);
            _towerColliders.Remove(towerCollider);
        }
    }
}