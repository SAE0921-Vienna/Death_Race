using System;
using UnityEngine;

namespace MISC
{
    public class DestroyParticleCustom : MonoBehaviour
    {
        [SerializeField] private float destroyTimer;

        private void Start()
        {
            DestroyParticle();
        }

        private void DestroyParticle()
        {
            Destroy(gameObject, destroyTimer);
        }
    }
}