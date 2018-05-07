using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTest : MonoBehaviour
{

    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;
    Vector2 currentDirection = Vector2.right;
    Vector2 nextDirectionWhenAvailable = Vector2.zero;
    public Sprite playerUp, playerDown, playerLeft, playerRight;

    private SpriteRenderer playerSprite;
    private Sprite nextSprite;

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
        Vector2 leftCheck = new Vector2 (-0.5f, 0);
        Vector2 rightCheck = new Vector2 (0.5f, 0);
        Vector2 downCheck = new Vector2(0, -0.5f);
        Vector2 upCheck = new Vector2(0, 0.5f);

        RaycastHit2D hitLeft = Physics2D.Linecast((pos + leftCheck) + dir, pos);
        RaycastHit2D hitRight = Physics2D.Linecast((pos + rightCheck) + dir, pos);
        RaycastHit2D hitDown = Physics2D.Linecast((pos + downCheck) + dir, pos);
        RaycastHit2D hitUp = Physics2D.Linecast((pos + upCheck) + dir, pos);

        if(dir.y == 0)
        {
            if (hitUp.collider.tag == "Environment" || hitDown.collider.tag == "Environment")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (hitLeft.collider.tag == "Environment" || hitRight.collider.tag == "Environment")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    void Update()
    {
        checkForUserInput();
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
    }
}
