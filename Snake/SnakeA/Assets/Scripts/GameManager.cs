using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //fields that determine map size
    public int columns = 10;
    public int rows = 15;

    //field holding instance of GameManager; makes sure there is only one
    public static GameManager instance = null;

    //fields holding scripts
    public BoardManager boardScript;
    public FoodScript foodScript;

    //field holds score text from top of the screen
    public Text scoreText;

    //field that holds scene changing script
    private LoadSceneOnClick sceneLoader;
    
    //method loads all necessary scripts, makes sure GameManager is a singleton, calls script spawning scene, food and sets score to 0 
    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
        foodScript = GetComponent<FoodScript>();
        sceneLoader = GetComponent<LoadSceneOnClick>();

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        boardScript.SetupScene();
        foodScript.InvokeSpawnSpecialFood();
        UpdateScore(0);
    }

    //method that updates score
    public void UpdateScore(int points)
    {
        scoreText.text = "Score: " + points;
    }

    //method that destroys GameManager and loads game over scene
    public void Quit()
    {
        Destroy(gameObject);
        sceneLoader.LoadByIndex(2);
    }
}
