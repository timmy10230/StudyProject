using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour {

    public float speed = 90;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime));
    }
}
