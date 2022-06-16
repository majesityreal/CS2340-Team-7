using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author: Zheng Yuan
 *  Date:   2022.06.15
 *  Ver:    1.0
 *  
 *  This is the script for split function in Game "BlueJack" for CS 2340 Team Blu Project.
 */
public class SplitHand : MonoBehaviour
{
   // The second hand for split.
    public Card[] playerSplitHand;

    // Booleans to check split status.
    private bool canSplit;
    private bool haveSplit;
    // Scripts
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        initSplitHand();
    }

    // Update is called once per frame
    void Update()
    {
        // Only check when not splited
        // and only two cards on hand.
        if (!haveSplit && gameManager.playerHand[3] == null)
        {   
            // Check if the two cards able to split.
            if (!canSplit && sameValue(gameManager.playerHand[0], gameManager.playerHand[1]))
            {   
                canSplit = true;
            }
        }
    }

    // If player is able to split,
    // click on the button will allow them to spliit.
    public void splitButton()
    {
        if (canSplit)
        {
            split();
            haveSplit = true;
            canSplit = false;
            Debug.Log("Split success!");
        } else
        {
            Debug.Log("You can't Split!");
        }
        

    }

    // Initalize the split part when called.
    public void initSplitHand()
    {
        canSplit = false;
        haveSplit = false;
        playerSplitHand = new Card[8];
        gameManager = FindObjectOfType<GameManager>();
    }

    // Change player's current hand to split hand.
    public void switchHand()
    {
        Card[] temp = gameManager.playerHand;
        gameManager.playerHand = playerSplitHand;
        playerSplitHand = temp;
    }

    // Function that split the hand in to two hands.
    private void split()
    {   
        // set the second card in hand to split hand.
        playerSplitHand[0] = gameManager.playerHand[1];
        gameManager.playerHand[1] = null;
    }

    // Check if two card's value is same.
    private bool sameValue(Card a, Card b)
    {
        return a.getValue() == b.getValue();
    } 
}
