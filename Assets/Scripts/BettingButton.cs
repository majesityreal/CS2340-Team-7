using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/**
 *  Author:         Zheng Yuan
 *  Date:           2022.06.16
 *  Version:        1.2
 *  
 *  Last update:    
 *                  2022.06.24  Change Text to TextMesh pro.
 *                  2022.06.23  Fix null reference bugs. Change static variable Name.
 *                  2022.06.20  Create an individual scene.
 *                  2022.06.16  Create the script for Betting Buttons.
 *  
 *  Script for Betting Stage's buttons.
 */
public class BettingButton : MonoBehaviour
{
    public static int CurrBet = 0;
        // The initial player balance.


    public GameObject moneyDisplay;
    public GameObject betDisplay;
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
        // Display button text.
        if (moneyDisplay != null)
        {
            moneyDisplay.GetComponent<TextMeshProUGUI>().SetText("Balance: $" + GameManager.PlayerMoney.ToString());
        }
        if (betDisplay != null)
        {
            betDisplay.GetComponent<TextMeshProUGUI>().SetText("Bet: $" + CurrBet.ToString());
        }
        if (betAmountSlider != null)
        {
            betAmountSlider.wholeNumbers = true;
            betAmountSlider.maxValue = GameManager.PlayerMoney;
        }
    }

    public void initBetStage()
    {
        // Dehide the bet stage UI.
        showUI(gameObject);

        // In case of null ref.
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        if (betAmountSlider == null)
        {
            betAmountSlider = GameObject.Find("BetAmountSlider").GetComponent<Slider>();
        }

        // Display button text.
        if (moneyDisplay != null)
        {
            moneyDisplay.GetComponent<TextMeshProUGUI>().SetText("Balance: $" + GameManager.PlayerMoney.ToString());
        }
        if (betDisplay != null)
        {
            betDisplay.GetComponent<TextMeshProUGUI>().SetText("Bet: $" + CurrBet.ToString());
        }

        // Initialize slider.
        if (betAmountSlider != null)
        {
            betAmountSlider.wholeNumbers = true;
            betAmountSlider.maxValue = GameManager.PlayerMoney;
            betAmountSlider.value = 0;
            betAmountSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        // Reset the current bet amount.
        CurrBet = 0;
    }

    public void ComfirmBet()
    {
        // Apply the change to the player balance. And Hide the UI.
        GameManager.PlayerMoney -= CurrBet;
        hideUI(gameObject);
        //Debug.Log("Comfirm Bet, total Bet: " + CurrBet.ToString());
        //Debug.Log("Total Money: " + PlayerMoney.ToString());
    }

    public void AddBet(int amount)
    {
        // If the new total bet is exceed total balance, make current bet max balance.
        if ((amount + CurrBet) > GameManager.PlayerMoney)
        {
            CurrBet = GameManager.PlayerMoney;
        }
        // Otherwise, add it to current bet.
        else if (amount > 0)
        {
            CurrBet += amount;
        }
        betAmountSlider.value = CurrBet;
        betDisplay.GetComponent<TextMeshProUGUI>().SetText("Bet: $" + CurrBet.ToString());
    }

    public void ResetBet()
    {
        CurrBet = 0;
        betAmountSlider.value = CurrBet;
        //Debug.Log("Reset bet. Total Bet: " + CurrBet.ToString());
    }

    public static void hideUI(GameObject gm)
    {
        // The script attached to deactive gameObject will also get deactive.
        // Make sure only children deactived.
        for (int i = 0; i < gm.transform.childCount; i++)
        {
            gm.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public static void showUI(GameObject gm)
    {
        // The script attached to deactive gameObject will also get deactive.
        // Make sure only children deactived.
        for (int i = 0; i < gm.transform.childCount; i++)
        {
            gm.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ValueChangeCheck()
    {
        CurrBet = (int)betAmountSlider.value;
        betDisplay.GetComponent<TextMeshProUGUI>().SetText("Bet: $" + CurrBet.ToString());
        //Debug.Log(betAmountSlider.value);
    }
}