using UnityEngine;

namespace Weapons
{
    class ExplosionTrigger : MonoBehaviour
    {
        private IExplosion _explosion;

        private void Awake()
        {
            _explosion = GetComponent<IExplosion>();
        }

        private void OnCollisionEnter(Collision other)
        {
            //if (other.collider.CompareTag("Player")) return;
            //if (other.collider.CompareTag("Bullet")) return;

            print("Collision Detected");
            if(other.gameObject.layer == 9 || other.gameObject.layer == 10 || other.gameObject.layer == 8 || other.gameObject.layer == 6)
            {
                _explosion.Explode();

            }
        }
    }
}