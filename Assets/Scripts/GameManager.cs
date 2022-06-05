using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Sprite[] cardSprites;

    public List<Card> deck;

    // Start is called before the first frame update
    void Start()
    {
        deck = new List<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ReshuffleDeck();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < 52; i++)
            {
                Card card = DealCard();
                Debug.Log("value: " + card.value + " suit: " + card.suit);
            }
        }
    }

    public Card DealCard()
    {
        Card temp = deck[0];
        deck.RemoveAt(0);
        return temp;
    }

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
        for (int i = 0; i < 15; i++)
        {
            int rand = (int)Random.Range(0, 52);
            int rand2 = (int)Random.Range(0, 52);
            Swap(rand, rand2);
        }
        // bubble shuffle
        for (int j = 0; j < 20; j++)
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
