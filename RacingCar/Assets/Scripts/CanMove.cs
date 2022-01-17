using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMove : MonoBehaviour
{
    public void NCanMove()
    {
        Lap._instance.canMove = true;
        Lap._instance.TimmerC = true;
        UI._instance.GameInfo.SetActive(true);
    }

    public void Hide()
    {
        UI._instance.reciprocal.SetActive(false);
    }
}
