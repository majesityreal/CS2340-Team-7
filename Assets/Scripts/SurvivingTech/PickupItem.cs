using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */
 
public class PickupItem : MonoBehaviour
{
    [SerializeField] STAudio sound;
    void OnTriggerEnter2D(Collider2D collision) {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null) {
            GetComponent<IPickupItem>().OnPickUp(player);
            Destroy(gameObject);
            sound.PlayGemPickup();
        }
    }
}
