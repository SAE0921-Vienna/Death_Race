using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    [SerializeField] private float durationMultiplier = 10f;

    public void DestroyParticleGameobject()
    {
        Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().main.duration * durationMultiplier);
    }
}