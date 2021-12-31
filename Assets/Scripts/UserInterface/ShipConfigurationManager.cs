using UnityEngine;

namespace UserInterface
{
    public class ShipConfigurationManager : MonoBehaviour
    {
        public ShipConfiguration[] shipConfigurations;
        
        [System.Serializable]
        public class ShipConfiguration
        {
            public GameObject ship;
            public GameObject shipWeapon;
            public Material shipColor;
            public Transform[] cameraConfigs;
        }
    }
}