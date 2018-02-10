using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static int playerScore = 0;
    public Text playerScoreText;

    public static GameObject[] intersectionObjects;
    private Vector3[] intersections;

    // Use this for initialization
    void Start () {
        intersectionObjects = GameObject.FindGameObjectsWithTag("Intersection");
    }
	
	// Update is called once per frame
	void Update () {
        playerScoreText.text = "Player Score : " + playerScore;
	}
}
