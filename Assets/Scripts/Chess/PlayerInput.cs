using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 *  Author:         Zheng Yuan
 *  Date:           2022.06.28
 *  Version:        1.0
 *  
 *  Last update:    
 *                  2022.06.28  Add Cancel current selection function.
 *  
 *  Script for Piece OnMouseClick.
 */
public class PlayerInput : MonoBehaviour
{
    public static GameObject CurrSelected;
    public static int PlayerColor = 1;// White1 Black-1
    public static bool IsPlayerTurn = true;
    public static bool IsGamePaused = false;
    //Game Object
    public GameObject ResultStage;
    // Start is called before the first frame update
    void Start()
    {
        //CurrSelected = null;
        //IsPlayerTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     ExitGame();
        // }
        // removing this since we have a pause menu
    }

    private void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
