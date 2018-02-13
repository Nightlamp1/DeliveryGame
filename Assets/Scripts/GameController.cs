using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static int playerScore = 0;
    public Text playerScoreText;

    public static GameObject[] intersections;
    public static Intersection[] intersectionObjects;

    // Use this for initialization
    void Start () {
        intersections = GameObject.FindGameObjectsWithTag("Intersection");
        intersectionObjects = new Intersection[intersections.Length];
        for(int i = 0; i < intersections.Length; i++)
        {
            intersectionObjects[i] = intersections[i].GetComponent<Intersection>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        playerScoreText.text = "Player Score : " + playerScore;
	}
}
