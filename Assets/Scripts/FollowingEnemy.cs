using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : MonoBehaviour {

    public Intersection startingNode;
    public Intersection currentNode;
    public Intersection targetNode;

    private Vector2 directionToPlayer;
    public GameObject player;
    private Rigidbody2D enemy;
    public float speed = 9f;


    // Use this for initialization
    void Start () {
        enemy = GetComponent<Rigidbody2D>();

		for(int i = 0; i < GameController.intersectionObjects.Length; i++)
        {
            if(Vector2.Distance(transform.position, GameController.intersectionObjects[i].transform.position) < 0.5f)
            {
                startingNode = GameController.intersectionObjects[i];
                currentNode = startingNode;
                break;
            }
        }

        Vector2 randomDirection = GetRandomDirectionFromNode();
        targetNode = FindTargetNode(randomDirection);
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if(transform.position == targetNode.transform.position)
        {
            currentNode = targetNode;
            Vector2 idealMoveDirection = FindDirectionToPlayer();
            targetNode = FindTargetNode(idealMoveDirection);

        }

        MoveEnemy();
	}



    Intersection FindTargetNode(Vector2 direction)
    {
        for (int i = 0; i < currentNode.validDirections.Length; i++)
        {
            if (direction == currentNode.validDirections[i])
            {
                return currentNode.neighbors[i];
            }

        }

        Vector2 randomDirection = GetRandomDirectionFromNode();
        Intersection randomNode = FindTargetNode(randomDirection);
        return randomNode;
    }


    Vector2 FindDirectionToPlayer()
    {
        Vector2 distanceToPlayer = transform.position - player.GetComponent<Transform>().position;
        Debug.Log(distanceToPlayer);

        if(Mathf.Abs(distanceToPlayer.x) > Mathf.Abs(distanceToPlayer.y))
        {
            if(distanceToPlayer.x > 0)
            {
                return (Vector2.left);
            }
            else
            {
                return (Vector2.right);
            }
        }
        else
        {
            if(distanceToPlayer.y > 0)
            {
                return (Vector2.down);
            }
            else
            {
                return (Vector2.up);
            }
        }
    }

    Vector2 GetRandomDirectionFromNode()
    {
        return currentNode.validDirections[Random.Range(0, currentNode.validDirections.Length)];
    }

    void MoveEnemy()
    {
        enemy.MovePosition(Vector2.MoveTowards(transform.position, targetNode.transform.position, speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
            EnemySpawner.enemyCount -= 1;
        }
    }
}
