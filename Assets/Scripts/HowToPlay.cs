using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /**
    *  Author: Kevin Kwan
    *  Date:   2022.06.19
    *  Ver:    1.0
    *  
    *  This script is used to navigate the how to play screen.
    *  Make sure to label UI elements in a logical/alphabetical order.
    *  ex. HowTo00, HowTo01, HowTo02, HowTo03, etc.
    */

public class HowToPlay : MonoBehaviour
{
    private GameObject[] uiElements;
    private int currentUIElement = 0;
    private GameObject nextButton;
    private GameObject prevButton;
    private bool inited = false;
    // Start is called before the first frame update
    void Start()
    {

        // ENSURE THAT ALL THE HOW-TO SCREENS ARE LABELLED IN ORDER!
        uiElements = GameObject.FindGameObjectsWithTag("Tutorial");
        nextButton = GameObject.Find("NextButton");
        prevButton = GameObject.Find("PrevButton");
        if (uiElements.Length <= 0) {
            throw new Exception("No tutorial elements found!");
        }
        // using a sorted dictionary to sort the elements by their name and number,
        // since we cannot depend on FindGameObjectsWithTag to return them in the correct order
        SortedDictionary<string, GameObject> sortedObjs = new SortedDictionary<string, GameObject>();
        for (int i = 0; i < uiElements.Length; i++)
        {
            sortedObjs.Add(uiElements[i].name, uiElements[i]);
        }
        // now, we get a sorted array of the objects
        // this is probably not the most efficient way to do this, but it is a simple implementation
        // we want to use index to traverse each page but keep track on what page the player was on
        sortedObjs.Values.CopyTo(uiElements, 0);
        for (int i = 0; i < uiElements.Length; i++)
        {
            Debug.Log(uiElements[i].name); // debug to ensure that everything loads correctly
            //hide all the elements
            uiElements[i].SetActive(false);
        }
        // hide the previous button
        if (currentUIElement == 0) {
            prevButton.SetActive(false);
            if (uiElements.Length == 1) {
                nextButton.SetActive(false);
            }
        }
        // show the first element
        uiElements[currentUIElement].SetActive(true);
        if (!inited) {
        gameObject.SetActive(false);
        inited = true;
        }
    }

    public void increment()
    {   uiElements[currentUIElement].SetActive(false);
        currentUIElement++;
        // if last element, hide next button
        if (currentUIElement >= uiElements.Length - 1)
        {
            nextButton.SetActive(false);
            prevButton.SetActive(true);
            currentUIElement = uiElements.Length - 1;
            uiElements[currentUIElement].SetActive(true);
        } else {
            Debug.Log("not last element");
        prevButton.SetActive(true);
        nextButton.SetActive(true);
        uiElements[currentUIElement].SetActive(true);
        }
        
    }

    public void decrement()
    {
        uiElements[currentUIElement].SetActive(false);
        currentUIElement--;
        if (currentUIElement <= 0)
        {
            prevButton.SetActive(false);
            nextButton.SetActive(true);
            currentUIElement = 0;
            uiElements[currentUIElement].SetActive(true);
        } else {
        prevButton.SetActive(true);
        nextButton.SetActive(true);
        uiElements[currentUIElement].SetActive(true);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // list of objects
    // increment and decrement
    //script on button with boolean to enable/disable
    // method to also close this window

    public void closeWindow()
    {
        gameObject.SetActive(false);
    }

    public void openWindow()
    {
        gameObject.SetActive(true);
    }
}
