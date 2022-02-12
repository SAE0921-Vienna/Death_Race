using System.Collections;
using System.Collections.Generic;
using PlayerController;
using UnityEngine;

public class ShipVfxController : MonoBehaviour
{

    /*Controller Responsible for Various VFX on the Ship*/
    
    [Header("Thrusters")]
    [SerializeField] private ParticleSystem[] thrusterParticleSystems;
    [SerializeField] [Range(0, 20)] private float minLifeTime, maxLifeTime;
    [SerializeField] private AnimationCurve thrusterAnimationCurve;
    
    [Header("Trails")]
    [SerializeField] private TrailRenderer[] trailRenderers;
    [SerializeField] private Color trailStartColor, trailEndColor;
    
    [Header("Collision Sparks")]
    [SerializeField] private ParticleSystem sparkParticleSystem;
    
    private VehicleController _vehicleController;
    
    private void Awake()
    {
        _vehicleController = GetComponentInParent<VehicleController>();
    }

    private void Update()
    {
        ThrusterController();
        WingTrailController();
    }

    /// <summary>
    /// Changes the Start-Lifetime of the particle system for the thrusters, depending on the ships speed.
    /// </summary>
    private void ThrusterController()
    {
        if (thrusterParticleSystems == null)
        {
            Debug.LogWarning("Thruster Particle System not found!");
            return;
        }
        foreach (var thruster in thrusterParticleSystems)
        {
            var main = thruster.main;
            main.startLifetime = Mathf.Lerp(minLifeTime, maxLifeTime, thrusterAnimationCurve.Evaluate(_vehicleController.currentSpeed));
        }
    }

    /// <summary>
    /// Changes the color of the Trails on the Ships Wings depending on the ships speed.
    /// </summary>
    private void WingTrailController()
    {
        if (trailRenderers == null)
        {
            Debug.LogWarning("Trail Renderers not found!");
            return;
        }
        foreach (var trail in trailRenderers)
        {
            trail.startColor = Color.Lerp(trailStartColor, trailEndColor, _vehicleController.currentSpeed);
            trail.endColor = Color.Lerp(trailStartColor, trailEndColor, _vehicleController.currentSpeed);
        }
    }
    
    /* If the ship collides with the wall layer, move the particle system to the collision point and start playing it.
     If it leaves the collision, the particle system will stop playing. */
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
