using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int playerScore = 0;

    public static GameObject[] intersectionObjects;
    private Vector3[] intersections;

    // Use this for initialization
    void Start () {
        intersectionObjects = GameObject.FindGameObjectsWithTag("Intersection");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
