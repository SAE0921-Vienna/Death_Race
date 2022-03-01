using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject mainmenuUI;
    public GameObject garageUI;
    public GameObject pauseUI;
    public GameObject HUDUI;

    public Slider loadingSlider;
    public TextMeshProUGUI loadingTitle;
    public GameObject pressanykeyUI;

    public Sprite[] backgrounds;
    public Image backgroundImage;

    public TextMeshProUGUI tipsText;

    public string[] tips;
    public int tipCount;

    public bool doAnimation;
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadLevel(string sceneName)
    {
        backgroundImage.sprite = backgrounds[Random.Range(0, backgrounds.Length)];

        DeactivateUIs();

        loadingScreen.SetActive(true);
        loadingTitle.text = sceneName;

        if (transition)
        {
            if (doAnimation)
            {
                StartCoroutine(StartAnimation());
                transition.enabled = true;
                transition.gameObject.GetComponent<CanvasGroup>().enabled = true;
            }
            else
            {
                transition.enabled = false;
                transition.gameObject.GetComponent<CanvasGroup>().enabled = false;

            }
        }


        StartCoroutine(LoadAsynchronously(sceneName));
        StartCoroutine(GenerateTips());

    }

    public void DeactivateUIs()
    {
        if (garageUI)
        {
            garageUI.SetActive(false);
        }

        if (mainmenuUI)
        {
            mainmenuUI.SetActive(false);
        }

        if (pauseUI)
        {
            pauseUI.SetActive(false);
        }

        if (HUDUI)
        {
            HUDUI.SetActive(false);
        }

    }

    IEnumerator StartAnimation()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;

            if (progress >= .9f && !operation.allowSceneActivation)
            {
                pressanykeyUI.SetActive(true);

                if (Input.anyKey)
                {
                    operation.allowSceneActivation = true;
                }
            }
            //Debug.Log(progress);

            yield return null;

        }

    }

    public IEnumerator GenerateTips()
    {
        tipCount = Random.Range(0, tips.Length);
        tipsText.text = tips[tipCount];

        //Debug.Log(loadingScreen.activeInHierarchy);

        while (loadingScreen.activeInHierarchy)
        {

            yield return new WaitForSeconds(8f);

            //Make transition

            //yield return new WaitForSeconds(0.5f);

            tipCount++;
            if (tipCount >= tips.Length)
            {
                tipCount = 0;
            }

            tipsText.text = tips[tipCount];

        }

    }


}
