using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour {

    public float coldTime = 2;
    private Image filledImage;
    private float timer = 0;
    private bool isStartTimer;
    public KeyCode keyCode;

	// Use this for initialization
	void Start () {
        filledImage = transform.Find("FS1").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keyCode))
        {
            isStartTimer = true;
        }

        if (isStartTimer)
        {
            timer += Time.deltaTime;
            filledImage.fillAmount = (coldTime - timer) / coldTime;
        }
        if (timer >= coldTime)
        {
            filledImage.fillAmount = 0;
            timer = 0;
            isStartTimer = false;
        }

	}

    public void OnClick()
    {
        isStartTimer = true;
    }

}
