using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */
 
public class Tile : MonoBehaviour
{
    [SerializeField] Vector2Int tilePos;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<ScrollingWorld>().Add(gameObject, tilePos);
    }
}

