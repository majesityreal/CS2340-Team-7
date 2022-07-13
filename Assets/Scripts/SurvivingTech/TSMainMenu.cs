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

    public GameObject Panel;


     void Update()
    {
        
    }

    public void Play()
    {
        hidePanel();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // PlayerPrefs.SetInt("loaded", 1);
        // SceneManager.LoadScene("Play");
    }
    

        public void PowerUp()
    {
        //Application.PowerUp();
        // PlayerPrefs.SetInt("loaded", 1);
        // SceneManager.LoadScene("PowerUp");
        hidePanel();

    }


        public void Quit()
    {
        // PlayerPrefs.SetInt("loaded", 1);
        // SceneManager.LoadScene("Quit");
        //hidePanel();
        //DestroyImmediate(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void hidePanel()
    {
        // for (int i = 0; i < Panel.transform.childCount; i++)
        {
            // Panel.transform.GetChild(i).gameObject.SetActive(false);
            Panel.gameObject.SetActive(false);
        }
    }

    public void showPanel()
    {

        // for (int i = 0; i < Panel.transform.childCount; i++)
        {
           // Panel.transform.GetChild(i).gameObject.SetActive(true);
           Panel.gameObject.SetActive(true);
        }
    }

}
