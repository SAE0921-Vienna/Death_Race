using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UserInterface;

public class MenuManager : MonoBehaviour
{
    [Header("Cinemachine")]
    public CinemachineBrain mainCamera;
    public float cameraMovingTime = 2f;
    public float additionalDelay = 0.25f;
    public CinemachineVirtualCamera[] frameCam;

    [Header("HUD")]
    public GameObject HUDParent;
    public GameObject[] activeHUD;
    private int lastActiveHUD;

    public void Play()
    {
        SceneManager.LoadScene("Scenes/pre-testing/George");
    }
    public void Settings()
    {
        StartCoroutine(ISettings());
    }
    public void Garage() //Button double-click possible?
    {
        StartCoroutine(IGarage());
    }
    public void Shop()
    {
        StartCoroutine(IShop());
    }
    public void Tutorial()
    {
        //SceneManager.LoadScene("");
        StartCoroutine(ITutorial());
    }
    public void Back()
    {
        StartCoroutine(IBack());
    }
    public void ShopOpen()
    {

    }
    public void LoadScene(GameObject responseGameObject)
    {
        responseGameObject.GetComponent<IClickResponse>().ExecuteFunctionality();
    }
    #region IEnumeratorButton
    public IEnumerator ISettings()
    {
        DeactivateCurrentHUD();
        frameCam[1].gameObject.SetActive(true);
        
        AudioManager.PlaySound(AudioManager.Sound.MMWhoosh, .4f);

        yield return new WaitForSeconds(cameraMovingTime + additionalDelay);

        activeHUD[1].SetActive(true);
    }
    public IEnumerator IGarage()
    {
        DeactivateCurrentHUD();
        frameCam[2].gameObject.SetActive(true);
        
        AudioManager.PlaySound(AudioManager.Sound.MMWhoosh, .4f);

        yield return new WaitForSeconds(cameraMovingTime + additionalDelay);

        activeHUD[2].SetActive(true);
    }
    public IEnumerator IShop()
    {
        DeactivateCurrentHUD();
        frameCam[3].gameObject.SetActive(true);
        
        AudioManager.PlaySound(AudioManager.Sound.MMWhoosh, .4f);

        yield return new WaitForSeconds(cameraMovingTime + additionalDelay);

        activeHUD[3].SetActive(true);
    }
    public IEnumerator IBack()
    {
        DeactivateCurrentHUD();

        frameCam[0].gameObject.SetActive(true);
        
        AudioManager.PlaySound(AudioManager.Sound.MMWhoosh, .8f);

        yield return new WaitForSeconds(cameraMovingTime + additionalDelay);

        activeHUD[0].SetActive(true);
    }
    public IEnumerator ITutorial()
    {
        DeactivateCurrentHUD();
        frameCam[4].gameObject.SetActive(true);
        
        AudioManager.PlaySound(AudioManager.Sound.MMWhoosh, .4f);

        yield return new WaitForSeconds(cameraMovingTime);

        activeHUD[4].SetActive(true);
    }
    #endregion IEnumeratorButton
    private void DeactivateCurrentHUD() 
    {
        for(int i = 0; i < HUDParent.gameObject.transform.childCount; i++)
        {
            if(HUDParent.gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                HUDParent.gameObject.transform.GetChild(i).gameObject.SetActive(false); // == activeHUD[i].SetActive(false);
                frameCam[i].gameObject.SetActive(false);
                lastActiveHUD = i;
            }
        }
    }
}