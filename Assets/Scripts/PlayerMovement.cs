using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 0.5f;
    public Vector3 currentDirection;

    public Sprite testsprite;
    public GameObject package;
    public bool hasPackage = false;

    private GameObject gameController;
    private Vector2 touchStart;

	// Use this for initialization
	void Start () {
        currentDirection = transform.up;
        gameController = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += currentDirection * speed * Time.deltaTime;

        //PC inputs
//#if UNITY_STANDALONE || UNITY_WEBPLAYER

        if (Input.GetKeyDown(KeyCode.W))
        {
            //Debug.Log("W is pressed go forward");
            currentDirection = transform.up;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
           //Debug.Log("S is pressed go down");
            currentDirection = -transform.up;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //Debug.Log("D is pressed go right");
            currentDirection = transform.right;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
           //Debug.Log("A is pressed go left");
            currentDirection = -transform.right;
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
            gameController.GetComponent<GameController>().playerScore += 10;
            Debug.Log(gameController.GetComponent<GameController>().playerScore);
            hasPackage = false;
            Debug.Log(hasPackage);
        }
        else if(collision.tag == "Mailbox")
        {
            Debug.Log("You don't have the package!!!!!");
        }
    }
}
