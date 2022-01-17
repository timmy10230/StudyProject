using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Lap._instance.B == false)
        {
            Lap._instance.c = true;
            Lap._instance.F = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Lap._instance.F = false;
        }
    }
}
