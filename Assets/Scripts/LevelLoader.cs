using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject mainmenuUI;
    public GameObject garageUI;

    public Slider loadingSlider;

    public void LoadLevel(string sceneName)
    {
        garageUI.SetActive(false);
        mainmenuUI.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            Debug.Log(progress);

            yield return null;

        }

    }


}
