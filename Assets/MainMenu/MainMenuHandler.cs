using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    // Kevin Kwan
    public GameObject mainMenu;
    public GameObject selectionMenu;
    // Start is called before the first frame update
    void Start()
    {
        // basically, if the game has not been loaded before, then load the main menu
        if (PlayerPrefs.GetInt("loaded", 0) == 0) {
            // for testing:
            // mainMenu.SetActive(false);
            // selectionMenu.SetActive(true);
            mainMenu.SetActive(true);
            selectionMenu.SetActive(false);
        } else { // otherwise, if you chose one of the games, we will set "loaded" to be 1 in those scenes
            // this will be called when the player hits a button to load the mainmenu scene, bringing them
            // immediately to the game selection menu
            mainMenu.SetActive(false);
            selectionMenu.SetActive(true);

            // this works as intended because when the player hits yes on quit, "loaded" will be reset to 0
            // if they alt f4, the game will start at the selection screen instead (saving where you left off feature?)
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}