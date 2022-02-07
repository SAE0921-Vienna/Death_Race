using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
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
    [SerializeField] private EAudioTypes EAudioTypes;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI textVolume;

    public SaveLoadScript saveLoadScript;
    private int AudioDisplayPuffer = 90; //Audio Volume Display Puffer
    #endregion Variables



    /// <summary>
    /// Load Player-Settings
    /// </summary>
    private void Awake()
    {
        //saveLoadScript.LoadOptionsData();

        switch (EAudioTypes)
        {
            case EAudioTypes.MASTER:
                volumeSlider.value = saveLoadScript.masterVolume;
                audioMixer.SetFloat("MasterVolume", Mathf.Log10(saveLoadScript.masterVolume)*20);
                //volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
                //audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
                break;
            case EAudioTypes.MUSIC:
                volumeSlider.value = saveLoadScript.musicVolume;
                audioMixer.SetFloat("MusicVolume", Mathf.Log10(saveLoadScript.musicVolume) * 20);
                //volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
                //audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
                break;
            case EAudioTypes.EFFECT:
                volumeSlider.value = saveLoadScript.effectVolume;
                audioMixer.SetFloat("EffectsVolume", Mathf.Log10(saveLoadScript.effectVolume) * 20);
                //volumeSlider.value = PlayerPrefs.GetFloat("EffectVolume");
                //audioMixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("EffectVolume"));
                break;
        }
    }
    private void Update()
    {
        //if(GetComponentInParent<GameObject>().activeInHierarchy)
        //{
        VolumeDisplay();
        //}

    }
    /// <summary>
    /// Change Volume Slider Value (Display) and Audio(Dezibel) Puffer?
    /// </summary>
    public void VolumeDisplay()
    {
        switch (EAudioTypes)
        {
            case EAudioTypes.MASTER:
                textVolume.text = "" + Mathf.Round(volumeSlider.value*100);
                //volumeSlider.value = saveLoadScript.masterVolume;
                //textVolume.text = "" + (PlayerPrefs.GetFloat("MasterVolume") + m_AudioDisplayPuffer);
                //volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
                break;
            case EAudioTypes.MUSIC:
                textVolume.text = "" + Mathf.Round(volumeSlider.value*100);
                //volumeSlider.value = saveLoadScript.musicVolume;
                //textVolume.text = "" + (PlayerPrefs.GetFloat("MusicVolume") + m_AudioDisplayPuffer);
                //volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
                break;
            case EAudioTypes.EFFECT:
                textVolume.text = "" + Mathf.Round(volumeSlider.value*100);
                //volumeSlider.value = saveLoadScript.effectVolume;
                //textVolume.text = "" + (PlayerPrefs.GetFloat("EffectVolume") + m_AudioDisplayPuffer);
                //volumeSlider.value = PlayerPrefs.GetFloat("EffectVolume");
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
                saveLoadScript.SaveOptionsData(_volume, saveLoadScript.musicVolume, saveLoadScript.effectVolume);
                break;
            case EAudioTypes.MUSIC:
                volumeName = "MusicVolume";
                saveLoadScript.SaveOptionsData(saveLoadScript.masterVolume, _volume, saveLoadScript.effectVolume);
                break;
            case EAudioTypes.EFFECT:
                volumeName = "EffectVolume";
                saveLoadScript.SaveOptionsData(saveLoadScript.masterVolume, saveLoadScript.musicVolume, _volume);
                break;
        }
        audioMixer.SetFloat(volumeName, Mathf.Log10(_volume) * 20);

        //PlayerPrefs.SetFloat(m_VolumeName, _volume);
        //PlayerPrefs.Save();

    }
}
