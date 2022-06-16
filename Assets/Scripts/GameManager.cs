using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Sprite[] cardSprites;

    public List<Card> deck;

    public int betAmount;

    public Card[] playerHand;
    public Card[] dealerHand;
    private int playerSize;
    private int dealerSize;

    // statistics variables:
    private int totalMoneyWon;
    private int totalCardsDealt;
    private int gamesWon;
    private int gamesLost;

    // Start is called before the first frame update
    void Start()
    {
        deck = new List<Card>();
        playerHand = new Card[8];
        dealerHand = new Card[8];
        ReshuffleDeck();
        playerHand[0] = DealCard();
        playerHand[1] = DealCard();
        dealerHand[0] = DealCard();
        dealerHand[1] = DealCard();
        playerSize = 2;
        dealerSize = 2;
    }

    // Update is called once per frame
    void Update()
    {
        // test for shuffling default to player
        if (Input.GetKeyDown(KeyCode.S))
        {
            ReshuffleDeck();
            playerHand = new Card[8];
            playerHand[0] = DealCard();
            playerHand[1] = DealCard();
            playerSize = 2;
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
        if (playerHand[playerSize] == null)
        {
            return;
        }

        playerHand[playerSize] = DealCard();

        if (GetScore(playerHand, playerSize) > 21)
        {
            // TODO: Losing Screen, Lose Money
        }

        playerSize++;
    }

    // stop dealing to the player
    public void Stand()
    {
        int dealerScore = GetScore(dealerHand, dealerSize);
        int playerScore = GetScore(playerHand, dealerSize);

        while (dealerSize < 8 && 21 - dealerScore < 5) {
            dealerHand[dealerScore] = DealCard();
            dealerScore = GetScore(dealerHand, dealerSize);
            dealerSize++;
        }

        if (dealerScore == playerScore)
        {
            // TODO: Draw Screen, Same Money
        }
        else if (dealerScore > 21 || playerScore > dealerScore)
        {
            // TODO: Winning Screen, Gain Money
        }
        else
        {
            // TODO: Losing Screen, Lose Money
        }
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

    private int GetScore(Card[] actor, int size)
    {
        int score = 0;
        int numOfAce = 0;

        // Calculates the total score. Stops adding when reaches null.
        // When Ace is added, adds 11 instead of 1.
        for (int i = 0; i < size; i++) 
        {
            if (actor[i].value == 1)
            {
                score += 10;
                numOfAce++; 
            }
            else
            {
                score += actor[i].value;
            }
        }

        // If score overflows and hand contains Ace, subtracts score until score is less than 21.
        while (numOfAce != 0 && score > 21)
        {
            score -= 10;
            numOfAce--;
        }
        return score;
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
                deck.Add(new Card(Mathf.Min(j, 10), i, cardSprites[(i * 13) + j]));
            }
        }

        // swap shuffle
        for (int i = 0; i < 25; i++)
        {
            int rand = (int)Random.Range(0, 52);
            int rand2 = (int)Random.Range(0, 52);
            Swap(rand, rand2);
        }

        // bubble shuffle
        for (int j = 0; j < 25; j++)
        {
            for (int i = 0; i < 51; i++)
            {
                int rand = (int)Random.Range(0, 2);
                if (rand == 0)
                {
                    Swap(i, i + 1);
                }
            }
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
