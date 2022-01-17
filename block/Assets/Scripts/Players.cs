using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour {

    [Header("水平移動速度")]
    public float speedX;
    Rigidbody2D playerRidgebody2D;

	void Start () {
        playerRidgebody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        moveLeftOrRight();
	}

    float LeftOrRight()
    {
        return Input.GetAxis("Horizontal");
    }

    void moveLeftOrRight()
    {
        playerRidgebody2D.velocity = LeftOrRight() * new Vector2(speedX, 0);
    }

}
