using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject playerPre;
    public int enemyCount;
    private int levelCount = 0;
    public int time = 180;
    private float timer = 0f;

    private MapController mapController;
    private GameObject player;
    private PlayerCtrl playerCrtl;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mapController = GetComponent<MapController>();
        LevelCtrl();
    }

    private void LevelTimer()
    {
        if (time <= 0)
        {
            if (playerCrtl.HP > 0)
            {
                playerCrtl.HP--;
                time = 200;
                return;
            }
            playerCrtl.PlayDieAni();
            return;
        }
        timer += Time.deltaTime;
        if(timer >= 1.0f)
        {
            time--;
            timer = 0;
        }
    }

    public void Gameover()
    {
        UIController.Instance.ShowGameoverPanel();
    }

    private void Update()
    {
        LevelTimer();
        UIController.Instance.Refresh(playerCrtl.HP, levelCount, time, enemyCount);
        if (Input.GetKeyDown(KeyCode.N))
        {
            LevelCtrl();
        }
    }

    public void LoadNextLevel()
    {
        if (enemyCount <= 0)
            LevelCtrl();
    }

    private void LevelCtrl()
    {
        time = levelCount * 50 + 130;

        int x = 6 + 2 * (levelCount / 3);
        int y = 4 + 2 * (levelCount / 3);
        if (x > 18) x = 18;
        if (y > 15) y = 15;

        enemyCount = (int)(levelCount * 1.5f) + 1;
        if (enemyCount > 40) enemyCount = 40;
        mapController.InitMap(x, y, x * y, enemyCount);
        if(player == null)
        {
            player = Instantiate(playerPre);
            playerCrtl = player.GetComponent<PlayerCtrl>();
            playerCrtl.Init(1, 3, 2);
        }
        playerCrtl.ResetPlayer();
        player.transform.position = mapController.GetPlayerPos();

        GameObject[] effect =  GameObject.FindGameObjectsWithTag(Tags.BombEffect);
        foreach (var item in effect)
        {
            ObjectPool.Instance.Add(ObjectType.BombEffect, item);
        }

        Camera.main.GetComponent<CameraFollow>().Init(player.transform,x,y);

        levelCount++;
        UIController.Instance.PlayLevelFade(levelCount);
    }

    public bool IsSuperWall(Vector2 pos)
    {
        return mapController.IsSuperWall(pos);
    }
}
