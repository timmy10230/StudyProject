using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelScroll : MonoBehaviour , IBeginDragHandler , IEndDragHandler{

    public float smooting = 5;
    public Toggle []toggleArray;
    private bool isDraging = false;
    private ScrollRect scrollRect;
    private float[] pageArray = new float[] {0f,0.33f,0.66f,0.985f };
    private float targethorizontalPosition=0;

    // Use this for initialization
    void Start () {
        scrollRect = GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isDraging==false)
        {
        scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targethorizontalPosition, Time.deltaTime*smooting);
        }
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
        int index = 0;
        float posX = scrollRect.horizontalNormalizedPosition;
        float offset = Mathf.Abs(pageArray[index] - posX);
        for(int i = 0; i < pageArray.Length; i++)
        {
            float offsetTemp= Mathf.Abs(pageArray[i] - posX);
            if (offsetTemp < offset)
            {
                index = i;
                offset = offsetTemp;
            }
        }
        targethorizontalPosition = pageArray[index];
        toggleArray[index].isOn = true;
        //scrollRect.horizontalNormalizedPosition = pageArray[index];
    }

    public void TurnToPege1(bool isOn)
    {
        targethorizontalPosition = pageArray[0];
    }

    public void TurnToPege2(bool isOn)
    {
        targethorizontalPosition = pageArray[1];
    }

    public void TurnToPege3(bool isOn)
    {
        targethorizontalPosition = pageArray[2];
    }

    public void TurnToPege4(bool isOn)
    {
        targethorizontalPosition = pageArray[3];
    }

}
