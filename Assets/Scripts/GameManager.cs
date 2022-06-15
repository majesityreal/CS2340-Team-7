using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Sprite[] cardSprites;

    public List<Card> deck;

    public int betAmount;

    int currIndex;
    public Card[] playerHand;
    public Card[] dealerHand;

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
        for (int i = 2; i < playerHand.Length; i++)
        {
            if (playerHand[i] == null)
            {
                currIndex = i;
                break;
            }
        }
        playerHand[currIndex] = DealCard();
        return;
    }

    // stop dealing to the player
    public void Stand()
    {

    }

    // equivalent to "hit" but instead doubling the bet
    public void Double ()
    {
        // TODO - check if player can make bet, then do this
        betAmount *= 2;

        Hit();

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
                deck.Add(new Card(j, i, cardSprites[(i * 13) + j]));
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
