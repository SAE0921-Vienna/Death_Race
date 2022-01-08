using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    [SerializeField] private float durationMultiplier = 10f;

    private void Update()
    {
        Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().main.duration * durationMultiplier);
    }
    
}