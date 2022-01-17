using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOrder : MonoBehaviour {

    private void Start()
    {
        StartCoroutine("F1");
    }

    IEnumerator F1()
    {
        Debug.Log("start");
        yield return new WaitForSeconds(2);
        Debug.Log("2s");
    }

    private void Update()
    {      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine("F1");
            Debug.Log("stop");
        }
    }
}
