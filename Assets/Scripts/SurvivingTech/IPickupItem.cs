using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */


[SerializeField] STAudio sound;

public interface IPickupItem
{
    public void OnPickUp(PlayerController player);
    [SerializeField] STAudio sound;
    sound = FindObjectOfType<STAudio>();
    sound.PlayGemPickup();

}
