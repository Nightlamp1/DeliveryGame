using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerV2 : MonoBehaviour {


    public float speed = 4f;
    public Vector3 currentDirection;
    public bool atIntersection = false;

    public Transform Top, Left, Right, Bottom;
    public bool canMoveUp, canMoveDown, canMoveLeft, canMoveRight;
    Rigidbody2D enemy;
    private GameObject targetPlayer;

    // Use this for initialization
    void Start () {
        currentDirection = -transform.right;
        enemy = GetComponent<Rigidbody2D>();
        enemy.velocity = new Vector2(speed * -1, 0f);
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }
	

    private void FixedUpdate()
    {
        atIntersection = IsEnemyAtIntersection();
        CheckPossibleMovements();
        if (atIntersection)
        {
            EnemyMove();
        }

    }

    bool IsEnemyAtIntersection()
    {
        foreach(GameObject intersect in GameController.intersectionObjects)
        {
            if (Vector3.Distance(intersect.transform.position,transform.position) < 1.003)
                return true;
        }
        return false;
    }

    void CheckPossibleMovements()
    {
        RaycastHit2D upDirectionHit = Physics2D.Raycast(Top.position, Vector2.up, 0.7f);
        RaycastHit2D leftDirectionHit = Physics2D.Raycast(Left.position, -Vector2.right, 0.7f);
        RaycastHit2D downDirectionHit = Physics2D.Raycast(Bottom.position, -Vector2.up, 0.7f);
        RaycastHit2D rightDirectionHit = Physics2D.Raycast(Right.position, Vector2.right, 0.7f);

        Debug.DrawRay(Top.position, transform.TransformDirection(Vector2.up) * 0.7f, Color.green);
        Debug.DrawRay(Left.position, transform.TransformDirection(-Vector2.right) * 0.7f, Color.green);
        Debug.DrawRay(Bottom.position, transform.TransformDirection(-Vector2.up) * 0.7f, Color.green);
        Debug.DrawRay(Right.position, transform.TransformDirection(Vector2.right) * 0.7f, Color.green);

        if (upDirectionHit.collider != null)
            canMoveUp = false;
        else
            canMoveUp = true;

        if (leftDirectionHit.collider != null)
            canMoveLeft = false;
        else
            canMoveLeft = true;

        if (downDirectionHit.collider != null)
            canMoveDown = false;
        else
            canMoveDown = true;

        if (rightDirectionHit.collider != null)
            canMoveRight = false;
        else
            canMoveRight = true;
    }

    void EnemyMove()
    {
        Debug.Log("Gonna Move Soon");
        Transform targetTransform = targetPlayer.GetComponent<Transform>();
        Vector2 distanceToPlayer = transform.position - targetTransform.position;
        Debug.Log(distanceToPlayer);

        if(Mathf.Abs(distanceToPlayer.x) > Mathf.Abs(distanceToPlayer.y))
        {
            Debug.Log("Would like to move left/right");
            if(distanceToPlayer.x > 0 && canMoveLeft)
            {
                Debug.Log("MOVING LEFT");
                enemy.velocity = Vector2.right * -speed;
            }
            else if(distanceToPlayer.x <= 0 && canMoveRight)
            {
                Debug.Log("MOVING RIGHT");
                enemy.velocity = Vector2.right * speed;
            }
            else if (canMoveDown || canMoveUp)
            {
                if (distanceToPlayer.y > 0 && canMoveDown)
                {
                    Debug.Log("MOVING Down but dont want to");
                    enemy.velocity = Vector2.up * -speed;
                }
                else if(canMoveUp)
                {
                    Debug.Log("MOVING Up but dont want to");
                    enemy.velocity = Vector2.up * speed;
                }
                else
                {
                    Debug.Log("MOVING Down but dont want to");
                    enemy.velocity = Vector2.up * -speed;
                }

            }

        }
        else if(Mathf.Abs(distanceToPlayer.x) <= Mathf.Abs(distanceToPlayer.y))
        {
            Debug.Log("Would like to move up/down");
            if (distanceToPlayer.y > 0 && canMoveDown)
            {
                Debug.Log("MOVING Down");
                enemy.velocity = Vector2.up * -speed;
            }
            else if (distanceToPlayer.y <= 0 && canMoveUp)
            {
                Debug.Log("MOVING Up");
                enemy.velocity = Vector2.up * speed;
            }
            else if (canMoveLeft || canMoveRight)
            {
                if (distanceToPlayer.x > 0 && canMoveLeft)
                {
                    Debug.Log("MOVING LEFT but dont want to");
                    enemy.velocity = Vector2.right * -speed;
                }
                else if(canMoveRight)
                {
                    Debug.Log("MOVING RIGHT but dont want to");
                    enemy.velocity = Vector2.right * speed;
                }
                else
                {
                    Debug.Log("MOVING LEFT but dont want to");
                    enemy.velocity = Vector2.right * -speed;
                }
            }
        }
    }
}
