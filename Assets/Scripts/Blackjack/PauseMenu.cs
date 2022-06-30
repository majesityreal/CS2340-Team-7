using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author: Kevin Kwan
 *  Date:   2022.06.16
 *  Ver:    1.0
 *  
 *  This script is used to pause the game and bring up the pause menu.
 */

public class PauseMenu : MonoBehaviour
{
    
    bool isPaused = false;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        //pauseMenu = GameObject.Find("PauseMenu");
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
                unpause();
            }
            else
            {
                pause();
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
        GameManager.Instance.QuitGame();
    }
}