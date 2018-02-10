using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{

    public Intersection startingNode;
    public Intersection currentNode;
    public Intersection targetNode;

    public Rigidbody2D player;
    public float speed = 12f;

    public bool upCheck, downCheck, leftCheck, rightCheck = false;
    public bool moving = false;
    public Vector2 currentDirection;

    // Use this for initialization
    void Start()
    {
        currentNode = startingNode;
        targetNode = startingNode;
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            targetNode = FindTargetNode(-Vector2.up);
            currentNode = targetNode;
            currentDirection = -Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            targetNode = FindTargetNode(Vector2.right);
            currentNode = targetNode;
            currentDirection = Vector2.right;
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            targetNode = FindTargetNode(Vector2.up);
            currentNode = targetNode;
            currentDirection = Vector2.up;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            targetNode = FindTargetNode(-Vector2.right);
            currentNode = targetNode;
            currentDirection = -Vector2.right;
        }

        player.MovePosition(Vector2.MoveTowards(transform.position, targetNode.transform.position, speed*Time.deltaTime));

        if((Vector2)transform.position == (Vector2)targetNode.transform.position)
        {
            Debug.Log("you have reached the destination");
            Debug.Log("Current Direction: " + currentDirection);
            targetNode = FindTargetNode(currentDirection);
            currentNode = targetNode;
        }
    }


    Intersection FindTargetNode(Vector2 direction)
    {
        for(int i = 0; i < currentNode.validDirections.Length; i++)
        {
            if(direction == currentNode.validDirections[i])
            {
                return currentNode.neighbors[i];
            }
            
        }
        return currentNode;
    }
}
