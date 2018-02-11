using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static GameObject[] enemySpawnLocations;
    public GameObject[] enemies;
    public static int enemyCount;
    public int maxEnemyCount = 2;

	// Use this for initialization
	void Start () {
        enemySpawnLocations = GameObject.FindGameObjectsWithTag("EnemySpawn");
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        InvokeRepeating("SpawnEnemy", 5.0f, 6.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnEnemy()
    {
        if(enemyCount < maxEnemyCount)
        {
            GameObject spawningEnemy = enemies[Random.Range(0, enemies.Length)];
            Vector3 spawningLocation = enemySpawnLocations[Random.Range(0, enemySpawnLocations.Length)].transform.position;

            //Check if object is already present at the randomly selected spawn location, if so return without spawning
            if (Physics2D.OverlapCircle((Vector2)spawningLocation, 0.5f) != null)
            {
                return;
            }

            Instantiate(spawningEnemy, spawningLocation, Quaternion.identity);
            enemyCount += 1;

        }
    }
}
