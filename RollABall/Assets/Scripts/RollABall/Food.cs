using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float r = Random.Range(0, 60);
        transform.Rotate(new Vector3(r,-r,r) * Time.deltaTime*3);
    }

    

}
