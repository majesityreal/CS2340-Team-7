using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Sprite[] cardSprites;

    public List<Card> deck;

    public Hand playerHand;
    public Hand dealerHand;

    // Used for splitted hands
    private int standCount;

    BettingButton betButton;

    public bool canDouble;

    // statistics variables:
    private int totalMoneyWon;
    private int totalCardsDealt;
    private int gamesWon;
    private int gamesLost;
    private int income;

    // Scripts
    private Audio sound;

    // lose / win screen
    // public GameObject loseScreen;
    // public GameObject winScreen;
    public GameObject ResultStage;
    public TextMeshProUGUI BalanceChangeText;
    public TextMeshProUGUI CurrBalanceText;




    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            ResetGame();
            DestroyImmediate(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ResultStage == null)
        {
            ResultStage = GameObject.Find("ResultStage");
        }

        sound = FindObjectOfType<Audio>();
        totalMoneyWon = 0;
        totalCardsDealt = 0;
        income = 0;
        gamesWon = 0;
        gamesLost = 0;
        deck = new List<Card>();
        BettingButton.playerMoney = 100;
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        // test for shuffling default to player
        if (Input.GetKeyDown(KeyCode.S))
        {
            InitializeGame();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("First Card" + playerHand.GetIndex(0).value.ToString());
            Debug.Log("Second Card" + playerHand.GetIndex(1).value.ToString());
        }
    }

    public void ResetGame()
    {
        InitalizeDeck();
        InitializeGame();
        SplitButton.InitSplitHand();
    }

    public void InitializeGame()
    {
        if (deck.Count < 26)
        {
            InitalizeDeck();
        }
        playerHand = new Hand();
        dealerHand = new Hand();
        standCount = 0;
        canDouble = true;
        ReshuffleDeck();
        playerHand.AddToHand(DealCard());
        playerHand.AddToHand(DealCard());
        dealerHand.AddToHand(DealCard());
        dealerHand.AddToHand(DealCard());
        CardBrain.isDealerTurn = false;
        income = 0;
        hideResult();
    }

    // method for dealing the next card
    public void Hit()
    {
        if (playerHand.GetScore() > 21) {
            Debug.Log("Stop hitting");
            return;
        }

        totalCardsDealt++;
        playerHand.AddToHand(DealCard());
        Debug.Log("Hit: Player: " + playerHand.GetScore() + " Dealer: " + dealerHand.GetScore());

        if (canDouble)
        {
            canDouble = false;
        }
        if (SplitButton.canSplit)
        {
            SplitButton.canSplit = false;
        }

        if (playerHand.GetScore() > 21)
        {
            totalMoneyWon -= BettingButton.currBet;
            income -= BettingButton.currBet;
            gamesLost++;
            BettingButton.playerMoney -= BettingButton.currBet;
            Debug.Log("Player Money left: " + BettingButton.playerMoney);

            if (SplitButton.haveSplit)
            {
                gamesLost++;
                // this happens if the player loses the second hand
                if (standCount == 2)
                {
                    showResult();
                    return;
                }
                
                standCount = 2;
                switchHand();
            }
            else
            {
                Debug.Log("Hit: Player Lost");
                // pulls up lose Screen
                showResult();
            }
        }
    }

    // stop dealing to the player
    public void Stand()
    {
        if (SplitButton.haveSplit && standCount == 0)
        {
            standCount++;
            switchHand();
            return;
        }

        CardBrain.isDealerTurn = true;

        while (dealerHand.GetSize() < 8 && dealerHand.GetScore() < 17) {
            dealerHand.AddToHand(DealCard());
        }

        Debug.Log("Player: " + playerHand.GetScore() + " Dealer: " + dealerHand.GetScore());

        if (dealerHand.GetScore() > 21 || playerHand.GetScore() > dealerHand.GetScore())
        {
            totalMoneyWon += BettingButton.currBet;
            income += BettingButton.currBet;
            gamesWon++;
            // TODO: Win Screen
            BettingButton.playerMoney += BettingButton.currBet;
            Debug.Log("Player win");
            showResult();
        }
        else
        {
            BettingButton.playerMoney -= BettingButton.currBet;
            Debug.Log("Player Money left: " + BettingButton.playerMoney);
            gamesLost++;
            // TODO: Lose Screen
            Debug.Log("Dealer win");
            showResult();
        }

        if (standCount == 1)
        {
            switchHand();
            standCount++;
            Stand();
            return;
        }

    }

    // equivalent to "hit" but instead doubling the bet
    public void Double()
    {
        if (!canDouble)
        {
            Debug.Log("Cannot Double");
            return;
        }
        BettingButton.playerMoney -= BettingButton.currBet;
        BettingButton.currBet *= 2;
        Hit();
        Stand();
    }

    //public void PlayerLose()
    //{
    //    StartCoroutine(PlayerLostCoroutine());
    //}

    //private IEnumerator PlayerLostCoroutine()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    loseScreen.SetActive(true);
    //}

    //public void PlayerWin()
    //{
    //    StartCoroutine(PlayerWinCoroutine());
    //}

    //private IEnumerator PlayerWinCoroutine()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    winScreen.SetActive(true);
    //}


    public void showResult()
    {
        for (int i = 0; i < ResultStage.transform.childCount; i++)
        {
            ResultStage.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (income >= 0)
        {
            BalanceChangeText.SetText("+ $" + income.ToString());
        }
        else
        {
            BalanceChangeText.SetText("+ $" + (0 - income).ToString());
        }
        
        CurrBalanceText.SetText(BettingButton.playerMoney.ToString());
        
    }

    public void hideResult()
    {
        for (int i = 0; i < ResultStage.transform.childCount; i++)
        {
            ResultStage.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // removes and returns a Card from the deck
    public Card DealCard()
    {
        Card temp = deck[0];
        deck.RemoveAt(0);
        totalCardsDealt++;
        return temp;
    }

    // set up deck to be dealt. Note: before referencing any cards in the deck, this method must be called!
    public void InitalizeDeck()
    {
        if (deck == null)
        {
            deck = new List<Card>();
        }
        else
        {
            deck.Clear();
        }
        //sound.PlayShuffle();
        // initializing a deck List
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                deck.Add(new Card(Mathf.Min(j + 1, 10), i, cardSprites[(i * 13) + j]));
            }
        }
    }

    public void ReshuffleDeck()
    {
        // Fisher-Yates Shuffle
        for (int i = 0; i < deck.Count - 2; i++)
        {
            Swap(i, (int) Random.Range(0, deck.Count));
        }
    }

    // reloads the blackjack scene to start again
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResetGame();
    }

    public void QuitGame()
    {
        // we are doing this because this is a singleton!
        DestroyImmediate(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    // helper method, swaps two indexes of the deck list
    private void Swap(int i, int j)
    {
        Card temp = deck[i];
        deck.Insert(i, deck[j]);
        deck.RemoveAt(i + 1);
        deck.Insert(j, temp);
        deck.RemoveAt(j + 1);
    }

    // Change player's current hand to split hand.
    public void switchHand()
    {
        Hand temp = playerHand;
        playerHand = SplitButton.playerSplitHand;
        SplitButton.playerSplitHand = temp;
    }
}

public class Card
{
    public int suit;
    public int value;
    public Sprite sprite;

    public Card(int value, int suit, Sprite sprite)
    {
        this.value = value;
        this.suit = suit;
        this.sprite = sprite;
    }

}

public class Hand
{
    private Card[] hand;
    private int size;
    private int score;
    private int numOfAce;

    public Hand()
    {
        this.size = 0;
        this.score = 0;
        this.numOfAce = 0;
        hand = new Card[11];
    }

    public void AddToHand(Card card)
    {
        if (size >= this.hand.Length)
        {
            Debug.Log("This cannot happen. Check code / card list for errors.");
            return;
        }

        this.hand[size] = card;
        UpdateScore();
        size++;
    }

    private void UpdateScore()
    {
        this.score += hand[size].value;

        if (hand[size].value == 1) {
            this.score += 10;
            this.numOfAce++;
        }

        while (this.score > 21 && numOfAce != 0)
        {
            this.score -= 10;
            this.numOfAce--;
        }
    }

    // Getters
    public int GetSize()
    {
        return this.size;
    }

    public int GetScore()
    {
        return this.score;
    }

    public Card GetIndex(int i)
    {
        return this.hand[i];
    }

    // Used for splitting.
    public Card RemoveLast()
    {
        size--;
        Card lastCard = this.hand[size];
        this.hand[size] = null;
        if (lastCard.value == 1)
        {
            this.numOfAce--;
        }
        this.score = hand[0].value;
        return lastCard;
    }
}