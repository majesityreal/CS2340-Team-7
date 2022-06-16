using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


public class BettingButton : MonoBehaviour
{
    public int currBet;
    public int playerMoney;
    public int inputBet;

    // Scripts
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
        // No need for now.
    }

    public void OnValueChanged(float newValue)
    {
        GetComponent<AudioSource>().volume = newValue;
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
        } else
        {
            currBet += amount;
            Debug.Log("Add " + amount.ToString() + "to bet. Total Bet: " + currBet.ToString());
        }
    }

    private void addBet()
    {
        addBet(inputBet);
    }

    public int getCurrBet()
    {
        return currBet;
    }
}
