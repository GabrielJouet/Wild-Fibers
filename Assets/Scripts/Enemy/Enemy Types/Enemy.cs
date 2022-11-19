using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy base class.
/// </summary>
public class Enemy : PoolableObject
{
    #region Display
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
    protected GameObject _dotDisplay;

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
    #endregion


    #region Behavior
    [Header("Behaviour Variables")]

    /// <summary>
    /// Health max.
    /// </summary>
    [SerializeField]
    protected float _healthMax;
    public float HealthMax { get => _healthMax; }

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
    public float SpeedMax { get => _speedMax; }

    /// <summary>
    /// Current speed.
    /// </summary>
    protected float Speed { get; set; }


    /// <summary>
    /// Number of lives removed if the enemy reaches the end of the path.
    /// </summary>
    [SerializeField]
    protected int _numberOfLivesTaken;
    public int LivesTaken { get => _numberOfLivesTaken; }


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
    public bool Flying { get => _flying; }


    /// <summary>
    /// Damage position transform component.
    /// </summary>
    [SerializeField]
    protected Transform _damagePosition;
    public Vector2 DamagePosition { get => _damagePosition.position; }
    #endregion


    #region Bestiary
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
    public string ArmorInfo { get => Converter.TransformArmor((Armor > 0 ? Armor : ArmorMax) / 100); }

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
    #endregion


    /// <summary>
    /// Does the enemy is moving?
    /// </summary>
    protected bool _moving = false;

    /// <summary>
    /// Does the enemy is slow down?
    /// </summary>
    protected bool _isSlowDown = false;

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
    /// Information UI component.
    /// </summary>
    public BackgroudSelecter InformationUI { get; set; }

    /// <summary>
    /// Current dot used.
    /// </summary>
    protected List<Attack> _dots = new List<Attack>();

    /// <summary>
    /// Current spawner related to this enemy.
    /// </summary>
    protected Spawner _spawner;


    /// <summary>
    /// Is the enemy dotted?
    /// </summary>
    public bool IsDotted { get; protected set; }



    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newPath">New path used</param>
    /// <param name="spawner">Spawner that spawns this enemy</param>
    /// <param name="pathIndex">Current progression on the path</param>
    public virtual void Initialize(List<Vector2> newPath, int pathIndex, Spawner spawner)
    {
        _spawner = spawner;

        _dotDisplay.SetActive(false);
        IsDotted = false;

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

        _moving = true;
    }


    /// <summary>
    /// Update method, called each frame.
    /// </summary>
    protected void Update()
    {
        if(_moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _path[_pathIndex], Time.deltaTime * Speed);

            if ((Vector2)transform.position == _path[_pathIndex] && _pathIndex + 1 < _path.Count)
                _pathIndex++;
            else if (_pathIndex + 1 == _path.Count)
                Die(true);

            _spriteRenderer.flipX = transform.position.x - _path[_pathIndex].x > 0;
        }
    }


    /// <summary>
    /// Method to remove health to enemy.
    /// </summary>
    /// <param name="newAttack">The related attack</param>
    public void TakeDamage(Attack newAttack)
    {
        float armorThrough = newAttack.ArmorThrough;
        float damage = newAttack.Damage;

        int damageLeft = Mathf.FloorToInt(Armor - armorThrough < 0 ? damage + (damage * (armorThrough - Armor) / 100) / 2 : damage - ((Armor - armorThrough) / 100 * damage));

        TakeDamage(damageLeft);

        if (newAttack.DotDuration > 0)
            ApplyDot(newAttack);
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

        if (InformationUI != null)
            InformationUI.UpdateEnemyInformation(this);
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

            if (InformationUI != null)
                InformationUI.UpdateEnemyInformation(this);
        }
    }

    /// <summary>
    /// Coroutine used to delay the slow down reset.
    /// </summary>
    /// <param name="slowDownTime">The slow down time</param>
    /// <returns>Yield the slow down reset time</returns>
    protected IEnumerator ResetSlowDown(float slowDownTime)
    {
        yield return new WaitForSeconds(slowDownTime);
        _isSlowDown = false;
        Speed = _speedMax;

        if (InformationUI != null)
            InformationUI.UpdateEnemyInformation(this);
    }


    /// <summary>
    /// Method used to apply a dot on the enemy.
    /// </summary>
    /// <param name="newAttack">The new dot applied</param>
    protected void ApplyDot(Attack newAttack)
    {
        IsDotted = true;

        if (_dots.Count > 3)
        {
            Attack attackRemoved = _dots[0];
            foreach(Attack attack in _dots)
                if (attack.DotDamage * attack.DotDuration * attack.ArmorThroughMalus < attackRemoved.DotDamage * attackRemoved.DotDuration * attackRemoved.ArmorThroughMalus)
                    attackRemoved = attack;

            _dots.Remove(attackRemoved);
        }

        _dots.Add(newAttack);

        _dotDisplay.SetActive(true);

        if (InformationUI != null)
            InformationUI.UpdateEnemyInformation(this);

        if (isActiveAndEnabled)
            StartCoroutine(TakePersistentDamage(newAttack));
    }


    /// <summary>
    /// Coroutine used take damage from dot.
    /// </summary>
    /// <param name="newDot">The dot applied</param>
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
            _dotDisplay.SetActive(false);
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


    /// <summary>
    /// Method called when a tower destroy a bit of armor each hit.
    /// </summary>
    /// <param name="percentage">The percentage broken by hit</param>
    public void DestroyArmor(int percentage)
    {
        ArmorMax = Mathf.Clamp(ArmorMax - percentage, 0, ArmorMax);
        Armor = Mathf.Clamp(Armor - percentage, 0, Armor);

        if (InformationUI != null)
            InformationUI.UpdateEnemyInformation(this);
    }


    /// <summary>
    /// Method used when the life of an enemy drops below zero or other specific behavior.
    /// </summary>
    /// <param name="reachEnd">Does the enemy reaches the end?</param>
    protected void Die(bool reachEnd)
    {
        _spawner.EnemyKilled();

        if (InformationUI)
            DesactivateUI();

        StopAllCoroutines();

        Controller.Instance.PoolController.In(GetComponent<PoolableObject>());

        if (!reachEnd)
            Controller.Instance.EnemyController.Die(_goldGained);
        else
            Controller.Instance.EnemyController.ReachEnd(_numberOfLivesTaken);
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
    /// Activate or desactivate selector state.
    /// </summary>
    /// <param name="state">The state value</param>
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="attack"></param>
    /// <returns></returns>
    public int DamageTaken(Attack attack)
    {
        float dotDamage = attack.DotDamage * 2 * attack.DotDuration;
        float rawDamage;

        if (Armor - attack.ArmorThrough <= 0)
            rawDamage = attack.Damage + (attack.Damage * (attack.ArmorThrough - Armor) / 100) / 2;
        else
            rawDamage = attack.Damage - ((Armor - attack.ArmorThrough) / 100 * attack.Damage);

        return Mathf.FloorToInt(rawDamage + dotDamage);
    }
}