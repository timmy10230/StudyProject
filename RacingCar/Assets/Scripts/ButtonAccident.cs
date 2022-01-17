using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonAccident : MonoBehaviour
{
    public Text lap;
    private string l;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GetLap()
    {
        try
        {
            LapData.Lap = int.Parse(lap.text.ToString());
        }
        catch (System.Exception)
        {
            throw;
        }
            
    }
}
