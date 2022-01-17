using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManeger : MonoBehaviour {

	public void OnStartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
