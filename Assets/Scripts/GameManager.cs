using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Scripts
    private Audio sound;
    public SplitButton splitButton;

    // Start is called before the first frame update
    void Start()
    {
        splitButton = FindObjectOfType<SplitButton>();
        sound = FindObjectOfType<Audio>();
        betButton = gameObject.AddComponent(typeof(BettingButton)) as BettingButton;
        totalMoneyWon = 0;
        totalCardsDealt = 0;
        gamesWon = 0;
        gamesLost = 0;
        deck = new List<Card>();
        InitalizeDeck();
        InitializeGame();
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
        splitButton.InitSplitHand();
    }

    // method for dealing the next card
    public void Hit()
    {
        totalCardsDealt++;
        playerHand.AddToHand(DealCard());
        Debug.Log("Hit: Player: " + playerHand.GetScore() + " Dealer: " + dealerHand.GetScore());

        if (canDouble)
        {
            canDouble = false;
        }
        if (splitButton.canSplit)
        {
            splitButton.canSplit = false;
        }

        if (playerHand.GetScore() > 21)
        {
            totalMoneyWon -= BettingButton.currBet;
            gamesLost--;
            if (splitButton.haveSplit)
            {
                // TODO: Lose Screen
                Debug.Log("Hit: Player Lost Split");
                if (standCount == 2)
                {
                    InitializeGame();
                    return;
                }
                
                standCount = 2;
                splitButton.SwitchHand();
            }
            else
            {
                Debug.Log("Hit: Player Lost");
                // TODO: Lose Screen
                InitializeGame();
            }
        }
    }

    // stop dealing to the player
    public void Stand()
    {
        if (splitButton.haveSplit && standCount == 0)
        {
            standCount++;
            splitButton.SwitchHand();
            return;
        }

        while (dealerHand.GetSize() < 8 && dealerHand.GetScore() < 17) {
            dealerHand.AddToHand(DealCard());
        }

        Debug.Log("Player: " + playerHand.GetScore() + " Dealer: " + dealerHand.GetScore());

        if (dealerHand.GetScore() > 21 || playerHand.GetScore() > dealerHand.GetScore())
        {
            totalMoneyWon += BettingButton.currBet;
            gamesWon++;
            // TODO: Win Screen
            BettingButton.playerMoney += BettingButton.currBet;
            Debug.Log("Player win");
        }
        else
        {
            gamesLost--;
            // TODO: Lose Screen
            Debug.Log("Dealer win");
        }

        if (standCount == 1)
        {
            splitButton.SwitchHand();
            standCount++;
            Stand();
            return;
        }

        InitializeGame();
    }

    // equivalent to "hit" but instead doubling the bet
    public void Double ()
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
        deck.Clear();
        sound.PlayShuffle();
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
    }

    public void QuitGame()
    {
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