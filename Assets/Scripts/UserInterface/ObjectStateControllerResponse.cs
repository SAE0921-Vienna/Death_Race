using System;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace UserInterface
{
    public class ObjectStateControllerResponse : MonoBehaviour, IClickResponse, ISoundPlayer
    {
        [SerializeField] private List<GameObject> objectsToActivate = new List<GameObject>();
        [SerializeField] private List<GameObject> objectsToDeactivate = new List<GameObject>();

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
            AudioManager.PlaySound(AudioManager.Sound.MMWhoosh, 0.4f);
        }
    }
}