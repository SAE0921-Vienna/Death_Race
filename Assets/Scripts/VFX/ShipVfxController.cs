using System;
using PlayerController;
using UnityEditor;
using UnityEngine;

namespace VFX
{
    public class ShipVfxController : MonoBehaviour
    {
        /*Controller Responsible for Various VFX on the Ship*/
        [Header("Thrusters")]
        [SerializeField] private ParticleSystem[] thrusterParticleSystems;
        [SerializeField] [Range(0, 5)] private float minLifeTime, maxLifeTime;
        [SerializeField] private AnimationCurve thrusterAnimationCurve;
        
        [Header("Trails")]
        [SerializeField] private TrailRenderer[] trailRenderers;
        [SerializeField] private Color trailStartColor, trailEndColor;
        
        [Header("Collision Sparks")]
        public ParticleSystem sparkParticleSystem;
        
        private VehicleController _vehicleController;
        private AIFollowCurve _aiFollowCurve;
        private float t;
        
        private void Awake()
        {
            _vehicleController = GetComponentInParent<VehicleController>();
            _aiFollowCurve = GetComponentInParent<AIFollowCurve>();
        }

        private void Start()
        {
            gameObject.layer = transform.root.gameObject.layer;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = transform.root.gameObject.layer;
            }
        }

        private void Update()
        {
            ThrusterController();
            WingTrailController();
        }

        private float SetInterpolator()
        {
            if (_vehicleController != null)
            {
                t = _vehicleController.currentSpeed;
            }
            else
            {
                t = _aiFollowCurve.GetCurrentSpeed();
            }
            return t;
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
                main.startLifetime = Mathf.Lerp(minLifeTime, maxLifeTime, thrusterAnimationCurve.Evaluate(SetInterpolator()));
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
                trail.startColor = Color.Lerp(trailStartColor, trailEndColor, SetInterpolator());
                trail.endColor = Color.Lerp(trailStartColor, trailEndColor, SetInterpolator());
            }
        }
    }
}
