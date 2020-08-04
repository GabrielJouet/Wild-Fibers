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


    [Header("Selection")]
    [SerializeField]
    protected GameObject _selector;

    [SerializeField]
    protected HealthBar _healthBar;


    protected Path _path;
    protected int _pathIndex;
    protected EnemyPool _enemyPool;


    protected bool _moving = false;


    public void Initialize(Path newPath, EnemyPool newPool)
    {
        gameObject.SetActive(true);

        _healthBar.ResetSize();

        _speed = _speedMax;
        _health = _healthMax;

        _pathIndex = 0;

        transform.position = newPath.GetPathPosition(0);
        _path = newPath;
        _enemyPool = newPool;

        _moving = true;
    }


    private void Update()
    {
        if(_moving)
            FollowPath();
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

        GetComponent<SpriteRenderer>().flipX = transform.position.x - _path.GetPath()[_pathIndex].x > 0;
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


    public void ApplySlowDown(/*TO DO*/)
    {
        //TO DO
    }


    public void ApplyDot(/*TO DO*/)
    {
        //TO DO
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
    }


    protected void ReachEnd()
    {
        _enemyPool.AddOneEnemy(gameObject, true, _numberOfLivesTaken);
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