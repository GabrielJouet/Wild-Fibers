using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Description")]
    [SerializeField]
    protected string _displayName;


    [Header("Behaviour Variables")]
    [SerializeField]
    protected float _healthMax;
    protected float _health;
    [SerializeField]
    protected float _armorMax;
    protected float _armor;
    [SerializeField]
    protected float _speedMax;
    protected float _speed;
    [SerializeField]
    protected int _numberOfLivesTaken;
    [SerializeField]
    protected int _goldGained;

    protected float _dotDuration;
    protected float _healthMalus = 0;


    [Header("Display")]
    [SerializeField]
    protected GameObject _selector;
    [SerializeField]
    protected HealthBar _healthBar;
    [SerializeField]
    protected SpriteRenderer _dotDisplay;
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;
    protected Sprite _dotEffect;
    [SerializeField]
    protected Animator _animator;

    protected bool _dotApplied = false;



    protected Path _path;
    protected int _pathIndex;
    protected EnemyPool _enemyPool;
    protected BackgroudSelecter _informationUI;

    protected bool _moving = false;


    public void Initialize(Path newPath, EnemyPool newPool)
    {
        _informationUI = null;

        _dotApplied = false;
        _dotDisplay.sprite = null;
        gameObject.SetActive(true);

        _speed = _speedMax;
        _health = _healthMax;
        _armor = _armorMax;

        _healthBar.ResetSize();

        _pathIndex = 0;

        transform.position = newPath.GetPathPosition(0);
        _path = newPath;
        _enemyPool = newPool;

        _moving = true;
    }


    private void Update()
    {
        if(_moving)
        {
            FollowPath();

            if(_animator)
            {
                _animator.SetBool("horizontal", transform.position.x - _path.GetPath()[_pathIndex].x >= transform.position.y - _path.GetPath()[_pathIndex].y);
                _animator.SetBool("flipped", transform.position.y - _path.GetPath()[_pathIndex].y < 0);
            }

            _spriteRenderer.flipX = transform.position.x - _path.GetPath()[_pathIndex].x > 0;
        }
    }


    private void FixedUpdate()
    {
        if (_informationUI != null)
            _informationUI.UpdateEnemyInformation(this);
    }


    protected void FollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, _path.GetPath()[_pathIndex], Time.deltaTime * _speed);

        if (transform.position == _path.GetPath()[_pathIndex] && _pathIndex + 1 < _path.GetPath().Count)
            _pathIndex++;
        else if (_pathIndex + 1 == _path.GetPath().Count)
        {
            _moving = false;
            ReachEnd();
        }
    }


    public void TakeDamage(float damage, float armorThrough)
    {
        int damageLeft = Mathf.FloorToInt(_armor - armorThrough < 0 ? damage + (damage * (armorThrough - _armor)/100)/2 : damage - ((_armor - armorThrough)/100 * damage));

        if (_health - damageLeft <= 0)
            Die();
        else 
            _health -= damageLeft;

        _healthBar.ChangeSize(_health / _healthMax);
    }


    public void TakeDamage(float damage)
    {
        if (_health - damage <= 0)
            Die();
        else
            _health -= damage;

        _healthBar.ChangeSize(_health / _healthMax);
    }


    public void ApplySlowDown(/*TO DO*/)
    {
        //TO DO
    }


    public void ApplyDot(float armorThroughMalus, float healthMalus, float duration, Sprite newIcon)
    {
        _armor -= armorThroughMalus;
        _healthMalus += healthMalus;
        _dotDuration = duration;
        _dotDisplay.sprite = newIcon;

        if(!_dotApplied)
            StartCoroutine(TakePersistentDamage());
    }


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


    public void ApplyMark(/*TO DO*/)
    {
        //TO DO
    }


    public void TeleportBack(/*TO DO*/)
    {
        //TO DO
    }


    protected void Die()
    {
        _enemyPool.AddOneEnemy(gameObject, false, _goldGained);

        if(_informationUI)
        {
            _informationUI.ErasePreviousEnemy();
            _informationUI.DisableEnemyInformation();
            _selector.SetActive(false);
        }

        StopAllCoroutines();
    }


    protected void ReachEnd()
    {
        _enemyPool.AddOneEnemy(gameObject, true, _numberOfLivesTaken);

        StopAllCoroutines();
    }


    public void SetInformationUI(BackgroudSelecter newUI)
    {
        _informationUI = newUI;
    }


    public void Pause(bool paused)
    {
        _animator.enabled = paused;
    }


    public string GetName() { return _displayName; }

    public float GetHealth() { return _health; }

    public float GetMaxHealth() { return _healthMax; }

    public float GetArmor() { return _armor; }

    public float GetMaxArmor() { return _armorMax; }

    public float GetSpeed() { return _speed; }

    public float GetMaxSpeed() { return _speedMax; }

    public int GetNumberOfLivesTaken() { return _numberOfLivesTaken; }

    public float GetPathPercentage() { return _pathIndex / _path.GetPath().Count; }

    public void SetSelector() { _selector.SetActive(true); }

    public void ResetSelector() { _selector.SetActive(false); }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TowerCollider towerCollider))
            towerCollider.EnemyCollide(this);
    }


    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TowerCollider towerCollider))
            towerCollider.EnemyExit(this);
    }
}