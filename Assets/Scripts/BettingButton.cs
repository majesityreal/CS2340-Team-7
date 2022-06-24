using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 *  Author:         Zheng Yuan
 *  Date:           2022.06.16
 *  Version:        1.2
 *  
 *  Last update:    
 *                  2022.06.23  Fix null reference bugs. Change static variable Name.
 *                  2022.06.20  Create an individual scene.
 *                  2022.06.16  Create the script for Betting Buttons.
 *  
 *  Script for Betting Stage's buttons.
 */
public class BettingButton : MonoBehaviour
{
    public static int CurrBet = 0;
    public static int PlayerMoney = 100;    // The initial player balance.

    public Text moneyDisplay;
    public Text betDisplay;
    public Slider betAmountSlider;
    public GameManager gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the betting stage.
        initBetStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initBetStage()
    {
        // Dehide the bet stage UI.
        showBet();

        // In case of null ref.
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        if (betAmountSlider)
        {
            betAmountSlider = GameObject.Find("BetAmountSlider").GetComponent<Slider>();
        }

        // Display button text.
        if (moneyDisplay != null)
        {
            moneyDisplay.text = "Balance: $" + PlayerMoney.ToString();
        }
        if (betDisplay != null)
        {
            betDisplay.text = "Current Bet: $" + CurrBet.ToString();
        }

        // Initialize slider.
        if (betAmountSlider != null)
        {
            betAmountSlider.wholeNumbers = true;
            betAmountSlider.maxValue = PlayerMoney;
            betAmountSlider.value = 0;
            betAmountSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        // Reset the current bet amount.
        CurrBet = 0;
    }

    public void ComfirmBet()
    {
        // Apply the change to the player balance. And Hide the UI.
        PlayerMoney -= CurrBet;
        hideBet();
        //Debug.Log("Comfirm Bet, total Bet: " + CurrBet.ToString());
        //Debug.Log("Total Money: " + PlayerMoney.ToString());
    }

    public void AddBet(int amount)
    {
        // If the new total bet is exceed total balance, make current bet max balance.
        if ((amount + CurrBet) > PlayerMoney)
        {
            CurrBet = PlayerMoney;
        }
        // Otherwise, add it to current bet.
        else if (amount > 0)
        {
            CurrBet += amount;
        }
        betAmountSlider.value = CurrBet;
        betDisplay.text = "Current Bet: $" + CurrBet.ToString();
    }

    public void ResetBet()
    {
        CurrBet = 0;
        betAmountSlider.value = CurrBet;
        //Debug.Log("Reset bet. Total Bet: " + CurrBet.ToString());
    }

    public void hideBet()
    {
        // The script attached to deactive gameObject will also get deactive.
        // Make sure only children deactived.
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void showBet()
    {
        // The script attached to deactive gameObject will also get deactive.
        // Make sure only children deactived.
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ValueChangeCheck()
    {
        CurrBet = (int)betAmountSlider.value;
        betDisplay.text = "Current Bet: $" + CurrBet.ToString();
        //Debug.Log(betAmountSlider.value);
    }
}
