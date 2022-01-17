using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap : MonoBehaviour
{
    public static Lap _instance;
    public bool F = false;
    public bool B = false;
    public bool correct;
    public bool c = false;
    public int LapCount;
    public int start;
    public GameObject[] checkPoint;
    public int nowLap = 1;
    public int checkPointID;
    public bool TimmerC = false;
    public float NTime = 0;
    private int min;
    private int sec;
    private float mSec;
    public bool gameOver = false;
    public bool canMove = false;

    private void Awake()
    {
        LapCount = LapData.Lap;
        _instance = this;
    }

    private void Start()
    {
        checkPointID = 0;
        foreach (GameObject checkPoint in checkPoint)
        {
            checkPoint.SetActive(false);
        }
        StartCoroutine("StartReciprocal");
    }

    IEnumerator StartReciprocal()
    {
        yield return new WaitForSeconds(3);
        UI._instance.StartReciprocal();
    }

    private void Update()
    {
        if (c)
        {
            start++;
            checkPoint[0].SetActive(true);
            TimmerC = true;
            c = false;
        }
        if (TimmerC)
        {
            Timmer();
        }
        if (gameOver)
        {
            GameOver();
        }
        UI._instance.Lap_N.text = nowLap.ToString();
        UI._instance.carSpeed.text = CarController._instance.rb.velocity.magnitude.ToString("0.00");    
    }

    public void passCheckPoint()
    {
        checkPointID++;
        if(checkPointID == checkPoint.Length)
        {
            checkPointID = 0;
            if (nowLap < LapCount)
                nowLap++;
            else
            {
                gameOver = true;
                UI._instance.ShowEnd();
            }
        }
        checkPoint[checkPointID].SetActive(true);
    }

    public void Timmer()
    {
        NTime += Time.deltaTime;
        mSec = (int)((NTime - (int)NTime) * 10);
        sec = (int)NTime % 60;
        min = (int)NTime / 60;
        UI._instance.nTime.text = min + ":" + sec + ":" + mSec;
    }

    public void GameOver()
    {
        canMove = false;
        TimmerC = false;
    }
}
