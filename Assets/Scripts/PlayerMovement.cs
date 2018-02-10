using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 4f;
    public Vector3 currentDirection; //will be retired
    public Vector2 nextMovement;
    public bool playerAtIntersection = false;
    Rigidbody2D player;

    public Sprite testsprite;
    public GameObject package;
    public bool hasPackage = false;

    private GameObject gameController;
    private Vector2 touchStart;

    public Transform Top, Left, Right, Bottom;
    public bool canMoveUp, canMoveDown, canMoveLeft, canMoveRight;

    // Use this for initialization
    void Start () {
        player = GetComponent<Rigidbody2D>();
        player.velocity = new Vector2(0f, 1 * speed);
        gameController = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Debug.Log(nextMovement);
        CheckPossibleMovements();
        playerAtIntersection = IsPlayerAtIntersection();

        if (playerAtIntersection)
        {
            //player.velocity = nextMovement;
        }

        //PC inputs
//#if UNITY_STANDALONE || UNITY_WEBPLAYER

        if (Input.GetKeyDown(KeyCode.W))
        {
            player.velocity = Vector2.up * speed;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player.velocity = Vector2.up * -speed;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            player.velocity = Vector2.right * speed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            player.velocity = Vector2.right * -speed;  
        }

        //Inputs for mobile touch devices
//#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (Input.touchCount > 0)
        {
            Debug.Log("Touch is detected");
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                touchStart = myTouch.position;
            }
            else if(myTouch.phase == TouchPhase.Ended && touchStart.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;

                float x = touchEnd.x - touchStart.x;

                float y = touchEnd.y - touchStart.y;

                touchStart.x = -1;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0)
                        currentDirection = transform.right;
                    else
                        currentDirection = -transform.right;
                }
                else
                {
                    if (y > 0)
                        currentDirection = transform.up;
                    else
                        currentDirection = -transform.up;
                }
            }
        }

//#endif



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Package")
        {
            Destroy(collision.gameObject);
            hasPackage = true;
            Debug.Log(hasPackage);
            Debug.Log("You have run into a package");
        }

        if(collision.tag == "Mailbox" && hasPackage)
        {
            Debug.Log("You have successfully delivered a package!!");
            Debug.Log("Your new score is:");
            //gameController.GetComponent<GameController>().playerScore += 10;
            //Debug.Log(gameController.GetComponent<GameController>().playerScore);
            hasPackage = false;
            Debug.Log(hasPackage);
        }
        else if(collision.tag == "Mailbox")
        {
            Debug.Log("You don't have the package!!!!!");
        }
    }

    bool IsPlayerAtIntersection()
    {
        foreach (GameObject intersect in GameController.intersectionObjects)
        {
            Debug.Log(Vector3.Distance(intersect.transform.position, transform.position));
            if (Vector3.Distance(intersect.transform.position, transform.position) < 1.03)
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
}
