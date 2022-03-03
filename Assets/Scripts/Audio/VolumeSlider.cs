using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Audio;
using TMPro;

/// <summary>
/// Enum-Holder for Audios-Settings
/// </summary>
public enum EAudioTypes
{
    NONE,
    MASTER,
    MUSIC,
    EFFECT
}

/// <summary>
/// Option Configurator (PauseCanvas)
/// </summary>
public class VolumeSlider : MonoBehaviour
{
    #region Variables
    [Header("Audio Settings")]
    [SerializeField] private GameObject PauseOptionsMenu;
    [SerializeField] private EAudioTypes EAudioTypes;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI textVolume;

    public SaveLoadScript saveLoadScript;
    #endregion Variables

    /// <summary>
    /// Load Player-Settings
    /// </summary>
    private void Awake()
    {
        GetAudiosAtStart();
    }
    private void Update()
    {
        if (PauseOptionsMenu.gameObject.activeInHierarchy)
        {
            VolumeDisplay();
        }
    }
    /// <summary>
    /// Change Volume Slider Value (Display) and Audio(Dezibel) Puffer?
    /// </summary>
    public void VolumeDisplay()
    {
        switch (EAudioTypes)
        {
            case EAudioTypes.MASTER:
                textVolume.text = "" + Mathf.Round(volumeSlider.value * 100);
                break;
            case EAudioTypes.MUSIC:
                textVolume.text = "" + Mathf.Round(volumeSlider.value * 100);
                break;
            case EAudioTypes.EFFECT:
                textVolume.text = "" + Mathf.Round(volumeSlider.value * 100);
                break;
        }
    }
    /// <summary>
    /// Changes the volume of the MasterMixer
    /// </summary>
    /// <param name="_volume">Is regulated via slider</param>
    public void SetVolume(float _volume)
    {
        string volumeName = string.Empty;
        switch (EAudioTypes)
        {
            case EAudioTypes.MASTER:
                volumeName = "MasterVolume";
                saveLoadScript.masterVolume = _volume;
                saveLoadScript.SaveOptionsData(_volume, saveLoadScript.musicVolume, saveLoadScript.effectVolume);
                break;
            case EAudioTypes.MUSIC:
                volumeName = "MusicVolume";
                saveLoadScript.musicVolume = _volume;
                saveLoadScript.SaveOptionsData(saveLoadScript.masterVolume, _volume, saveLoadScript.effectVolume);
                break;
            case EAudioTypes.EFFECT:
                volumeName = "EffectVolume";
                saveLoadScript.effectVolume = _volume;
                saveLoadScript.SaveOptionsData(saveLoadScript.masterVolume, saveLoadScript.musicVolume, _volume);
                break;
        }
        audioMixer.SetFloat(volumeName, Mathf.Log10(_volume) * 20);
    }

    /// <summary>
    /// Loads the audio settings
    /// </summary>
    public void GetAudiosAtStart()
    {
        saveLoadScript.LoadOptionsData();

        switch (EAudioTypes)
        {
            case EAudioTypes.MASTER:
                volumeSlider.value = saveLoadScript.masterVolume;
                audioMixer.SetFloat("MasterVolume", ((Mathf.Log10(saveLoadScript.masterVolume)) * 20));
                textVolume.text = "" + Mathf.Round(volumeSlider.value * 100);
                SetVolume(saveLoadScript.masterVolume);
                break;
            case EAudioTypes.MUSIC:
                volumeSlider.value = saveLoadScript.musicVolume;
                audioMixer.SetFloat("MusicVolume", (Mathf.Log10(saveLoadScript.musicVolume)) * 20);
                textVolume.text = "" + Mathf.Round(volumeSlider.value * 100);
                SetVolume(saveLoadScript.musicVolume);
                break;
            case EAudioTypes.EFFECT:
                volumeSlider.value = saveLoadScript.effectVolume;
                audioMixer.SetFloat("EffectVolume", (Mathf.Log10(saveLoadScript.effectVolume)) * 20);
                textVolume.text = "" + Mathf.Round(volumeSlider.value * 100);
                SetVolume(saveLoadScript.effectVolume);
                break;
        }
    }

    /// <summary>
    /// Plays a sound effect
    /// </summary>
    public void PlayEffectSound()
    {
        AudioManager.PlaySound(AudioManager.Sound.RocketExplosion);
    }
}
