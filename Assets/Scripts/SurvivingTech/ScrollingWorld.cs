using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */
 
public class ScrollingWorld : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector2Int currentTilePos = new Vector2Int(0, 0);
    [SerializeField] Vector2Int playerTilePos;
    Vector2Int onTilePlayerPos;
    [SerializeField] float tileSize = 51.1f;
    GameObject[,] tiles;

    // 3x3, accounting for tiles that will have diff stuff on them to be generated
    [SerializeField] int tileHCount; // number of horizontal tiles
    [SerializeField] int tileVCount; // number of vertical tiles

    // do not set higher than tile count
    [SerializeField] int fovHeight = 3;
    [SerializeField] int fovWidth = 3;

    // Start is called before the first frame update
    void Awake()
    {
        tiles = new GameObject[tileHCount, tileVCount];
    }

    private void Update()
    {
        playerTilePos.x = (int) (playerTransform.position.x / tileSize);
        playerTilePos.y = (int) (playerTransform.position.y / tileSize);

        // account for when player goes into the negative tile pos
        playerTilePos.x -= playerTransform.position.x % tileSize < 0 ? 1 : 0;
        playerTilePos.y -= playerTransform.position.y % tileSize < 0 ? 1 : 0;

        if (currentTilePos != playerTilePos)
        {
            currentTilePos = playerTilePos;
            onTilePlayerPos.x = CalculateGridPosition(onTilePlayerPos.x, true);
            onTilePlayerPos.y = CalculateGridPosition(onTilePlayerPos.y, false);
            UpdateTilesInView();
        }
    }

    private int CalculateGridPosition(float currentValue, bool isHorizontalValue)
    {
        if (isHorizontalValue) {
            if (currentValue >= 0) {
               currentValue = currentValue % tileHCount;
            } else {
                currentValue += 1;
                currentValue = tileHCount -1 + currentValue % tileHCount;
            }
        } else {
            if (currentValue >= 0) {
                currentValue = currentValue % tileVCount;
            } else {
                currentValue += 1;
                currentValue = tileVCount -1 + currentValue % tileVCount;
            }
        }
        return (int)currentValue;
    }

    private void UpdateTilesInView()
    {
        // fov relative to player, not player position
        for(int fov_x = -(fovWidth/2); fov_x <= fovWidth/2; fov_x++)
            {
                for (int fov_y = -(fovHeight/2); fov_y <= fovHeight/2; fov_y++)
                {
                    int tileUpdate_x = CalculateGridPosition(playerTilePos.x + fov_x, true);
                    int tileUpdate_y = CalculateGridPosition(playerTilePos.y + fov_y, false);

                    GameObject tile = tiles[tileUpdate_x, tileUpdate_y];
                    tile.transform.position = CalculateTilePosition(playerTilePos.x + fov_x, playerTilePos.y + fov_y);
                }
            }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    public void Add(GameObject tile, Vector2Int tilePos)
    {
        tiles[tilePos.x, tilePos.y] = tile;
    }
}
