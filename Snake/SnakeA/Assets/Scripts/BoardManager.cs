using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    //fields holding floor, wall and head of snake prefabs
    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject headTile;
    
    //field that holds transform for entire board
    private Transform boardHolder;

    //method spawning entire map
    void BoardSetup()
    {
        int columns = GameManager.instance.columns;
        int rows = GameManager.instance.rows;

        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate;
                if (x == -1 || y == -1 || x == columns || y == rows)
                {
                    toInstantiate = wallTile;
                }
                else
                {
                    toInstantiate = floorTile;
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
    }

    //method spawning head of the snake
    public void SpawnSnake()
    {
        GameObject head = headTile;
        Instantiate(head, new Vector3(0, 4, 0f), Quaternion.identity);
    }

    //method that calls for spawning map and snake
    public void SetupScene()
    {
        BoardSetup();
        SpawnSnake();
    }
}
