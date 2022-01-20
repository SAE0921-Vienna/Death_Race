using System;
using UnityEngine;

namespace Customization
{
    public class CustomizationConfigurator : MonoBehaviour
    {
        [SerializeField] private CustomizationData customizationData;

        
        // Set all the values in the scriptable object, to the MonoBehavior object.
        private void Awake()
        {
            GetComponentInChildren<MeshFilter>().mesh = customizationData.vehicleMesh;
            Instantiate(customizationData.vehicleWeapon, GameObject.Find("WeaponPosition").transform, true);
            GetComponentInChildren<MeshRenderer>().material = customizationData.vehicleMaterial;
        }
    }
}