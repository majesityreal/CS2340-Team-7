using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour, IPickupItem
{
    [SerializeField] float healAmount = 10;
    public void OnPickUp(PlayerController player) {
        player.Heal(healAmount);
    }
    

}
