using UnityEngine;

namespace Core
{
    public class SoundDestroyer : MonoBehaviour
    {
        public void DestroySoundObject(GameObject soundObject, float destructionTimer)
        {
            if (soundObject.GetComponent<AudioSource>() == null) return;
            Destroy(soundObject, destructionTimer);
        }
    }
}