using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour {

    public Vector2[] validDirections;
    public Intersection[] neighbors;

	// Use this for initialization
	void Start () {

        validDirections = new Vector2[neighbors.Length];

		for(int i = 0; i < neighbors.Length; i++)
        {
            validDirections[i] = (neighbors[i].transform.position - transform.position).normalized;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
