using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class AIShipWeapon : ShipWeapon
{
    private AIManager aIManager;
    protected override void Awake()
    {
        aIManager = GetComponent<AIManager>();
    }
    
    public override void Shoot()
    {
        if (!aIManager.canShoot) return;

        if (Time.time > nextFire)
        {
            Debug.Log("NEXTFIRE" + nextFire);
            nextFire = Time.time + 1 / fireRate;

            ammoSize -= 1;

            var tempobj = HitTarget();
            if (tempobj != null && tempobj != this)
            {
                tempobj.GetComponent<IDamageable>().GetDamage(projectileDamage);
                Debug.Log("Ich Gengner SCHADEN");
            }
            else
            {
                Debug.Log("MAMAMAAAAAA");
            }
        }
        //Projectile instanzieren?
    }

    protected override void RotateWeapon()
    {
        throw new System.NotImplementedException();
    }
}
