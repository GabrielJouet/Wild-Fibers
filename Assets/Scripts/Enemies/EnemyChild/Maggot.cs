using System.Collections;
using UnityEngine;

public class Maggot : Enemy
{
    [Header("Hatchling related")]
    [SerializeField]
    protected float _hatchingTime;

    [SerializeField]
    protected float _cocoonTime;

    [SerializeField]
    protected Enemy _hatchling;


    protected LevelController _levelController;



    public override void Initialize(Path newPath, EnemyPool newPool, int pathIndex)
    {
        base.Initialize(newPath, newPool, pathIndex);
        _levelController = FindObjectOfType<LevelController>();

        StartCoroutine(DelayHatch());
    }


    //Update method, called each frame
    protected new void Update()
    {
        if (_moving)
            FollowPath();
    }



    protected IEnumerator DelayHatch()
    {
        yield return new WaitForSeconds(_hatchingTime);
        _moving = false;
        _animator.SetTrigger("cocoon");

        if (_dotApplied)
            _armor = _armor * 3 - (_armorMax - _armor);
        else
            _armor *= 3;

        _armorMax *= 3;
        yield return new WaitForSeconds(_cocoonTime);
        Enemy hatchling = _levelController.RecoverPool(_hatchling).GetOneEnemy();
        hatchling.Initialize(_path, _enemyPool, _pathIndex);
        _goldGained = 0;
        Die();
    }
}
