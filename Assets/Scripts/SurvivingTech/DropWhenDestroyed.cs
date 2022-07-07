using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWhenDestroyed : MonoBehaviour
{
    [SerializeField] GameObject drop;
    [Range(0, 1), SerializeField] float dropChance = 0.5f;

    void OnDestroy()
    {
        if (Random.value < dropChance)
        {
            if(!this.gameObject.scene.isLoaded) return;
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
}
