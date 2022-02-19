using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenuScript : MonoBehaviour
{
    //Pause Menu
    [Header("Loading/Pause/Options Menu Settings")]
    [SerializeField] RectTransform pausePanel;
    [SerializeField] GameObject pausePanelObject;
    [SerializeField] RectTransform optionsPanel;
    [SerializeField] GameObject optionsPanelObject;
    [SerializeField] private bool isPause = false;
    [SerializeField] private List<HUDColor> hudColors;
    private InputActions inputActions;
    private int currentMaterial;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.UI.PauseMenu.performed += ctx => PauseMenu();
    }
    private void Start()
    {
        currentMaterial = GetComponent<SpaceshipLoad>().currentMaterial;
        pausePanelObject.GetComponent<Image>().color = new Color(hudColors[currentMaterial].R, hudColors[currentMaterial].G, hudColors[currentMaterial].B, hudColors[currentMaterial].A);
        optionsPanelObject.GetComponent<Image>().color = new Color(hudColors[currentMaterial].R, hudColors[currentMaterial].G, hudColors[currentMaterial].B, hudColors[currentMaterial].A);
    }

    void Update()
    {
        if (isPause)
        {
            //PlayerDriveFunction()
            //PlayShootFunction()
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;

        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void PauseMenu()
    {
        if (pausePanel.gameObject.activeInHierarchy == false && optionsPanel.gameObject.activeInHierarchy == false)
        {
            pausePanel.gameObject.SetActive(true);
            //Cursor.lockState = CursorLockMode.None;

            isPause = true;
        }
        else if (pausePanel.gameObject.activeInHierarchy == true || optionsPanel.gameObject.activeInHierarchy == true)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            pausePanel.gameObject.SetActive(false);
            optionsPanel.gameObject.SetActive(false);
            isPause = false;
        }
    }
    public void Resume()
    {
        pausePanel.gameObject.SetActive(false);
        isPause = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0); //Menu
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
