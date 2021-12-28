using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    public class ShipConfigurationManager : MonoBehaviour
    {
        private List<Transform> allCameraAngles = new List<Transform>();

        public ShipConfiguration[] shipConfigurations;
        
        [System.Serializable]
        public class ShipConfiguration
        {
            public GameObject ship;
            public Material shipColor;
            public Transform[] cameraConfigs;
        }
    }
}