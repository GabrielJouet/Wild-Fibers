using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    //Base enemy prefab
    private Projectile _projectilePrefab;

    //List of non used enemies but yet instanciated and desactivated
    private readonly Stack<Projectile> _projectilePool = new Stack<Projectile>();



    //Method used instead of using start (kinda a constructor)
    //
    //Parameters => newPrefab, the new projectile prefab used in spawn
    public void Initialize(GameObject newPrefab)
    {
        _projectilePrefab = newPrefab.GetComponent<Projectile>();
    }


    //Method used to recover one enemy
    //
    //Return a new enemy either already instanciated or newly created
    public Projectile GetOneProjectile()
    {
        Projectile newProjectile = _projectilePool.Count > 0 ? _projectilePool.Pop() : Instantiate(_projectilePrefab, transform);
        newProjectile.gameObject.SetActive(true);

        return newProjectile;
    }



    //Method used to add one enemy to the pool
    //
    //Parameters => newProjectile, game object of the projectile
    public void AddOneProjectile(Projectile newProjectile)
    {
        //We desactivate it
        newProjectile.gameObject.SetActive(false);
        _projectilePool.Push(newProjectile);
    }


    //Getter
    public Projectile GetPrefab() { return _projectilePrefab; }
}
