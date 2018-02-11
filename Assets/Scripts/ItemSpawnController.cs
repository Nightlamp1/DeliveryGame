using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnController : MonoBehaviour {

    public static GameObject[] itemSpawnLocations;
    public static int envelopeCount;
    public GameObject[] itemsToSpawn;
    public static int envelopeMaxLimit = 5;

	// Use this for initialization
	void Start () {
        itemSpawnLocations = GameObject.FindGameObjectsWithTag("ItemSpawner");
        envelopeCount = GameObject.FindGameObjectsWithTag("Envelope").Length;
        InvokeRepeating("SpawnItem", 2.0f, 4.0f);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void SpawnItem()
    {
        if(envelopeCount < envelopeMaxLimit)
        {
            GameObject spawningObject = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];
            Vector3 spawningLocation = itemSpawnLocations[Random.Range(0, itemSpawnLocations.Length)].transform.position;

            if(Physics2D.OverlapCircle((Vector2)spawningLocation, 0.5f) != null)
            {
                Debug.Log("Something is already at this location, can not spawn");
                return;
            }

            Debug.Log("I want to spawn an item at " + spawningLocation);
            Instantiate(spawningObject, spawningLocation, Quaternion.identity);
            envelopeCount += 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(-10,18,-1), 0.5f);
    }
}
