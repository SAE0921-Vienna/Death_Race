using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UserInterface;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenuScript : MonoBehaviour
{
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
        ChangeUIColor();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    /// <summary>
    /// Changes the color of the UI (to cockpit color)
    /// </summary>
    private void ChangeUIColor()
    {
        currentMaterial = GetComponent<SpaceshipLoad>().currentMaterial;
        pausePanelObject.GetComponent<Image>().color = new Color(hudColors[currentMaterial].R, hudColors[currentMaterial].G, hudColors[currentMaterial].B, hudColors[currentMaterial].A);
        optionsPanelObject.GetComponent<Image>().color = new Color(hudColors[currentMaterial].R, hudColors[currentMaterial].G, hudColors[currentMaterial].B, hudColors[currentMaterial].A);
    }

    /// <summary>
    /// Opens the pause menu.
    /// </summary>
    private void PauseMenu()
    {
        if (pausePanel.gameObject.activeInHierarchy == false && optionsPanel.gameObject.activeInHierarchy == false)
        {
            pausePanel.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);

            Time.timeScale = 0;
            isPause = true;
        }
        else if (pausePanel.gameObject.activeInHierarchy == true || optionsPanel.gameObject.activeInHierarchy == true)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            pausePanel.gameObject.SetActive(false);
            optionsPanel.gameObject.SetActive(false);
            FindObjectOfType<UIManager>().SetCursor();

            Time.timeScale = 1;
            isPause = false;
        }
    }

    /// <summary>
    /// Resume the game.
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
        FindObjectOfType<UIManager>().SetCursor();
        isPause = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// 
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene(0); //Menu
    }

    /// <summary>
    /// Close the game.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
