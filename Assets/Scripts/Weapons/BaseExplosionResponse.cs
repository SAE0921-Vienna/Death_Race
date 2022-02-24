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

        public void Explode()
        {
            DealDamage();
            ExplosionAnimation();

        }

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

        private void ExplosionForce(Rigidbody targetRigidbody)
        {
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, forceMode);
        }

        private void ExplosionAnimation()
        {
            boomEffect.transform.parent = null;
            boomEffect.Play();
            boomEffect.gameObject.AddComponent<DestroyParticle>();

            AudioManager.PlaySound(AudioManager.Sound.RocketExplosion, transform.position);

            Destroy(gameObject);
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}