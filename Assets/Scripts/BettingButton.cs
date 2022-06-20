using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BettingButton : MonoBehaviour
{
    public static int currBet = 0;
    public static int playerMoney = 100;
    public static int inputBet;
    public Text moneyDisplay;
    public Text betDisplay;
   

    private GameObject betStage;
    private GameManager gameManager;
    private Slider betAmountSlider;

    // Start is called before the first frame update
    void Start()
    {
        initBet();
        betStage = GameObject.Find("BetStage");
        gameManager = FindObjectOfType<GameManager>();
        betAmountSlider = GameObject.Find("BetAmountSlider").GetComponent<Slider>();

        betAmountSlider.maxValue = playerMoney;

        moneyDisplay.text = "Balance: $" + playerMoney.ToString();
        betDisplay.text = "Current Bet: $" + currBet.ToString();

        betAmountSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {
        betAmountSlider.value = currBet;
    }

    // Initalize player bet amount.
    public void initBet()
    {
        currBet = 0;
    }

    public void comfirmBet()
    {
        //gameManager.betAmount = currBet;
        playerMoney = playerMoney - currBet;
        hideBet();
        Debug.Log("Comfirm Bet, total Bet: " + currBet.ToString());
    }

    public void addBet(int amount)
    {
        if ((amount + currBet) > playerMoney)
        {
            currBet = playerMoney;
        }
        else if (amount > 0)
        {
            currBet += amount;
        }
        betDisplay.text = "Current Bet: $" + currBet.ToString();
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
        betAmountSlider.value = currBet;
        Debug.Log("Reset bet. Total Bet: " + currBet.ToString());
    }

    public void hideBet()
    {
        betStage.SetActive(false);
    }

    public void showBet()
    {
        betStage.SetActive(true);
    }

    public void ValueChangeCheck()
    {
        currBet = (int)betAmountSlider.value;
        betDisplay.text = "Current Bet: $" + currBet.ToString();
        Debug.Log(betAmountSlider.value);
    }
}
