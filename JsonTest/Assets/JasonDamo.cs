using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JasonDamo : MonoBehaviour {

    string path;
    string jsonString;


	void Start () {
        path = Application.streamingAssetsPath + "/Creture.json";
        jsonString = File.ReadAllText(path);
        Creature Yomo = JsonUtility.FromJson<Creature>(jsonString);
        Debug.Log(Yomo.Name+Yomo.Level);
        string oldYomo = JsonUtility.ToJson(Yomo);
        Debug.Log("OLD: " + oldYomo);
        Yomo.Name = "Yomo";
        Yomo.Level += 1;
        Yomo.State = new int[] { 4, Yomo.Level };
        string newYomo = JsonUtility.ToJson(Yomo);
        Debug.Log("NEW: "+newYomo);
	}
}

[System.Serializable]
public class Creature
{
    public string Name;
    public int Level;
    public int[] State;
}
