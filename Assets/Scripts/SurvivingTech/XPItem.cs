using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */
 
public class XPItem : MonoBehaviour, IPickupItem
{
    [SerializeField] STAudio sound;
    [SerializeField] int xpAmount = 10;
    public void OnPickUp(PlayerController player) {
        sound = FindObjectOfType<STAudio>();
        player.addXP(xpAmount);
        sound.PlayGemPickup();
    }
    

}
