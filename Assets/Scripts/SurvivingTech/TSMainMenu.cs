using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 *  Authors:        Jing Liu
 *                  Kevin Kwan
 *  Date:           2022.07.12
 *  Version:        1.1

 *  
 *  Script for Surviving Tech Start Menu
 */

public class TSMainMenu : MonoBehaviour
{

    [SerializeField] GameObject StartMenu;


     void Start()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        hideStart();
    }


    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void hideStart()
    {
        StartMenu.gameObject.SetActive(false);
    }

    public void showPanel()
    {
        StartMenu.gameObject.SetActive(true);
    }

    public void reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
