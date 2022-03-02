using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    bool instaTrigger = false;
    [SerializeField]
    private GameObject impactFX;

    private void OnTriggerEnter(Collider other)
    {
        if (!instaTrigger)
            StartCoroutine(CheckTimer());
        else
        {
            Instantiate(impactFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public IEnumerator CheckTimer()
    {
        yield return new WaitForSeconds(0.05f);
        instaTrigger = true;
    }
}
