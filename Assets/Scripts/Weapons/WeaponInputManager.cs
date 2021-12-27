using Audio;
using UnityEngine;

namespace Weapons
{
    public class WeaponInputManager : MonoBehaviour
    {
        private IWeapon _weapon;
        private ISoundPlayer _soundPlayer;

        private void Awake()
        {
            _weapon = GetComponentInChildren<IWeapon>();
            if (_weapon is ISoundPlayer) _soundPlayer = GetComponentInChildren<ISoundPlayer>();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            _weapon.Shoot();
            _soundPlayer?.PlaySound();
        }
    }
}