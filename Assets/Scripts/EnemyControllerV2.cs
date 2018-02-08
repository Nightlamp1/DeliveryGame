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
        Transform targetTransform = targetPlayer.GetComponent<Transform>();
        Vector2 distanceToPlayer = transform.position - targetTransform.position;

        //Handling cases where player is further away in x direction than in y direction
        if(Mathf.Abs(distanceToPlayer.x) > Mathf.Abs(distanceToPlayer.y))
        {
            if (distanceToPlayer.x > 0 && canMoveLeft)
            {
                enemy.velocity = Vector2.right * -speed;
            }
            else if (distanceToPlayer.x <= 0 && canMoveRight)
            {
                enemy.velocity = Vector2.right * speed;
            }
            else if (canMoveDown || canMoveUp)
            {
                if (distanceToPlayer.y > 0 && canMoveDown)
                {
                    enemy.velocity = Vector2.up * -speed;
                }
                else if (canMoveUp)
                {
                    enemy.velocity = Vector2.up * speed;
                }
                else
                {
                    enemy.velocity = Vector2.up * -speed;
                }
            }
        }
        //Handling cases where player is further away in y direction than in x direction
        else if(Mathf.Abs(distanceToPlayer.x) <= Mathf.Abs(distanceToPlayer.y))
        {
            if (distanceToPlayer.y > 0 && canMoveDown)
            {
                enemy.velocity = Vector2.up * -speed;
            }
            else if (distanceToPlayer.y <= 0 && canMoveUp)
            {
                enemy.velocity = Vector2.up * speed;
            }
            else if (canMoveLeft || canMoveRight)
            {
                if (distanceToPlayer.x > 0 && canMoveLeft)
                {
                    enemy.velocity = Vector2.right * -speed;
                }
                else if (canMoveRight)
                {
                    enemy.velocity = Vector2.right * speed;
                }
                else
                {
                    enemy.velocity = Vector2.right * -speed;
                }
            }
        }
    }
}
