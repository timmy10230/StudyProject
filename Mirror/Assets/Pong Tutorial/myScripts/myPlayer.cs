using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Bolt;
using Ludiq;

public class myPlayer : NetworkBehaviour
{
    public GameObject boltMachine;

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            CustomEvent.Trigger(boltMachine, "Move");
        }
    }
}
