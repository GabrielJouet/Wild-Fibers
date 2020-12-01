using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class that handles every enemy beahvior
 */
public class Enemy : MonoBehaviour
{
    [Header("Description")]
    //Name display in the UI
    [SerializeField]
    protected string _displayName;
    public string Name { get => _displayName; }


    [Header("Behaviour Variables")]
    //Health max
    [SerializeField]
    protected float _healthMax;
    public float HealthMax { get => _healthMax; }
    
    //Current health
    protected float _health;
    public float Health { get => _health; }

    //Armor max
    [SerializeField]
    protected float _armorMax;
    //Current armor
    protected float _armor;
    public float Armor { get => _armor; }

    //Speed max
    [SerializeField]
    protected float _speedMax;
    //Current speed
    protected float _speed;

    //Number of lives removed if the enemy reaches the end
    [SerializeField]
    protected int _numberOfLivesTaken;
    public int LivesTaken { get; }

    //Money gained when the enemy is killed
    [SerializeField]
    protected int _goldGained;

    //Does the enemy flies?
    [SerializeField]
    protected bool _flying;
    public bool Flying { get; }

    [SerializeField]
    protected Transform _damagePosition;
    public Vector2 DamagePosition { get => _damagePosition.position; }


    //Dot duration in seconds
    protected float _dotDuration;

    //Dot damage per half second
    protected float _healthMalus = 0;

    //Does the enemy is moving?
    protected bool _moving = false;

    protected bool _isSlowDown = false;


    [Header("Display")]
    //Selector object
    [SerializeField]
    protected GameObject _selector;

    //Health bar script
    [SerializeField]
    protected HealthBar _healthBar;

    //Dot display sprite renderer
    [SerializeField]
    protected SpriteRenderer _dotDisplay;

    //Sprite renderer component
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    //Sprite used in dot effect
    protected Sprite _dotEffect;
    [SerializeField]
    protected Animator _animator;


    [Header("Particle")]
    [SerializeField]
    protected Particle _damageParticle;

    [SerializeField]
    protected List<Transform> _particleEmissionsPoints;

    [SerializeField]
    protected int _numberOfParticles;
    protected ParticleController _particleController;


    //Does the enemy currently has a dot effect?
    protected bool _dotApplied = false;


    //Current path used in enemy movement
    protected List<Vector2> _path;

    //Index on the path used
    protected int _pathIndex;

    public float PathRatio { get => (float)(_pathIndex / _path.Count); }

    //Enemy pool where the enemy came from
    protected EnemyPool _enemyPool;

    //All information UI
    public BackgroudSelecter InformationUI { get; set; }

    public bool AlreadyAimed { get; set; } = false;



    //Method used instead of using start (kinda a constructor)
    //
    //Parameters => newPath, the new path the enemy will used
    //              newPool, the pool enemy came from
    public virtual void Initialize(List<Vector2> newPath, EnemyPool newPool, int pathIndex)
    {
        if (_particleController == null)
            _particleController = FindObjectOfType<ParticleController>();

        //We reset every variable
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
        _enemyPool = newPool;

        _moving = true;
        AlreadyAimed = false;
    }


    //Update method, called each frame
    protected void Update()
    {
        if(_moving)
            FollowPath();
    }


    //Fixed Update method, called 50 times per second
    protected void FixedUpdate()
    {
        if (InformationUI != null)
            InformationUI.UpdateEnemyInformation(this);
    }



