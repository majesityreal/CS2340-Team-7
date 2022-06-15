using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBrain : MonoBehaviour
{
    private GameManager gameManager;
    public int index;
    public Image image;
    public bool isPlayerCard;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerCard)
        {
            if (gameManager.playerHand[index] == null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            else
            {
                image.sprite = gameManager.playerHand[index].sprite;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            }
        }
        else
        {
            if (gameManager.dealerHand[index] == null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            else
            {
                image.sprite = gameManager.dealerHand[index].sprite;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            }
        }
        
    }
}
