using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {

        transform.DORotate(new Vector3(90, 90, 90), 5);
	}
	
}
