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

    public bool canMoveUp, canMoveDown, canMoveLeft, canMoveRight = false;
    public bool moving = false;
    public Vector2 currentDirection;
    public Transform leftTop, leftBot, rightTop, rightBot;
    public Transform upLeft, upRight, downLeft, downRight;

    private Vector2 touchStartPosition;

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
        CheckForUserInput();
        MovePlayer();
        UpdateCurrentNode();

        if((Vector2)transform.position == (Vector2)targetNode.transform.position)
        {
            targetNode = FindTargetNode(currentDirection);
            //currentNode = targetNode;
        }

        CheckForValidMoveDirections();
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

    void MovePlayer()
    {
        player.MovePosition(Vector2.MoveTowards(transform.position, targetNode.transform.position, speed * Time.deltaTime));
    }

    void UpdateCurrentNode()
    {
        if(Vector2.Distance(transform.position,currentNode.transform.position) > 0.4f)
        {
            currentNode = targetNode;
        }
    }

    void CheckForValidMoveDirections()
    {
        RaycastHit2D leftTopHit = Physics2D.Raycast(leftTop.position, Vector2.right, 1f);
        RaycastHit2D leftBotHit = Physics2D.Raycast(leftBot.position, Vector2.right, 1f);
        RaycastHit2D rightTopHit = Physics2D.Raycast(rightTop.position, -Vector2.right, 1f);
        RaycastHit2D rightBotHit = Physics2D.Raycast(rightBot.position, -Vector2.right, 1f);

        RaycastHit2D topLeftHit = Physics2D.Raycast(upLeft.position, -Vector2.up, 1f);
        RaycastHit2D topRightHit = Physics2D.Raycast(upRight.position, -Vector2.up, 1f);
        RaycastHit2D botLeftHit = Physics2D.Raycast(downLeft.position, Vector2.up, 1f);
        RaycastHit2D botRightHit = Physics2D.Raycast(downRight.position, Vector2.up, 1f);


        if(leftTopHit.collider.tag == "Player" && leftBotHit.collider.tag == "Player")
        {
            canMoveLeft = true;
        }
        else if(leftTopHit.collider.tag == "Environment" || leftBotHit.collider.tag == "Environment")
        {
            canMoveLeft = false;
        }

        if (rightTopHit.collider.tag == "Player" && rightBotHit.collider.tag == "Player")
        {
            canMoveRight = true;
        }
        else if (rightTopHit.collider.tag == "Environment" || rightBotHit.collider.tag == "Environment")
        {
            canMoveRight = false;
        }

        if (topLeftHit.collider.tag == "Player" && topRightHit.collider.tag == "Player")
        {
            canMoveUp = true;
        }
        else if (topLeftHit.collider.tag == "Environment" || topRightHit.collider.tag == "Environment")
        {
            canMoveUp = false;
        }

        if (botLeftHit.collider.tag == "Player" && botRightHit.collider.tag == "Player")
        {
            canMoveDown = true;
        }
        else if (botLeftHit.collider.tag == "Environment" || botRightHit.collider.tag == "Environment")
        {
            canMoveDown = false;
        }
    }

    void CheckForUserInput()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (canMoveDown)
            {
                targetNode = FindTargetNode(-Vector2.up);
                //currentNode = targetNode;
            }
            currentDirection = -Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (canMoveRight)
            {
                targetNode = FindTargetNode(Vector2.right);
                //currentNode = targetNode;
            }
            currentDirection = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (canMoveUp)
            {
                targetNode = FindTargetNode(Vector2.up);
                //currentNode = targetNode;
            }
            currentDirection = Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (canMoveLeft)
            {
                targetNode = FindTargetNode(-Vector2.right);
                //currentNode = targetNode;
            }
            currentDirection = -Vector2.right;
        }
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                touchStartPosition = myTouch.position;
            }
            else if(myTouch.phase == TouchPhase.Ended && touchStartPosition.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;

                float x = touchEnd.x - touchStartPosition.x;

                float y = touchEnd.y - touchStartPosition.y;

                touchStartPosition.x = -1;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0)
                    {
                        if (canMoveRight)
                        {
                            targetNode = FindTargetNode(Vector2.right);
                            //currentNode = targetNode;
                        }
                        currentDirection = Vector2.right;
                    }
                    else
                    {
                        if (canMoveLeft)
                        {
                            targetNode = FindTargetNode(-Vector2.right);
                            //currentNode = targetNode;
                        }
                        currentDirection = -Vector2.right;
                    }
                }
                else
                {
                    if (y > 0)
                    {
                        if (canMoveUp)
                        {
                            targetNode = FindTargetNode(Vector2.up);
                            //currentNode = targetNode;
                        }
                        currentDirection = Vector2.up;
                    }
                    else
                    {
                        if (canMoveDown)
                        {
                            targetNode = FindTargetNode(-Vector2.up);
                            //currentNode = targetNode;
                        }
                        currentDirection = -Vector2.up;
                    }
                }
            }
        }
#endif
    }
}
