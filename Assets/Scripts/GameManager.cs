using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Sprite[] cardSprites;

    public List<Card> deck;

    public int betAmount;

    public Hand playerHand;
    public Hand dealerHand;

    // statistics variables:
    private int totalMoneyWon;
    private int totalCardsDealt;
    private int gamesWon;
    private int gamesLost;

    // Start is called before the first frame update
    void Start()
    {
        deck = new List<Card>();
        playerHand = new Hand();
        dealerHand = new Hand();
        ReshuffleDeck();
        playerHand.AddToHand(DealCard());
        playerHand.AddToHand(DealCard());
        dealerHand.AddToHand(DealCard());
        dealerHand.AddToHand(DealCard());
    }

    // Update is called once per frame
    void Update()
    {
        // test for shuffling default to player
        if (Input.GetKeyDown(KeyCode.S))
        {
            ReshuffleDeck();
            playerHand = new Hand();
            playerHand.AddToHand(DealCard());
            playerHand.AddToHand(DealCard());
        }
    }

    public void PlaceBet()
    {
        Debug.Log("I am changing");
        // check for if text is valid
    }

    // method for dealing the next card
    public void Hit()
    {
        playerHand.AddToHand(DealCard());
        // TODO: Method for checking end.
    }

    // stop dealing to the player
    public void Stand()
    {
        while (dealerHand.GetSize() < 8 && dealerHand.GetScore() < 17) {
            dealerHand.AddToHand(DealCard());
        }

        // TODO: Method for checking end & winner.
    }

    // equivalent to "hit" but instead doubling the bet
    public void Double ()
    {
        betAmount *= 2;
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
    public void ReshuffleDeck()
    {
        deck.Clear();
        // initializing a deck List
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                deck.Add(new Card(Mathf.Min(j + 1, 10), i, cardSprites[(i * 13) + j]));
            }
        }

        // Fisher-Yates Shuffle
        for (int i = 0; i < 50; i++)
        {
            Swap(i, (int) Random.Range(0, 52));
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
        hand = new Card[8];
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
}