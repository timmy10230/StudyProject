using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTrancefrom;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - playerTrancefrom.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTrancefrom.position + offset;
    }
}
