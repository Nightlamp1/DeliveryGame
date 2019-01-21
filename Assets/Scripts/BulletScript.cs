using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public Vector2 fireDirection;
    public Rigidbody2D bulletRb;
    public float speed = 15.0f;
    //test
	// Use this for initialization
	void Start () {
        fireDirection = PlayerMoveTest.currentDirection;
        bulletRb = GetComponent<Rigidbody2D>();
        bulletRb.velocity = fireDirection * speed;


        Debug.Log(fireDirection);

        if(fireDirection == new Vector2(0, -1))
        {
            transform.Rotate(new Vector3(0f, 0f, 270f));
        }
        else if(fireDirection == new Vector2(0, 1))
        {
            transform.Rotate(new Vector3(0f, 0f, 90f));
        }
        else if(fireDirection == new Vector2(-1, 0))
        {
            transform.Rotate(new Vector3(0f, 0f, 180f));
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Environment")
        {
            Destroy(this.gameObject);
        }

        if(collision.collider.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Debug.Log("Enemy Destroyed + 10 points");
        }
        
    }
}
