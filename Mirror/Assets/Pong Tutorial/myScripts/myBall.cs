using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Bolt;
using Ludiq;

public class myBall : NetworkBehaviour
{
    public GameObject boltMachine;

    public override void OnStartServer()
    {
        base.OnStartServer();
        CustomEvent.Trigger(boltMachine, "setSimulated");
    }

    [ClientRpc]
    public void RpcWin()
    {

    }


}
