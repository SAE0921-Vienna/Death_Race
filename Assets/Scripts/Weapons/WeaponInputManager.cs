using System;
using UnityEngine;

namespace Weapons
{
    public class WeaponInputManager : MonoBehaviour
    {
        private IWeapon _weapon;

        private void Awake()
        {
            _weapon = GetComponentInChildren<IWeapon>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _weapon.Shoot();
            }
        }
    }
}