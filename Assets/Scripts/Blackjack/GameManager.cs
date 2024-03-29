using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Sprite[] cardSprites;

    public List<Card> deck;

    public Hand playerHand;
    public Hand dealerHand;

    // Used for splitted hands
    public static int StandCount;

    BettingButton betButton;

    public bool canDouble;

    // Scripts
    private Audio sound;

    // lose / win screen
    // public GameObject loseScreen;
    // public GameObject winScreen;
    public GameObject ResultStage;
    public GameObject BetStage;
    public TextMeshProUGUI BalanceChangeText;
    public TextMeshProUGUI PlayerBalanceText;

    public TextMeshProUGUI PlayerHandPointText;
    public TextMeshProUGUI DealerHandPointText;

    public static int PlayerMoney = 100;
    public int moneyAtStart;

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

        if (BetStage == null)
        {
            ResultStage = GameObject.Find("BetStage");
        }

        sound = FindObjectOfType<Audio>();
        deck = new List<Card>();
        PlayerMoney = 100;
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
        PlayerHandPointText.SetText("Player Hand Value: " + playerHand.GetScore().ToString());
        if (CardBrain.showDealerHand)
        {
            DealerHandPointText.SetText("Dealer Hand Value: " + dealerHand.GetScore().ToString());
        } else
        {
            DealerHandPointText.SetText("");
        }
        
    }

    public void ResetGame()
    {
        InitalizeDeck();
        InitializeGame();
        SplitButton.InitSplitHand();
        BettingButton.showUI(BetStage);
    }

    public void InitializeGame()
    {
        if (deck.Count < 26)
        {
            InitalizeDeck();
        }
        playerHand = new Hand();
        dealerHand = new Hand();
        moneyAtStart = PlayerMoney;
        StandCount = 1;
        canDouble = true;
        ReshuffleDeck();
        playerHand.AddToHand(DealCard());
        playerHand.AddToHand(DealCard());
        dealerHand.AddToHand(DealCard());
        dealerHand.AddToHand(DealCard());
        CardBrain.showDealerHand = false;
        PlayerBalanceText.SetText("Balance: $" + PlayerMoney.ToString());
        hideResult();
    }

    // method for dealing the next card
    public void Hit()
    {
        if (playerHand.GetScore() > 21)
        {
            Debug.Log("Stop hitting");
            return;
        }
        sound.PlayFlipCard();
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
            PlayerMoney -= BettingButton.CurrBet;
            Debug.Log("Player Money left: " + PlayerMoney);
            StandCount--;

            if (StandCount > 0)
            {
                //// this happens if the player loses the second hand
                //if (StandCount == 0)
                //{
                //    showResult();
                //    return;
                //}
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
        // Stand count 0 : All hand finished.
        // Stand count 1 : One Hand Remain.
        // Stand count 2 : Two Hand Remain.
        
        StandCount--;

        if (StandCount == 0)
        {
            CardBrain.showDealerHand = true;
            // Used for flipping the dealer's first card
            while (dealerHand.GetSize() < 11 && dealerHand.GetScore() < 17)
            {
                sound.PlayFlipCard2();
                dealerHand.AddToHand(DealCard());
            }
        }

        Debug.Log("Player: " + playerHand.GetScore() + " Dealer: " + dealerHand.GetScore());

        // Player wins
        if (dealerHand.GetScore() > 21 || playerHand.GetScore() > dealerHand.GetScore())
        {
            PlayerMoney += BettingButton.CurrBet;
            Debug.Log("Player win");

            if (StandCount ==1)
            {
                switchHand();
                return;
            }
            showResult();
        }
        else if (dealerHand.GetScore() == playerHand.GetScore()) // Draw
        {
            Debug.Log("It is a draw");
            if (StandCount == 1)
            {
                switchHand();
                return;
            }
            showResult();
        }
        else // Lost 
        {
            PlayerMoney -= BettingButton.CurrBet;
            Debug.Log("Player Money left: " + PlayerMoney);
            Debug.Log("Dealer win");
            if (StandCount == 1)
            {
                switchHand();
                return;
            }
            showResult();
            PlayerBalanceText.SetText("Balance: $" + PlayerMoney.ToString());
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
        BettingButton.CurrBet *= 2;
        Hit();
        Stand();
    }

    public void showResult()
    {
        CardBrain.showDealerHand = true;

        for (int i = 0; i < ResultStage.transform.childCount; i++)
        {
            ResultStage.transform.GetChild(i).gameObject.SetActive(true);
        }

        if (BettingButton.CurrBet == 0)
        {
            BalanceChangeText.SetText("No Balance Change");
        }
        else if (PlayerMoney > moneyAtStart)
        {
            if (SplitButton.haveSplit)
            {
                BalanceChangeText.SetText("You won! +$" + (BettingButton.CurrBet * 2).ToString());
            }
            else
            {
                Debug.Log(StandCount);
                BalanceChangeText.SetText("You won! +$" + BettingButton.CurrBet.ToString());
            }
        }
        else if (playerHand.GetScore() == dealerHand.GetScore() || PlayerMoney == moneyAtStart)
        {
            BalanceChangeText.SetText("You tied! Push!");
        }
        else
        {
            if (SplitButton.haveSplit)
            {
                BalanceChangeText.SetText("You lost! -$" + (BettingButton.CurrBet * 2).ToString());
            }
            else
            {
                BalanceChangeText.SetText("You lost! -$" + BettingButton.CurrBet.ToString());
            }
        }
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
            Swap(i, (int)Random.Range(0, deck.Count));
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

        if (hand[size].value == 1)
        {
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