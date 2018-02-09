using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour {
    //this field determines snake movement speed
    public float movementSpeed = 0.5f;

    //field holding body prefab
    public GameObject bodyTile;
    
    //field that holds all snake body parts transforms(positions)
    private List<Transform> bodyList = new List<Transform>();

    //field used to determine if snake should grow given step or just move
    private bool grow = false;

    //field used to hold recent tap positions
    private Vector2 touchOrigin = -Vector2.one;

    //field holding last given order from player: 0 - no order given; 1 - turn left; 2 - turn right
    private short command = 0;

    //local field that holds player score at given time - after game is over it is sent to ScoreScript
    private int points;

    //method that spawns initial snake body lenght, spawns first food instance and starts snake movement
	void Start ()
    {
        for (int i = 3; i >= 0; i--)
        {
            Vector2 position = new Vector2(0, i);
            GameObject body = Instantiate(bodyTile, position, Quaternion.identity);
            bodyList.Add(body.transform);
        }
        points = 0;
        GameManager.instance.foodScript.SpawnRegularFood();
        InvokeRepeating("Move", 0.5f, movementSpeed);
    }
	
    //method detects screen touches to set command to turn left or right
	void Update ()
    {
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;

                if (touchOrigin.x < Screen.width / 2)
                {
                    command = 1;
                }
                else if (touchOrigin.x > Screen.width / 2)
                {
                    command = 2;
                }
            }
        }
    }

    /*
     * method "moving" snake: head always moves forward,
     * if snake doesnt grow, last body piece is inserted into the gap;
     * if snake has to grow, new body piece is inserted into the gap 
     *  if player gave order to turn left or right(command == 1 or 2), rotate head into proper direction
     *  */
    void Move()
    {
        Vector2 direction = new Vector2(0, 1);
        Vector2 gap = transform.position;
        if(command == 1)
            transform.Rotate(0, 0, 90);
        else if (command == 2)
            transform.Rotate(0, 0, -90);
        transform.Translate(direction);
        command = 0;

        if (grow)
        {
            GameObject newBody = Instantiate(bodyTile, gap, gameObject.transform.rotation);
            bodyList.Insert(0, newBody.transform);
            grow = false;
        }
        else
        {
            bodyList.Last().position = gap;
            bodyList.Last().rotation = gameObject.transform.rotation;
            bodyList.Insert(0, bodyList.Last());
            bodyList.RemoveAt(bodyList.Count - 1);
        }
    }

    //method detects collision with objects on the map and acts accordingly
    void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.tag)
        {
            case "RegularFood":
                grow = true;
                points++;
                Destroy(collider.gameObject);
                GameManager.instance.UpdateScore(points);
                break;
            case "SpecialFood":
                grow = true;
                points += 10;
                Destroy(collider.gameObject);
                GameManager.instance.UpdateScore(points);
                break;
            case "Killer":
                GameManager.instance.Quit();
                ScoreScript.Points = points;
                break;
        }
    }
}
