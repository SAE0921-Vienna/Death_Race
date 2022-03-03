using PlayerController;
using UnityEngine;

namespace Audio
{
    public class VehicleSoundManager : MonoBehaviour
    {
        // Linearly Interpolates between min/max PITCH and VOLUME with a normalized value of 0 and maxSpeed as t.

        [SerializeField] protected AudioSource _audioSource;
        private VehicleController _vehicleController;

        [SerializeField] [Range(0, 2)] protected float minPitch, maxPitch;
        [SerializeField] [Range(0, 1)] protected float minVolume, maxVolume;

        private void Awake()
        {
            _vehicleController = GetComponentInParent<VehicleController>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_audioSource == null) return;
            _audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, _vehicleController.currentSpeed);
            _audioSource.volume = Mathf.Lerp(minVolume, maxVolume, _vehicleController.currentSpeed);
        }



    }
}