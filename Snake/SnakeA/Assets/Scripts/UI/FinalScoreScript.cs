using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FinalScoreScript : MonoBehaviour {

    public Text finalScoreText;

	// Use this for initialization
	void Start () {
        finalScoreText = GetComponent<Text>();
        finalScoreText.text = ScoreScript.Points.ToString();
	}
}
