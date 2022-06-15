using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    bool isPaused = false;
    private GameObject pauseMenu;
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Escape key was pressed");
            if (isPaused)
            {
                pauseMenu.SetActive(false);
                isPaused = false;
            }
            else
            {
                pauseMenu.SetActive(true);
                isPaused = true;
            }
        }
        
    }

    public void unpause()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void backToMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
