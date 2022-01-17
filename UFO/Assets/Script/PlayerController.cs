using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [Header("水平方向")]
    public float moveX;

    [Header("垂直方向")]
    public float moveY;

    [Header("推力")]
    public float push;

    Rigidbody2D rb2D;

    [Header("分數文字UI")]
    public Text countText;

    [Header("過關文字UI")]
    public Text winText;

    int score;

    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();

        winText.text = "";
        setScoreText();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        moveX = Input.GetAxis("Horizontal");

        print("Horizontal:" + moveX);

        moveY = Input.GetAxis("Vertical");

        print("Vertical:" + moveY);

        Vector2 movement = new Vector2(moveX, moveY);

        rb2D.AddForce(push * movement);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(name + "觸發了" + other.name);

        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            score += 1;
            setScoreText();
        }
    }

    void setScoreText()
    {
        countText.text = "目前分數:" + score.ToString();
        //計分文字UI.文字 = "目前分數:" + 分數.轉字串 ( );

        if (score >= 7) //如果分數大於12
        {
            winText.text = "你贏了";
            //過關文字UI.文字　= "你贏了";
        }
    }

}
