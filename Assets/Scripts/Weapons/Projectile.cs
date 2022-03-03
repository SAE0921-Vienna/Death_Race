using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    bool instaTrigger = false;
    [SerializeField]
    private GameObject impactFX;

    /// <summary>
    /// Adds effects on impact
    /// </summary>
    /// <param name="other"></param>
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
    /// <summary>
    /// Buffer so the projectile cannot collide instantaneously
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckTimer()
    {
        yield return new WaitForSeconds(0.05f);
        instaTrigger = true;
    }
}
