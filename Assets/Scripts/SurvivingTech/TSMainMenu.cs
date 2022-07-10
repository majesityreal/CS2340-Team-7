using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 *  Author:         Jing Liu
 *  Date:           2022.07.10
 *  Version:        1.0

 *  
 *  Script for Surviving Tech Start Menu
 */

public class TSMainMenu : MonoBehaviour
{



     void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // PlayerPrefs.SetInt("loaded", 1);
        // SceneManager.LoadScene("Play");
    }

        public void PowerUp()
    {
        //Application.PowerUp();
        PlayerPrefs.SetInt("loaded", 1);
        SceneManager.LoadScene("PowerUp");
    }

        public void Collection()
    {
        PlayerPrefs.SetInt("loaded", 1);
        SceneManager.LoadScene("Collection");
    }
    
        public void Credits()
    {
        PlayerPrefs.SetInt("loaded", 1);
        SceneManager.LoadScene("Credits");
    }

        public void Archievements()
    {
        PlayerPrefs.SetInt("loaded", 1);
        SceneManager.LoadScene("Acrhievements");
    }
        public void Quit()
    {
        PlayerPrefs.SetInt("loaded", 1);
        SceneManager.LoadScene("Quit");
    }

}
