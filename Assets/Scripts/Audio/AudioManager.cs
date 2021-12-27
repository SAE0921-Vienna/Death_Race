using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

//Add de-spawner to this.

namespace Audio
{
    public static class AudioManager 
    {
        public enum Sound{
            VehicleSound,
            LaserSound
        }
        private static Dictionary<Sound, float> _soundTimerDictionary;
        private static Dictionary<Sound[], float> _soundArrayTimerDictionary;

        public static void Initialize(){
            _soundTimerDictionary = new Dictionary<Sound, float>();
            _soundArrayTimerDictionary  = new Dictionary<Sound[], float>();
        }
    
        public static void PlaySound(Sound sound){
            if (CanPlaySound(sound)){
                var soundGameObject = new GameObject("Sound");
                var audioSource = soundGameObject.AddComponent<AudioSource>();

                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(GetAudioClip(sound));
                
                var destroyer = soundGameObject.AddComponent<SoundDestroyer>();
                destroyer.DestroySoundObject(soundGameObject, GetAudioClip(sound).length);
            }
        }
        //plays static sound without directional properties.

        //Plays sound with directional properties.
        public static void PlaySound(Sound sound, Vector3 position){
            if (CanPlaySound(sound)){
            
                var soundGameObject = new GameObject("Sound");
                soundGameObject.transform.position = position;
                var audioSource = soundGameObject.AddComponent<AudioSource>();

                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.clip = GetAudioClip(sound);

                audioSource.Play(); 
                
                var destroyer = soundGameObject.AddComponent<SoundDestroyer>();
                destroyer.DestroySoundObject(soundGameObject, GetAudioClip(sound).length);
            }
        }
    
        //Volume property
        public static void PlaySound(Sound sound, float volume){
            if (CanPlaySound(sound)){
            
                var soundGameObject = new GameObject("Sound");
                var audioSource = soundGameObject.AddComponent<AudioSource>();
                

                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.volume = volume;
                audioSource.PlayOneShot(GetAudioClip(sound));
                
                var destroyer = soundGameObject.AddComponent<SoundDestroyer>();
                destroyer.DestroySoundObject(soundGameObject, GetAudioClip(sound).length);
            }
        }
    
        //Volume and Positional Property
        public static void PlaySound(Sound sound, Vector3 position, float volume){
            if (CanPlaySound(sound)){
            
                var soundGameObject = new GameObject("Sound");
                soundGameObject.transform.position = position;
                var audioSource = soundGameObject.AddComponent<AudioSource>();

                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.volume = volume;
                audioSource.PlayOneShot(GetAudioClip(sound));
                
                var destroyer = soundGameObject.AddComponent<SoundDestroyer>();
                destroyer.DestroySoundObject(soundGameObject, GetAudioClip(sound).length);
            }
        }
    
        //Add variety to sounds by choosing a Random sound each time.
        public static void PlaySound(float volume, string soundArrayName = "", params Sound[] sounds)
        {
            var sound = sounds[Random.Range(0, sounds.Length)];
            if (CanPlaySound(soundArrayName, sounds)){
                var soundGameObject = new GameObject("Sound");
                var audioSource = soundGameObject.AddComponent<AudioSource>();
                
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.volume = volume;
                audioSource.PlayOneShot(GetAudioClip(sound));
            }
        }
    
        //Variety of sound, including the control of the replay time for walking / running for example
        public static void PlaySound(float volume, float pitch, float duration, string soundArrayName = "", params Sound[] sounds)
        {
            var sound = sounds[Random.Range(0, sounds.Length)];
            if (CanPlaySound(duration, soundArrayName, sounds)){
                var soundGameObject = new GameObject("Sound");
                var audioSource = soundGameObject.AddComponent<AudioSource>();

                audioSource.pitch = pitch;
                audioSource.volume = volume;
                audioSource.PlayOneShot(GetAudioClip(sound));
            }
        }

        //Checks if the sound can be played again.
        private static bool CanPlaySound(Sound sound){
            switch(sound){
                case Sound.VehicleSound:
                    if (_soundTimerDictionary.ContainsKey(sound)){
                
                        var lastTimePlayed = _soundTimerDictionary[sound];
                        var playerMoveTimerMax = GetAudioClip(Sound.VehicleSound).length;
                
                        if (lastTimePlayed + playerMoveTimerMax < Time.time){
                            _soundTimerDictionary[sound] = Time.time;
                            return true;
                        }
                        return false;
                    }
                    else return true;
            
                default:
                    return true;
            }
        }
    
        private static bool CanPlaySound(string soundArrayName = "", params Sound[]sounds)
        {
            var sound = sounds[Random.Range(0, sounds.Length)];
            switch(soundArrayName){
                case "runSounds":
                    if (_soundTimerDictionary.ContainsKey(sound))
                    {

                        var lastTimePlayed = 0f; 
                        var playerMoveTimerMax = GetAudioClip(Sound.VehicleSound).length;
                
                        if (lastTimePlayed + playerMoveTimerMax < Time.time){
                            _soundTimerDictionary[sound] = Time.time;
                            return true;
                        }
                        return false;
                    }
                    else return true;
            
                default:
                    return true;
            }
        }
    
        private static bool CanPlaySound(float duration, string soundArrayName = "", params Sound[] sounds)
        {
            switch(soundArrayName){
                case "runSounds":
                    if (_soundArrayTimerDictionary.ContainsKey(sounds))
                    {
                        var lastTimePlayed = _soundArrayTimerDictionary[sounds];
                        var playerMoveTimerMax = duration;
                
                        if (lastTimePlayed + playerMoveTimerMax < Time.time){
                            _soundArrayTimerDictionary[sounds] = Time.time;
                            return true;
                        }
                        return false;
                    }
                    else return true;
            
                default:
                    return true;
            }
        }

        //Loops through all audio clips, and returns the correct one for the instance.
        private static AudioClip GetAudioClip(Sound sound){
            foreach(var soundAudioClip in GameAssets.i.soundAudioClips){
                if (soundAudioClip.sound == sound){
                    return soundAudioClip.audioClip;
                }
            }
            return null;
        }
    }


}

