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
        currBet = 0;
        playerMoney = 100;
    }

    // Update is called once per frame
    void Update()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ComfirmBet()
    {
        playerMoney -= currBet;
        Debug.Log("Comfirm Bet, total Bet: " + currBet.ToString());
    }

    public void AddBet(int amount)
    {
        if (amount <= 0 && ((amount + currBet) > playerMoney))
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

    public void AddBet()
    {
        AddBet(inputBet);
    }

    public void ResetBet()
    {
        currBet = 0;
        Debug.Log("Reset bet. Total Bet: " + currBet.ToString());
    }
}
