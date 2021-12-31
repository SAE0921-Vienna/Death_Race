using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace UserInterface
{
    public class ObjectStateControllerResponse : MonoBehaviour, IClickResponse, ISoundPlayer
    {
        [Header("Objects")]
        [SerializeField] private List<GameObject> objectsToActivate = new List<GameObject>();
        [SerializeField] private List<GameObject> objectsToDeactivate = new List<GameObject>();
        
        [Header("Sounds")]
        [SerializeField] private AudioManager.Sound actionSound;
        [SerializeField] private float soundVolume;

        public void ExecuteFunctionality()
        {
            ChangeActiveObjects(objectsToActivate, objectsToDeactivate);
            PlaySound();
        }
        
        private void ChangeActiveObjects(List<GameObject> activeObj, List<GameObject> deactivObj)
        {
            foreach (var targetObject in deactivObj)
            {
                targetObject.SetActive(false);
            }
            
            foreach (var targetObject in activeObj)
            {
                targetObject.SetActive(true);
            }
        }
        
        public void PlaySound()
        {
            AudioManager.PlaySound(actionSound, soundVolume);
        }
    }
}