    //Method used to follow cezier path 
    protected void FollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, _path[_pathIndex], Time.deltaTime * _speed);

        //If the enemy position is reaching the end of the path part we increase the path index
        if ((Vector2)transform.position == _path[_pathIndex] && _pathIndex + 1 < _path.Count)
            _pathIndex++;
        //Else if the enemy reaches the end of the path
        else if (_pathIndex + 1 == _path.Count)
            ReachEnd();

        _spriteRenderer.flipX = transform.position.x - _path[_pathIndex].x > 0;
    }


    //Method used to remove life on enemy
    //
    //Parameters => damage, amount of damage into the enemy
    //              armorThrough, percentage of destroyed armor 
    public void TakeDamage(float damage, float armorThrough)
    {
        //If armor through is higher than armor the damage is higher, else it is lower
        int damageLeft = Mathf.FloorToInt(_armor - armorThrough < 0 ? damage + (damage * (armorThrough - _armor)/100)/2 : damage - ((_armor - armorThrough)/100 * damage));

        //If the health is dropping below 0 the enemy die
        if (_health - damageLeft <= 0)
            Die();
        else 
            _health -= damageLeft;

        foreach (Transform current in _particleEmissionsPoints)
        {
            foreach (Particle particle in _particleController.GetParticle(_damageParticle, _numberOfParticles))
                particle.Initialize(current.position);
        }

        //And we change health bar size
        _healthBar.ChangeSize(_health / _healthMax);
    }


    //Method used to remove life on enemy without armor related
    //
    //Parameter => damage, amount of damage taken
    public void TakeDamage(float damage)
    {
        if (_health - damage <= 0)
            Die();
        else
            _health -= damage;

        _healthBar.ChangeSize(_health / _healthMax);
    }


    //Method used to apply a slow down on enemies
    public void ApplySlowDown(float slowDownRatio, float slowDownTime)
    {
        if(!_isSlowDown)
        {
            _isSlowDown = true;
            StartCoroutine(ResetSlowDown(slowDownTime));
            _speed = _speedMax * (100 - slowDownRatio) / 100f;
        }
    }


    //Coroutine used to reset slow down once it started
    protected IEnumerator ResetSlowDown(float slowDownTime)
    {
        yield return new WaitForSeconds(slowDownTime);
        _isSlowDown = false;
        _speed = _speedMax;
    }


    //Method used to apply a dot (damage over time) on enemies
    //
    //Parameters => armorThroughMalus, amount of armor reduced
    //              healthMalus, amount of health lost every half second
    //              duration, duration of the dot
    //              newIcon, dot icon on enemy
    public void ApplyDot(float armorThroughMalus, float healthMalus, float duration, Sprite newIcon)
    {
        _healthMalus = healthMalus;
        _dotDuration = duration;
        _dotDisplay.sprite = newIcon;

        if(!_dotApplied)
        {
            StartCoroutine(TakePersistentDamage());
            _armor -= armorThroughMalus;
        }
    }


    //Coroutine used to take damage from dot at a half second rate
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


    //Method used to teleport back enemies 
    public void TeleportBack(/*TO DO*/)
    {
        //TO DO
    }


    //Method called when the enemy health drops below 0
    protected void Die()
    {
        if (InformationUI)
            DesactivateUI();

        StopAllCoroutines();

        _enemyPool.AddOneEnemy(this, false, _goldGained);
    }


    //Method called when an enemy reaches the end of the path
    protected void ReachEnd()
    {
        _enemyPool.AddOneEnemy(this, true, _numberOfLivesTaken);

        if (InformationUI) 
            DesactivateUI();

        StopAllCoroutines();
    }


    //Method used to desactivate tracked UI
    protected void DesactivateUI()
    {
        InformationUI.ErasePreviousEnemy();
        InformationUI.DisableEnemyInformation();
        _selector.SetActive(false);

        InformationUI = null;
    }


    public bool CanSurvive(float damage, float armorThrough) { return _health - Mathf.FloorToInt(_armor - armorThrough < 0 ? damage + (damage * (armorThrough - _armor) / 100) / 2 : damage - ((_armor - armorThrough) / 100 * damage)) > 0; }


    public void SetSelector(bool state) 
    { 
        _selector.SetActive(state);
    }



    //Collision method called when the enemy enters a trigger
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TowerCollider towerCollider))
            towerCollider.EnemyCollide(this);
    }


    //Collision method called when the enemy exits a trigger
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TowerCollider towerCollider))
            towerCollider.EnemyExit(this);
    }
}