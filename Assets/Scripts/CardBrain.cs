using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBrain : MonoBehaviour
{
    public int index;
    public Image image;
    public bool isPlayerCard;
    public bool isSplitCard;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerCard)
        {
            if (GameManager.Instance.playerHand.GetIndex(index) == null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            else
            {
                image.sprite = GameManager.Instance.playerHand.GetIndex(index).sprite;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            }
        }
        else if (isSplitCard)
        {
            if (GameManager.Instance.splitButton.playerSplitHand.GetIndex(index) == null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            else
            {
                image.sprite = GameManager.Instance.splitButton.playerSplitHand.GetIndex(index).sprite;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            }
        }
        else
        {
            if (GameManager.Instance.dealerHand.GetIndex(index) == null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            else
            {
                image.sprite = GameManager.Instance.dealerHand.GetIndex(index).sprite;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            }
        }
        
    }
}
