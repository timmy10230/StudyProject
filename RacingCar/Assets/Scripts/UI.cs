using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    static public UI _instance;
    public GameObject reciprocal;
    public GameObject EndPanel;
    public GameObject GameInfo;
    public Text Lap_N;
    public Text Lap_T;
    public Text endTime;
    public Text totleLap;
    public Text carSpeed;
    public Text nTime;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        totleLap.text = "圈數 " + Lap._instance.LapCount.ToString();
        Lap_T.text = Lap._instance.LapCount.ToString();
    }

    public void ShowEnd()
    {
        endTime.text = "時間 " + nTime.text;
        EndPanel.SetActive(true);
    }

    public void StartReciprocal()
    {
        reciprocal.SetActive(true);
    }

}
