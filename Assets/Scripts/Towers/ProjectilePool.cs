using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    //Base enemy prefab
    public Projectile Projectile { get; private set; }

    //List of non used enemies but yet instanciated and desactivated
    private readonly Stack<Projectile> _projectilePool = new Stack<Projectile>();



    //Method used instead of using start (kinda a constructor)
    //
    //Parameters => newPrefab, the new projectile prefab used in spawn
    public void Initialize(GameObject newPrefab)
    {
        Projectile = newPrefab.GetComponent<Projectile>();
    }


    //Method used to recover one projectile
    //
    //Return a new projectile either already instanciated or newly created
    public Projectile GetOneProjectile()
    {
        Projectile newProjectile = _projectilePool.Count > 0 ? _projectilePool.Pop() : Instantiate(Projectile, transform);
        newProjectile.gameObject.SetActive(true);

        return newProjectile;
    }



    //Method used to add one projectile to the pool
    //
    //Parameters => newProjectile, game object of the projectile
    public void AddOneProjectile(Projectile newProjectile)
    {
        //We desactivate it
        newProjectile.gameObject.SetActive(false);
        _projectilePool.Push(newProjectile);
    }
}
