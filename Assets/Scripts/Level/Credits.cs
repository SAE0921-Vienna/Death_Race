using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public LevelLoader levelLoader;

    private void Update()
    {
        if (Input.anyKey)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                return;
            }
            else
            {
                levelLoader.LoadLevel("MainMenu");
            }
        }

    }

}
