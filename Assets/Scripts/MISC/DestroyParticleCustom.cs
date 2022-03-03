using UnityEngine;

namespace MISC
{
    public class DestroyParticleCustom : MonoBehaviour
    {
        [SerializeField] private float destroyTimer;
        
        public void DestroyParticle()
        {
            Destroy(gameObject, destroyTimer);
        }
    }
}