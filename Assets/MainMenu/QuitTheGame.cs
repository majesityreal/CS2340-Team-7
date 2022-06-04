using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitTheGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Coding done by Kevin Kwan
    // Last updated 6/3/2022
    // This function is called when the quit button is pressed
    // This is a public method because Unity buttons can only called public methods
    // Not sure if this violates encapsulation or not, but we can only blame Unity
    public void QuitGame()
    {
        // Tested and successfully runs as intended.
        //Debug.Log("Successfully quit.");
        Application.Quit();
    }
}
