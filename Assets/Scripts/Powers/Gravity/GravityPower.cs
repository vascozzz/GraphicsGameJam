using UnityEngine;
using System.Collections;

public class GravityPower : Power 
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileForce;

    private GravityProjectileController shotProjectile;
    
    public override void Step() 
    {
        if (Input.GetButtonDown("Fire" + playerController.GetPlayer()))
        {
            powerController.UsedPower();
            SpawnProjectile();
        }

        if (Input.GetButtonUp("Fire" + playerController.GetPlayer()))
        {
            if (shotProjectile != null)
                shotProjectile.Explode();
        }
	}


    public void SpawnProjectile()
    {
        Vector2 direction = playerController.GetShotDirection();

        GameObject projectileObj = GameObject.Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody2D>().AddForce(direction * projectileForce);

        shotProjectile = projectileObj.GetComponent<GravityProjectileController>();
    }
}
