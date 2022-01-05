using UnityEngine;

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
            var allHitTargets = Physics.SphereCastAll(transform.position, explosionRadius, Vector3.up, 1f);
            foreach (var target in allHitTargets)
            {
                if (target.collider.GetComponent<PlayerManager>() != null)
                    target.collider.GetComponent<PlayerManager>().health -= explosionDamage;
                
                if (target.rigidbody != null) 
                    ExplosionForce(target.rigidbody);
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
            
            Destroy(gameObject);
        }
    }
}