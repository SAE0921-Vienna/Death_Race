using UnityEngine;
using Weapons;

namespace Customization
{
    [CreateAssetMenu (menuName = "Scriptables", fileName = "Customization Data")]
    public class CustomizationData : ScriptableObject
    {
        //Set these in Main Menu, or randomly generate them during runtime.
        
        public Mesh vehicleMesh;
        public GameObject vehicleWeapon;
        public Material vehicleMaterial;
        
        [HideInInspector] 
        public IWeapon vehicleWeaponScript;

        private void Awake()
        {
            if (vehicleWeapon.GetComponent<IWeapon>() == null) return;
            vehicleWeaponScript = vehicleWeapon.GetComponent<IWeapon>();
        }
    }
}