using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager instance;
    public int money;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            name = "FirsGM";
            money = 0;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

    }
}
