using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SparkCollisionParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem sparkParticleSystem;

    [SerializeField] private LayerMask _wallLayerMask;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Wall")) return;
        
        sparkParticleSystem.gameObject.transform.position = collision.contacts[0].point;
        sparkParticleSystem.Play(true);

        print("Collided with wall");
    }

    private void OnCollisionExit()
    {
        sparkParticleSystem.Stop(true);

        print("stopped colliding with wall");
    }
}
