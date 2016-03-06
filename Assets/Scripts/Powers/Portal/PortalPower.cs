using UnityEngine;
using System.Collections;

public class PortalPower : Power 
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject portal0;
    [SerializeField] private GameObject portal1;
    [SerializeField] private float teleportCooldown = 1f;
    [SerializeField] private float projectileForce = 150f;

    private GameObject portal0Obj, portal1Obj;
    private bool portal0Spawned, portal1Spawned;

    private Hashtable travelers;

    protected override void Start()
    {
        base.Start();

        portal0Spawned = false;
        portal1Spawned = false;

        travelers = new Hashtable();
    }

	public override void Step() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            powerController.UsedPower();
            SpawnProjectile(portal0);
        }

        else if (Input.GetMouseButtonDown(1))
        {
            powerController.UsedPower();
            SpawnProjectile(portal1);
        }
	}

    void SpawnProjectile(GameObject portal)
    {
        Vector3 pos = GetMouseAsWorldCoords();
        Vector3 direction = (pos - transform.position).normalized;

        GameObject projectileObj = GameObject.Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody2D>().AddForce(direction * projectileForce);

        PortalProjectileController ctrl = projectileObj.GetComponent<PortalProjectileController>();
        ctrl.portal = portal;
        ctrl.portalPower = this;
    }

    public void OnProjectileCollision(GameObject portal, Vector2 pos)
    {
        if (portal == portal0)
        {
            if (!portal0Spawned)
            {
                portal0Obj = GameObject.Instantiate(portal0, pos, Quaternion.identity) as GameObject;
                portal0Obj.GetComponent<PortalController>().portalPower = this;
                portal0Spawned = true;
            }
            else
            {
                portal0Obj.transform.position = pos;
            }
        }

        if (portal == portal1)
        {
            if (!portal1Spawned)
            {
                portal1Obj = GameObject.Instantiate(portal1, pos, Quaternion.identity) as GameObject;
                portal1Obj.GetComponent<PortalController>().portalPower = this;
                portal1Spawned = true;
            }
            else
            {
                portal1Obj.transform.position = pos;
            }
        }
    }

    public void OnPortalEnter(GameObject portal, GameObject hit)
    {
        // check cooldown for hit object
        if (travelers.ContainsKey(hit))
        {
            if (Time.time - (float) travelers[hit] < teleportCooldown)
            {
                return;
            }
        }
        else
        {
            travelers.Add(hit, Time.time);     
        }

        // if object is able to teleport
        if (portal == portal0Obj)
        {
            if (portal1Spawned)
            {
                hit.gameObject.transform.position = portal1Obj.transform.position;
                travelers[hit] = Time.time;
            }
        }
        else if (portal == portal1Obj)
        {
            if (portal0Spawned)
            {
                hit.gameObject.transform.position = portal0Obj.transform.position;
                travelers[hit] = Time.time;
            }
        }
    }
}
