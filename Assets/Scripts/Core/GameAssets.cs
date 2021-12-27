using System;
using Audio;
using UnityEngine;

namespace Core
{
    //Central Asset Storage for the game. Not sure if this is good practice or not, please give feedback.
    
    public partial class GameAssets : MonoBehaviour
    {
        private static GameAssets _i;
        public static GameAssets i{
            get{
                if (_i == null){
                    _i = (Instantiate(Resources.Load("GameAssets")) as GameObject)?.GetComponent<GameAssets>();
                } 
                return _i; }
        }
    
        public SoundAudioClip[] soundAudioClips;

        [Serializable]
        public class SoundAudioClip{
            public AudioManager.Sound sound;
            public AudioClip audioClip;
        }

        private void Start() {
            AudioManager.Initialize();
        }
    }
}