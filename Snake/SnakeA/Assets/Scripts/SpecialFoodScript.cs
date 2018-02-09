using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFoodScript : MonoBehaviour {
    //method that turns on and off special foods sprite - makes it blink
    void Blink()
    {
        SpriteRenderer spriteRend = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRend.enabled)
            spriteRend.enabled = false;
        else spriteRend.enabled = true;
    }

    //method that makes special food blink after 7 sec and destroys it after 10 sec
    void Start ()
    {
        InvokeRepeating("Blink", 7, 0.15f);
        Invoke("Disappear", 10);
	}

    //method that destroys food
    void Disappear()
    {
        Destroy(gameObject);
    }
}
