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


    [Header("Bestiary related")]

    /// <summary>
    /// Description used in bestiary.
    /// </summary>
    [SerializeField]
    protected string _description;
    public string Description { get => _description; }

    /// <summary>
    /// Screen shot used in bestiary.
    /// </summary>
    [SerializeField]
    protected Sprite _screenShot;
    public Sprite ScreenShot { get => _screenShot; }

    /// <summary>
    /// Special info box.
    /// </summary>
    [SerializeField]
    protected string _special;
    public string Special { get => _special; }

    /// <summary>
    /// Gold in string version.
    /// </summary>
    public string GoldInfo { get => _goldGained.ToString(); }

    /// <summary>
    /// Speed transformed.
    /// </summary>
    public string SpeedInfo { get => Converter.TransformSpeed(_speedMax); }

    /// <summary>
    /// Armor transformed.
    /// </summary>
    public string ArmorInfo { get => Converter.TransformArmor(ArmorMax / 100); }

    /// <summary>
    /// Resistance transformed.
    /// </summary>
    public string ResistanceInfo { get => Converter.TransformResistance(_resistance / 100); }

    /// <summary>
    /// Lives taken in string version.
    /// </summary>
    public string LivesTakenInfo { get => _numberOfLivesTaken.ToString(); }

    /// <summary>
    /// Health in string version.
    /// </summary>
    public string HealthInfo { get => _healthMax.ToString(); }

    protected List<Attack> _dots = new List<Attack>();


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


    public List<Attack> Attacks { get; set; } = new List<Attack>();


    /// <summary>
    /// Can the enemy be targeted?
    /// </summary>
    public bool CanBeTargeted { get; protected set; } = true;


    public bool IsDotted { get; protected set; }



    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newPath">New path used</param>
    /// <param name="newPool">Pool used for the current enemy</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public virtual void Initialize(List<Vector2> newPath, PoolController newPool, int pathIndex)
    {
        _dotDisplay.gameObject.SetActive(false);
        IsDotted = false;
        CanBeTargeted = true;
        Attacks.Clear();

        if (_particleController == null)
            _particleController = FindObjectOfType<ParticleController>();

        InformationUI = null;
        _isSlowDown = false;

        _dots = new List<Attack>();
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
    public void TakeDamage(Attack newAttack)
    {
        float armorThrough = newAttack.ArmorThrough;
        float damage = newAttack.Damage;

        int damageLeft = Mathf.FloorToInt(Armor - armorThrough < 0 ? damage + (damage * (armorThrough - Armor)/100)/2 : damage - ((Armor - armorThrough)/100 * damage));

        TakeDamage(damageLeft);

        Attacks.Remove(newAttack);

        foreach (Transform current in _particleEmissionsPoints)
            foreach (Particle particle in _particleController.GetParticle(_damageParticle, 3))
                particle.Initialize(current.position);

        if (newAttack.DotDuration > 0)
            ApplyDot(newAttack.ArmorThroughMalus, newAttack.DotDamage, newAttack.DotDuration);
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
    protected void ApplyDot(float armorThroughMalus, int healthMalus, float duration)
    {
        IsDotted = true;
        Attack buffer = new Attack(0, 0, duration, armorThroughMalus, healthMalus);
        _dots.Add(buffer);

        //TO REWORK, ITS NOT A PROPER WAY TO DISPLAY DOT
        _dotDisplay.gameObject.SetActive(true);

        if(isActiveAndEnabled)
            StartCoroutine(TakePersistentDamage(buffer));
    }


    /// <summary>
    /// Coroutine used take damage from dot.
    /// </summary>
    /// <returns>Yield 0.5 sec</returns>
    protected IEnumerator TakePersistentDamage(Attack newDot)
    {
        Armor -= newDot.ArmorThroughMalus * ResistancePercentage;
        float timeRemaining = newDot.DotDuration;

        while(timeRemaining > 0)
        {
            timeRemaining -= 0.5f;
            TakeDamage(newDot.DotDamage);
            yield return new WaitForSeconds(0.5f);
        }

        _dots.Remove(newDot);
        Armor += newDot.ArmorThroughMalus * ResistancePercentage;

        if (_dots.Count == 0)
        {
            _dotDisplay.gameObject.SetActive(false);
            IsDotted = false;
        }
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


    public void DestroyArmor(int percentage)
    {
        ArmorMax -= percentage;
        Armor -= percentage;
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
        Attacks.Add(new Attack(newAttack.Damage, newAttack.ArmorThrough, newAttack.DotDuration, newAttack.ArmorThroughMalus, newAttack.Dot));

        int total = 0;
        foreach (Attack current in Attacks)
        {
            float armor = ArmorMax - current.ArmorThroughMalus;

            total += Mathf.FloorToInt(armor - current.ArmorThrough < 0 ? current.Damage + (current.Damage * (current.ArmorThrough - armor) / 100) / 2 : current.Damage - ((armor - current.ArmorThrough) / 100 * current.Damage));
            total += Mathf.FloorToInt(current.DotDamage * 2 * current.DotDuration);
        }

        if (Health - total <= 0)
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