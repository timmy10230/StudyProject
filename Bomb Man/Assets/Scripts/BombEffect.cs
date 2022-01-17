using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    private void AniFin()
    {
        //Destroy(gameObject);
    }

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime >= 1 && info.IsName("BombEffect"))
        {
            ObjectPool.Instance.Add(ObjectType.BombEffect, gameObject);
        }
    }
}
