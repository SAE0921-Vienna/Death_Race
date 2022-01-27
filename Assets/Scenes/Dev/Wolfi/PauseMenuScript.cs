using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenuScript : MonoBehaviour
{
    //Pause Menu
    [Header("Loading/Pause/Options Menu Settings")]
    [SerializeField] RectTransform pausePanel;
    [SerializeField] RectTransform optionsPanel;
    [SerializeField] private bool isPause = false;


    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            //PlayerDriveFunction()
            //PlayShootFunction()
        }
        PauseMenu();
    }
    private void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.gameObject.activeInHierarchy == false && optionsPanel.gameObject.activeInHierarchy == false)
        {
            pausePanel.gameObject.SetActive(true);
            //Cursor.lockState = CursorLockMode.None;
            
            isPause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.gameObject.activeInHierarchy == true || Input.GetKeyDown(KeyCode.Escape) && optionsPanel.gameObject.activeInHierarchy == true)
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
