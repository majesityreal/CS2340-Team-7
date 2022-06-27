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
public class SplitButton : MonoBehaviour
{
    // The second hand for split.
    public Hand playerSplitHand;

    // Booleans to check split status.
    public bool canSplit;
    public bool haveSplit;
    // Scripts
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        InitSplitHand();
    }

    // Update is called once per frame
    void Update()
    {
        // Only check when not splited
        // and only two cards on hand.
        if (!haveSplit && gameManager.playerHand.GetSize() == 2)
        {
            // Check if the two cards able to split.
            if (!canSplit && SameValue(gameManager.playerHand.GetIndex(0), gameManager.playerHand.GetIndex(1)))
            {
                canSplit = true; 
            }
        }

        // Update to Game Manager.
        gameManager = FindObjectOfType<GameManager>();
    }

    // If player is able to split,
    // click on the button will allow them to split.
    public void ButtonPressed()
    {
        if (canSplit)
        {
            Split();
            haveSplit = true;
            canSplit = false;
            Debug.Log("Split success!");
        }
        else
        {
            Debug.Log("You can't Split!");
        }
    }

    // Initalize the split part when called.
    public void InitSplitHand()
    {
        canSplit = false;
        haveSplit = false;
        playerSplitHand = new Hand();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Change player's current hand to split hand.
    public void SwitchHand()
    {
        Hand temp = gameManager.playerHand;
        gameManager.playerHand = playerSplitHand;
        playerSplitHand = temp;
    }

    // Function that split the hand in to two hands.
    private void Split()
    {
        // set the second card in hand to split hand.
        playerSplitHand.AddToHand(gameManager.playerHand.RemoveLast());
    }

    // Check if two card's value is same.
    private bool SameValue(Card a, Card b)
    {
        return a.value == b.value;
    }
}