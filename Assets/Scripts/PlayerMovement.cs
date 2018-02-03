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

	// Use this for initialization
	void Start () {
        currentDirection = transform.up;
        gameController = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += currentDirection * speed * Time.deltaTime;
		
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
