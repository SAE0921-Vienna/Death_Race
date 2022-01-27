using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

/// <summary>
/// Enum-Holder für Audios-Settings
/// </summary>
public enum EAudioTypes 
{
    NONE,
    MASTER,
    MUSIC,
    EFFECT
}

/// <summary>
/// Hier werden die Optionen konfiguriert
/// </summary>
public class VolumeSlider : MonoBehaviour
{
    #region Variables
    [Header("Audio Settings")]
    [SerializeField] private EAudioTypes EAudioTypes;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI textVolume;

    private int m_AudioDisplayPuffer = 70;
    #endregion Variables

    /// <summary>
    /// Spieler-Settings werden geladen
    /// </summary>
    private void Awake()
    {
        switch(EAudioTypes)
        {
            case EAudioTypes.MASTER:
                //volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
                //audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
                break;
            case EAudioTypes.MUSIC:
                //volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
                //audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
                break;
            case EAudioTypes.EFFECT:
                //volumeSlider.value = PlayerPrefs.GetFloat("EffectVolume");
                //audioMixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("EffectVolume"));
                break;
        }
    }
    private void Update()
    {
        VolumeDisplay();
    }
    /// <summary>
    /// Hier wird der Slider als Wert angezeit und die Audio(Dezibel) darauf angepasst
    /// </summary>
    public void VolumeDisplay()
    {
        switch (EAudioTypes)
        {
            case EAudioTypes.MASTER:
                //textVolume.text = "" + (PlayerPrefs.GetFloat("MasterVolume") + m_AudioDisplayPuffer);
                //volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
                break;
            case EAudioTypes.MUSIC:
                //textVolume.text = "" + (PlayerPrefs.GetFloat("MusicVolume") + m_AudioDisplayPuffer);
                //volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
                break;
            case EAudioTypes.EFFECT:
                //textVolume.text = "" + (PlayerPrefs.GetFloat("EffectVolume") + m_AudioDisplayPuffer);
                //volumeSlider.value = PlayerPrefs.GetFloat("EffectVolume");
                break;
        }
    }
    /// <summary>
    /// Hier wird die Lautstärke des Volumes angepasst
    /// </summary>
    /// <param name="_volume">Wird über den Slider reguliert</param>
    public void SetVolume(float _volume)
    {
        string m_VolumeName = string.Empty;
        switch (EAudioTypes)
        {
            case EAudioTypes.MASTER:
                m_VolumeName = "MasterVolume";           
                break;
            case EAudioTypes.MUSIC:
                m_VolumeName = "MusicVolume";           
                break;
            case EAudioTypes.EFFECT:
                m_VolumeName = "EffectVolume";
                break;
        }
        audioMixer.SetFloat(m_VolumeName,_volume);
        
        //PlayerPrefs.SetFloat(m_VolumeName, _volume);
        //PlayerPrefs.Save();
    }
}
