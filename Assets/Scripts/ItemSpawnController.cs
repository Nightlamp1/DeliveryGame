using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnController : MonoBehaviour {

    public static GameObject[] itemSpawnLocations;
    public static int envelopeCount;
    public GameObject[] itemsToSpawn;
    public static int envelopeMaxLimit = 4;

	// Use this for initialization
	void Start () {
        itemSpawnLocations = GameObject.FindGameObjectsWithTag("ItemSpawner");
        envelopeCount = GameObject.FindGameObjectsWithTag("Envelope").Length;
        InvokeRepeating("SpawnItem", 2.0f, 4.0f);
	}

    void SpawnItem()
    {
        //Verify that we have not exceeded the max number of objects for the current level
        if(envelopeCount < envelopeMaxLimit)
        {
            GameObject spawningObject = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];
            Vector3 spawningLocation = itemSpawnLocations[Random.Range(0, itemSpawnLocations.Length)].transform.position;
            //Check if object is already present at the randomly selected spawn location, if so return without spawning
            if(Physics2D.OverlapCircle((Vector2)spawningLocation, 0.5f) != null)
            {
                return;
            }
            //Spawning an item
            Instantiate(spawningObject, spawningLocation, Quaternion.identity);
            envelopeCount += 1;
        }
    }
}
