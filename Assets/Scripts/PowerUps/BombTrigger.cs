using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    private PlayerStats playerstats;
    private ParticleSystem boomEffect;
    public bool hasBeenActivated = false;
    public float bombTimer;


    private void Awake()
    {
        boomEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        boomEffect.Stop();
        playerstats = FindObjectOfType<PlayerStats>();
        bombTimer = playerstats.bombTimer;
    }

    private void Update()
    {
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0 && hasBeenActivated)
        {
            boomEffect.transform.parent = null;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenActivated && collision.transform.tag == "Player")
        {
            boomEffect.transform.parent = null;
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        boomEffect.Play();
        boomEffect.gameObject.AddComponent<DestroyParticle>();        
    }

}
