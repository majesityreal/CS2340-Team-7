using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingButton : MonoBehaviour
{
    public static int currBet;
    public static int playerMoney;
    public int inputBet;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        initBet();
        playerMoney = 100;
    }

    // Update is called once per frame
    void Update()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Initalize player bet amount.
    public void initBet()
    {
        currBet = 0;
    }

    public void comfirmBet()
    {
        gameManager.betAmount = currBet;
        playerMoney -= currBet;
        Debug.Log("Comfirm Bet, total Bet: " + currBet.ToString());
    }

    public void addBet(int amount)
    {
        if (amount <= 0
            && ((amount + currBet) > playerMoney))
        {
            Debug.Log("Bet value is invalid");
            // Will add an exception here.
        }
        else
        {
            currBet += amount;
            Debug.Log("Add " + amount.ToString() + "to bet. Total Bet: " + currBet.ToString());
        }
    }

    public void addBet()
    {
        addBet(inputBet);
    }

    public int getCurrBet()
    {
        return currBet;
    }

    public void resetBet()
    {
        currBet = 0;
        Debug.Log("Reset bet. Total Bet: " + currBet.ToString());
    }
}
