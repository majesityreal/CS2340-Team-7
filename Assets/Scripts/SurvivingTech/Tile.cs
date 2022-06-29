using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Vector2Int tilePos;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<ScrollingWorld>().Add(gameObject, tilePos);
    }
}

