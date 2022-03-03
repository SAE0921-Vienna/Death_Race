using UnityEngine;
using Audio;

public class VehicleSoundAI : VehicleSoundManager
{

    AIFollowCurve aiFollowCurve;
    
    private void Awake()
    {
        aiFollowCurve = GetComponentInParent<AIFollowCurve>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_audioSource == null) return;
        _audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, aiFollowCurve.speed);
        _audioSource.volume = Mathf.Lerp(minVolume, maxVolume, aiFollowCurve.speed);
    }
}
