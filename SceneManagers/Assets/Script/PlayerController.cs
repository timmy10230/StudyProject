using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D playerRB;

    public float speed;

	void Start () {
        playerRB = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		

	}

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRB.AddForce(Vector2.left*speed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRB.AddForce(Vector2.right * speed);
        }
    }

}
