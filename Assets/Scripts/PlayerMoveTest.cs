﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveTest : MonoBehaviour
{

    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;
    public static Vector2 currentDirection = Vector2.right;
    Vector2 nextDirectionWhenAvailable = Vector2.zero;
    public Sprite playerUp, playerDown, playerLeft, playerRight;
    public GameObject bulletObject;

    private SpriteRenderer playerSprite;
    private Sprite nextSprite;
    private Vector2 touchStartPosition;
    private bool uiTouchDetected = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
        Debug.Log(dest);
        Debug.Log(currentDirection);
    }

    void Start()
    {
        dest = transform.position;
        playerSprite = GetComponent<SpriteRenderer>();
        playerSprite.sprite = playerRight;
        nextSprite = playerRight;
    }

    bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        Debug.Log(pos);
        Vector2 leftCheck = new Vector2(-1.5f, pos.y);
        Vector2 rightCheck = new Vector2(1.5f, pos.y);
        Vector2 downCheck = new Vector2(pos.x, -1.5f);
        Vector2 upCheck = new Vector2(pos.x, 1.5f);

        Vector2 finalVector1 = new Vector2(0, 0);
        Vector2 finalVector2 = new Vector2(0, 0);
        Vector2 finalVector3 = new Vector2(0, 0);

        if (dir == Vector2.left)
        {
            finalVector1 = new Vector2(-1.5f, 0.0f);
            finalVector2 = new Vector2(-1.5f, 0.9f);
            finalVector3 = new Vector2(-1.5f, -0.9f);
        }
        else if(dir == Vector2.right)
        {
            finalVector1 = new Vector2(1.5f, 0.0f);
            finalVector2 = new Vector2(1.5f, 0.9f);
            finalVector3 = new Vector2(1.5f, -0.9f);
        }
        else if(dir == Vector2.up)
        {
            finalVector1 = new Vector2(0.0f, 1.5f);
            finalVector2 = new Vector2(0.9f, 1.5f);
            finalVector3 = new Vector2(-0.9f, 1.5f);
        }
        else if(dir == Vector2.down)
        {
            finalVector1 = new Vector2(0.0f, -1.5f);
            finalVector2 = new Vector2(0.9f, -1.5f);
            finalVector3 = new Vector2(-0.9f, -1.5f);
        }

        RaycastHit2D hitLeft = Physics2D.Linecast((finalVector1 + pos), pos);
        RaycastHit2D hitRight = Physics2D.Linecast((finalVector2 + pos), pos);
        RaycastHit2D hitDown = Physics2D.Linecast((finalVector3 + pos), pos);

        Debug.DrawLine((finalVector1 + pos), pos, Color.red);
        Debug.DrawLine((finalVector2 + pos), pos, Color.red);
        Debug.DrawLine((finalVector3 + pos), pos, Color.red);

        if (hitDown.collider.tag == "Environment" || hitRight.collider.tag == "Environment" || hitLeft.collider.tag == "Environment")
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    void Update()
    {
        checkForUserInput();
        checkForShooting();
    }

    void FixedUpdate()
    {
        if ((Vector2)transform.position == dest)
        {
            if (valid(nextDirectionWhenAvailable))
            {
                dest = (Vector2)transform.position + nextDirectionWhenAvailable;
                currentDirection = nextDirectionWhenAvailable;
                playerSprite.sprite = nextSprite;
            }
            else if (valid(currentDirection))
            {
                dest = (Vector2)transform.position + currentDirection;
            }

        }

        MovePlayer(dest);

    }

    void MovePlayer(Vector2 destination)
    {
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
    }

    void checkForUserInput()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.W))
        {
            nextDirectionWhenAvailable = Vector2.up;
            nextSprite = playerUp;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            nextDirectionWhenAvailable = Vector2.right;
            nextSprite = playerRight;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            nextDirectionWhenAvailable = -Vector2.up;
            nextSprite = playerDown;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            nextDirectionWhenAvailable = -Vector2.right;
            nextSprite = playerLeft;
        }

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];
            if (myTouch.phase == TouchPhase.Began)
            {
                touchStartPosition = myTouch.position;
                if (EventSystem.current.IsPointerOverGameObject(myTouch.fingerId))
                {
                    uiTouchDetected = true;
                }
                else
                {
                    uiTouchDetected = false;
                }
            }
            else if (myTouch.phase == TouchPhase.Ended && touchStartPosition.x >= 0)
            {
                if (uiTouchDetected)
                {
                    GameController.playerScore += 333;
                }
                else if (!uiTouchDetected)
                {
                    Vector2 touchEnd = myTouch.position;
                    float x = touchEnd.x - touchStartPosition.x;
                    float y = touchEnd.y - touchStartPosition.y;
                    touchStartPosition.x = -1;

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        if (x > 0)
                        {
                            nextDirectionWhenAvailable = Vector2.right;
                            nextSprite = playerRight;
                        }
                        else
                        {
                            nextDirectionWhenAvailable = -Vector2.right;
                            nextSprite = playerLeft;
                        }
                    }
                    else
                    {
                        if (y > 0)
                        {
                            nextDirectionWhenAvailable = Vector2.up;
                            nextSprite = playerUp;
                        }
                        else
                        {
                            nextDirectionWhenAvailable = -Vector2.up;
                            nextSprite = playerDown;
                        }
                    }

                    GameController.playerScore += 1000;
                }

            }
        }
#endif
    }

    void checkForShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 fireLocation = (Vector3)transform.position + (Vector3)currentDirection * 2;
            GameObject firedBullet = Instantiate(bulletObject, fireLocation, Quaternion.identity);
        }
    }

    public void shootButtonPressed()
    {
        Debug.Log("BUTTON PRESSED");
        Vector3 fireLocation = (Vector3)transform.position + (Vector3)currentDirection * 2;
        GameObject firedBullet = Instantiate(bulletObject, fireLocation, Quaternion.identity);
    }
}