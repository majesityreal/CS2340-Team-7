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
    public bool isSplitCard;

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
            if (gameManager.playerHand.GetIndex(index) == null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            else
            {
                image.sprite = gameManager.playerHand.GetIndex(index).sprite;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            }
        }
        else if (isSplitCard)
        {
            if (gameManager.splitButton.playerSplitHand.GetIndex(index) == null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            else
            {
                image.sprite = gameManager.splitButton.playerSplitHand.GetIndex(index).sprite;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            }
        }
        else
        {
            if (gameManager.dealerHand.GetIndex(index) == null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            else
            {
                image.sprite = gameManager.dealerHand.GetIndex(index).sprite;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            }
        }
        
    }
}
