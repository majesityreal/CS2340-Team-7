using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision) {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null) {
            GetComponent<IPickupItem>().OnPickUp(player);
            Destroy(gameObject);
        }
    }
}
