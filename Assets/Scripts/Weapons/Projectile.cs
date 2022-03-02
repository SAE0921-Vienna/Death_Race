using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    bool instaTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!instaTrigger)
            StartCoroutine(CheckTimer());
        else
        {
            //Do VFX
            Destroy(gameObject);
        }
    }
    public IEnumerator CheckTimer()
    {
        yield return new WaitForSeconds(0.5f);
        instaTrigger = true;
    }
}
