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


    protected Path _path;
    protected int _pathIndex;

    protected RessourceController _ressourceController;
    protected EnemiesController _enemiesController;
    protected InformationUIController _informationUiController; 


    protected bool _moving = false;


    public void Initialize(Path newPath, RessourceController newRessourceController, EnemiesController newEnemiesController, InformationUIController newInformationUIController)
    {
        gameObject.SetActive(true);

        _speed = _speedMax;
        _pathIndex = 0;

        transform.position = newPath.GetPathPosition(0);
        _path = newPath;

        _ressourceController = newRessourceController;
        _enemiesController = newEnemiesController;
        _informationUiController = newInformationUIController;

        _moving = true;
    }



    public void ResetEnemy()
    {
        gameObject.SetActive(false);
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
    }


    protected void ReachEnd()
    {
        _ressourceController.RemoveLives(_numberOfLivesTaken);
        ResetEnemy();
        _enemiesController.AddOneEnemy(this);
    }


    public void TakeDamage(float damage)
    {
        //TO DO
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
        //TO DO
        _ressourceController.AddGold(_goldGained);
        ResetEnemy();
        _enemiesController.AddOneEnemy(this);
    }



    private void OnMouseDown()
    {
        _informationUiController.ResetEnemies();

        _informationUiController.SetEnemyInformation(_displayName, Mathf.FloorToInt(_health), Mathf.FloorToInt(_healthMax), _armorMax, _numberOfLivesTaken);
        _selector.SetActive(true);
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

    public void ResetSelector()
    {
        _selector.SetActive(false);
    }
}