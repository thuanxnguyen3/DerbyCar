using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // if I press the back space key, reset the level
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // determine the current scene
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            // reload that scene
            SceneManager.LoadScene(currentLevelIndex);
        }
        // if I press the escape key, exit the application
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Exit the game");
            Application.Quit();
        }
    }
}
