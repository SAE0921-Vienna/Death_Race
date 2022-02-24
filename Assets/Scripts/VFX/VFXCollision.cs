using System;
using Audio;
using UnityEngine;

namespace VFX
{
    public class VFXCollision : MonoBehaviour
    {
        private ShipVfxController _shipVfxController;

        private void Start()
        {
            _shipVfxController = GetComponentInChildren<ShipVfxController>();
        }

        /* If the ship collides with the wall layer, move the particle system to the collision point and start playing it.
         If it leaves the collision, the particle system will stop playing. */
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Wall")) return;
            
            _shipVfxController.sparkParticleSystem.gameObject.transform.position = collision.contacts[0].point;
            _shipVfxController.sparkParticleSystem.Play(true);

            AudioManager.PlaySound(AudioManager.Sound.HitWall, 0.5f);
        }
    
        private void OnCollisionExit()
        {
            _shipVfxController.sparkParticleSystem.Stop(true);
        }
    }
}