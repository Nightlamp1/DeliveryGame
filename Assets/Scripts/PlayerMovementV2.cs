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
            Debug.Log("moving down");
            targetNode = FindTargetNode(-Vector2.up);
            currentNode = targetNode;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            targetNode = FindTargetNode(Vector2.right);
            currentNode = targetNode;
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            targetNode = FindTargetNode(Vector2.up);
            currentNode = targetNode;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            targetNode = FindTargetNode(-Vector2.right);
            currentNode = targetNode;
        }

        player.MovePosition(Vector2.MoveTowards(transform.position, targetNode.transform.position, speed*Time.deltaTime));
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
