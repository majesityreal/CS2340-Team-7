using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 *  Author: Kevin Kwan
 *  Date:   2022.06.30
 *  Ver:    1.0
 *  
 *  This script is used to pause the game and bring up the pause menu.
 */

public class PauseMenu : MonoBehaviour
{
    
    bool isPaused = false;
    [SerializeField] bool isPhysicsRelated = false;
    [SerializeField] bool gameStarted = false;
    [SerializeField] GameObject pauseMenu;
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
        if (isPhysicsRelated && gameStarted)
        {
            Time.timeScale = 1f;
        }
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        if (isPhysicsRelated && gameStarted)
        {
            Time.timeScale = 0f;
        }
    }

    public void backToMenu()
    {
        //Application.LoadLevel("MainMenu");
        if (isPhysicsRelated) {
            Time.timeScale = 1f;
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void backToMenuSingleton()
    {
        GameManager.Instance.QuitGame();
    }

    public void startGame() // we need this because ST will have a starting screen
    {
        if (!gameStarted)
        {
            gameStarted = true;
        }
    }
}