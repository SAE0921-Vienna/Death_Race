using System;
using UnityEngine;

namespace Audio
{
    public class VehicleSoundManager : MonoBehaviour
    {
        // Linearly Interpolates between min and max pitch and volume using the current speed.
        
        private AudioSource _audioSource;
        private VehicleController _vehicleController;

        [SerializeField] [Range(0, 2)] private float minPitch, maxPitch;
        [SerializeField] [Range(0,1)] private float minVolume, maxVolume;

        private void Awake()
        {
            _vehicleController = GetComponentInParent<VehicleController>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            _audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, _vehicleController.currentSpeed);
            _audioSource.volume = Mathf.Lerp(minVolume, maxVolume, _vehicleController.currentSpeed);
        }
    }
}