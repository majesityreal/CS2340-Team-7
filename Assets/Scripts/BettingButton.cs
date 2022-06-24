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

    public GameObject cover;

    // Start is called before the first frame update
    void Start()
    {
        currBet = 0;
        betStage = GameObject.Find("BetStage");
        gameManager = FindObjectOfType<GameManager>();
        betAmountSlider = GameObject.Find("BetAmountSlider").GetComponent<Slider>();

        betAmountSlider.maxValue = playerMoney;

        if (moneyDisplay == null)
        {
            GameObject moDisplay = GameObject.Find("TotalMoneyDisplay");
            moneyDisplay = moDisplay.GetComponent<Text>();
        }

        if (betDisplay == null)
        {
            GameObject beDisplay = GameObject.Find("CurrentBet");
            betDisplay = beDisplay.GetComponent<Text>();
        }

        moneyDisplay.text = "Balance: $" + playerMoney.ToString();
        betDisplay.text = "Current Bet: $" + currBet.ToString();

        betAmountSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        currBet = 0;
    }

    // Update is called once per frame
    void Update()
    {
        betAmountSlider.value = currBet;
    }

    public void ComfirmBet()
    {
        //gameManager.betAmount = currBet;
        playerMoney -= currBet;
        hideBet();
        GameManager.Instance.ResetGame();
        Debug.Log("Comfirm Bet, total Bet: " + currBet.ToString());
    }

    public void AddBet(int amount)
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

    public void AddBet()
    {
        AddBet(inputBet);
    }

    public void ResetBet()
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
