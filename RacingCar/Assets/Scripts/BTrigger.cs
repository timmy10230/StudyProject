using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Lap._instance.B = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && Lap._instance.F == false)
        {
            Lap._instance.B = true;
        }
        else if(other.CompareTag("Player") && Lap._instance.F == true)
        {
            Lap._instance.B = false;
        }
    }
}
