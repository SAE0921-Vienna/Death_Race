using Audio;
using UnityEngine;
using AI;

namespace Weapons
{
    public class BaseExplosionResponse : MonoBehaviour, IExplosion
    {
        [SerializeField] private int explosionDamage;
        [SerializeField] private float explosionForce, explosionRadius, upwardsModifier;
        [SerializeField] private ForceMode forceMode;
        [SerializeField] private ParticleSystem boomEffect;
        [SerializeField] private AudioSource explosionSound;
        [SerializeField] private bool destroyObject = true;

        public void Explode()
        {
            DealDamage();
            ExplosionAnimation();
        }

        /// <summary>
        /// Deals damage to any vehicle in the area
        /// </summary>
        private void DealDamage()
        {
            var allHitTargets = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var target in allHitTargets)
            {
                if (target.GetComponentInParent<IDamageable>() != null)
                    target.GetComponentInParent<IDamageable>().GetDamage(explosionDamage);

                if (target.gameObject.GetComponentInParent<Rigidbody>() != null)
                    ExplosionForce(target.gameObject.GetComponentInParent<Rigidbody>());
            }
        }

        /// <summary>
        /// Inflicts an explosive force on any vehicle in the area
        /// </summary>
        /// <param name="targetRigidbody"></param>
        private void ExplosionForce(Rigidbody targetRigidbody)
        {
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, forceMode);
        }

        /// <summary>
        /// Creates an explosion effects
        /// </summary>
        private void ExplosionAnimation()
        {
            if (boomEffect == null) return;
            boomEffect.transform.parent = null;
            boomEffect.Play();
            boomEffect.gameObject.AddComponent<DestroyParticle>();
            explosionSound.Play();

            if (destroyObject)
                Destroy(gameObject);
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}