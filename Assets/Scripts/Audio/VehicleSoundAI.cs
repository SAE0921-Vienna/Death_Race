using UnityEngine;
using Audio;

public class VehicleSoundAI : VehicleSoundManager
{
    private AIFollowCurve _aiFollowCurve;
    
    private void Awake()
    {
        _aiFollowCurve = GetComponentInParent<AIFollowCurve>();
        _audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (_audioSource == null) return;
        _audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, _aiFollowCurve.GetCurrentSpeed());
        _audioSource.volume = Mathf.Lerp(minVolume, maxVolume, _aiFollowCurve.Speed);
    }
}
