using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected ProjectilePool _projectilePool;


    //Method used to stop wave and brings it back to parent tower
    protected void StopProjectile()
    {
        gameObject.SetActive(false);
        _projectilePool.AddOneProjectile(this);
    }
}
