using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Rigidbody rb;

    public int score = 0;

    public Text scoreText;

    public GameObject winText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(h, 0, v)*7);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            Destroy(other.gameObject);

            score++;

            scoreText.text = "分數:" + score;

            if (score == 10)
            {
                winText.SetActive(true);
            }
        }
    }

    

}
