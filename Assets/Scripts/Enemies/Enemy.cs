using System;
using System.Collections;
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


    [Header("Behaviour Variables")]
    //Health max
    [SerializeField]
    protected float _healthMax;
    //Current health
    protected float _health;

    //Armor max
    [SerializeField]
    protected float _armorMax;
    //Current armor
    protected float _armor;

    //Speed max
    [SerializeField]
    protected float _speedMax;
    //Current speed
    protected float _speed;

    //Number of lives removed if the enemy reaches the end
    [SerializeField]
    protected int _numberOfLivesTaken;

    //Money gained when the enemy is killed
    [SerializeField]
    protected int _goldGained;

    //Does the enemy flies?
    [SerializeField]
    protected bool _flying;

    [SerializeField]
    protected Transform _damagePosition;


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

    //Does the enemy currently has a dot effect?
    protected bool _dotApplied = false;


    //Current path used in enemy movement
    protected Path _path;

    //Index on the path used
    protected int _pathIndex;

    //Enemy pool where the enemy came from
    protected EnemyPool _enemyPool;

    //All information UI
    protected BackgroudSelecter _informationUI;


    private DateTime _dotCoroutineStartTime;

    private float _dotCoroutineTimeNeeded = 0f;

    private bool _enemyPaused = false;

    private DateTime _slowDownCoroutineStartTime;

    private float _slowDownCoroutineTimeNeeded = 0f;



    //Method used instead of using start (kinda a constructor)
    //
    //Parameters => newPath, the new path the enemy will used
    //              newPool, the pool enemy came from
    public virtual void Initialize(Path newPath, EnemyPool newPool, int pathIndex)
    {
        //We reset pretty every variables
        _informationUI = null;

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

        transform.position = newPath.GetPathPosition(pathIndex);
        _path = newPath;
        _enemyPool = newPool;

        _moving = true;
    }


    //Update method, called each frame
    protected void Update()
    {
        if(_moving)
        {
            FollowPath();

            //TO ADD WHEN FACE AND BACK ANIMATIONS ARE DONE
            /*if(_animator)
            {
                _animator.SetBool("horizontal", transform.position.x - _path.GetPath()[_pathIndex].x >= transform.position.y - _path.GetPath()[_pathIndex].y);
                _animator.SetBool("flipped", transform.position.y - _path.GetPath()[_pathIndex].y < 0);
            }*/
        }
    }


    //Fixed Update method, called 50 times per second
    protected void FixedUpdate()
    {
        if (_informationUI != null)
            _informationUI.UpdateEnemyInformation(this);
    }



    //Method used to follow cezier path 
    protected void FollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, _path.GetPath()[_pathIndex], Time.deltaTime * _speed);

        //If the enemy position is reaching the end of the path part we increase the path index
        if (transform.position == _path.GetPath()[_pathIndex] && _pathIndex + 1 < _path.GetPath().Count)
            _pathIndex++;
        //Else if the enemy reaches the end of the path
        else if (_pathIndex + 1 == _path.GetPath().Count)
        {
            _moving = false;
            ReachEnd();
        }

        _spriteRenderer.flipX = transform.position.x - _path.GetPath()[_pathIndex].x > 0;
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


    protected IEnumerator ResetSlowDown(float slowDownTime)
    {
        _slowDownCoroutineStartTime = DateTime.Now;
        _slowDownCoroutineTimeNeeded = 0;
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
            _dotCoroutineStartTime = DateTime.Now;
            _dotCoroutineTimeNeeded = 0.5f;
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
        if (_informationUI)
            DesactivateUI();

        StopAllCoroutines();

        _enemyPool.AddOneEnemy(this, false, _goldGained);
    }


    //Method called when an enemy reaches the end of the path
    protected void ReachEnd()
    {
        _enemyPool.AddOneEnemy(this, true, _numberOfLivesTaken);

        if (_informationUI) 
            DesactivateUI();

        StopAllCoroutines();
    }


    //Method used to desactivate tracked UI
    protected void DesactivateUI()
    {
        _informationUI.ErasePreviousEnemy();
        _informationUI.DisableEnemyInformation();
        _selector.SetActive(false);

        _informationUI = null;
    }


    public void Pause(bool paused)
    {
        _animator.enabled = paused;

        if (!_enemyPaused)
        {
            StopAllCoroutines();
            _dotCoroutineTimeNeeded -= (float)(DateTime.Now - _dotCoroutineStartTime).TotalSeconds;
            _slowDownCoroutineTimeNeeded -= (float)(DateTime.Now - _slowDownCoroutineStartTime).TotalSeconds;
        }
        else if(_slowDownCoroutineTimeNeeded != 0 || _dotCoroutineTimeNeeded != 0)
        {
            if (_dotApplied)
                StartCoroutine(UnPauseDotDelay());
            if (_isSlowDown)
                StartCoroutine(UnPauseSlowDelay());
        }
        _enemyPaused = !_enemyPaused;
    }


    //Method to delay action when pause is unpaused
    protected IEnumerator UnPauseDotDelay()
    {
        _dotCoroutineStartTime = DateTime.Now;
        yield return new WaitForSeconds(_dotCoroutineTimeNeeded);
        _dotDisplay.sprite = null;
        _dotApplied = false;
        _armor = _armorMax;
        _dotCoroutineTimeNeeded = 0f;
    }


    //Method to delay action when pause is unpaused
    protected IEnumerator UnPauseSlowDelay()
    {
        _slowDownCoroutineStartTime = DateTime.Now;
        yield return new WaitForSeconds(_slowDownCoroutineTimeNeeded);
        _isSlowDown = false;
        _speed = _speedMax;
        _dotCoroutineTimeNeeded = 0f;
    }


    //Getters
    public string GetName() { return _displayName; }

    public float GetHealth() { return _health; }

    public float GetMaxHealth() { return _healthMax; }

    public float GetArmor() { return _armor; }

    public float GetMaxArmor() { return _armorMax; }

    public float GetSpeed() { return _speed; }

    public float GetMaxSpeed() { return _speedMax; }

    public int GetNumberOfLivesTaken() { return _numberOfLivesTaken; }

    public float GetPathPercentage() { return _pathIndex / _path.GetPath().Count; }

    public bool GetFlying() { return _flying; }

    public Vector2 GetDamagePosition() { return _damagePosition.position; }


    //Setters
    public void SetSelector() { _selector.SetActive(true); }

    public void ResetSelector() { _selector.SetActive(false); }


    public void SetInformationUI(BackgroudSelecter newUI) { _informationUI = newUI; }



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