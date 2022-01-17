using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
    public float tilt;
    public Boundary boundary;
    private Rigidbody rb;
    private AudioSource ads;

    public GameObject shot;
    public Transform[] shotSpawns;
    public float fireRate;
    private float nextFire;

    void Start()
	{
        rb = GetComponent<Rigidbody>();
        ads = GetComponent<AudioSource>();
	}

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            foreach (var shotSpawn in shotSpawns)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            
            ads.Play();
        }
    }

    void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);

    }
}
