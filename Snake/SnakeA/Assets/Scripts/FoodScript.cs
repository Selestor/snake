using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script responsible for spawning food
public class FoodScript : MonoBehaviour {
    //fields holding both food types prefabs
    public GameObject regularFoodTile;
    public GameObject specialFoodTile;

    //fields holding instances of food - used to check if there is only one on the map at given time
    private GameObject regularFood;
    private GameObject specialFood;

    //field to determine if special food countdown to spawn has already started
    private bool isInvoking;

    //fields holding map size - needed for food respawn at random location 
    private int columns;
    private int rows;

    //method used to delay spawn of special food by a random amount of seconds
    public void InvokeSpawnSpecialFood()
    {
        isInvoking = true;
        int randomTime = Random.Range(5,20);
        Invoke("SpawnSpecialFood", randomTime);
    }

    //method that makes sure spawn location is random and is not occupied by other game elements (snake itself, other food)
    Vector3 RandomPosition(int x, int y)
    {
        int layerMask = 1 << 9;
        RaycastHit2D hit;
        Vector2 randomPosition = new Vector3(0f, 0f);
        do
        {
            int randomX = Random.Range(0, x);
            int randomY = Random.Range(0, y);

            randomPosition.x = randomX;
            randomPosition.y = randomY;

            hit = Physics2D.Linecast(randomPosition, randomPosition, layerMask);
        } while (hit.transform != null);

        return randomPosition;
    }

    //method that actually spawns food - parameter is type of food: special or regular
    void LayoutFoodAtRandom(GameObject foodTile)
    {
        columns = GameManager.instance.columns;
        rows = GameManager.instance.rows;
        Vector3 randomPosition = RandomPosition(columns, rows);
        GameObject tile = foodTile;
        if (tile.tag == "RegularFood")
            regularFood = Instantiate(tile, randomPosition, Quaternion.identity);
        if (tile.tag == "SpecialFood")
        {
            specialFood = Instantiate(tile, randomPosition, Quaternion.identity);
            isInvoking = false;
        }
    }

    //method that calls spawning regular food
    public void SpawnRegularFood()
    {
        LayoutFoodAtRandom(regularFoodTile);
    }

    //method that calls spawning special food
    public void SpawnSpecialFood()
    {
        LayoutFoodAtRandom(specialFoodTile);
    }

    //method that makes sure there is always food on the map
    private void Update()
    {
        if (regularFood == null)
        {
            SpawnRegularFood();
        }
        if(specialFood == null && !isInvoking)
        {
            InvokeSpawnSpecialFood();
        }
    }
}
