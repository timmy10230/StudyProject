using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Text text_HP, text_Level, text_Time, text_Enemy;
    public GameObject gameoverPanel;
    public Animator levelFadeAnim;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {
        gameoverPanel.transform.Find("btn_Again").GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        gameoverPanel.transform.Find("btn_Main").GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Start");
        });
    }

    public void Refresh(int hp, int level, int time, int enemy)
    {
        text_HP.text = "HP: " + hp.ToString();
        text_Level.text = "Level: " + level.ToString();
        text_Time.text = "Time: " + time.ToString();
        text_Enemy.text = "Enemy: " + enemy.ToString();
    }

    public void ShowGameoverPanel()
    {
        gameoverPanel.SetActive(true);
    }

    public void PlayLevelFade(int levelIndex)
    {
        Time.timeScale = 0;
        levelFadeAnim.transform.Find("txt_Level").GetComponent<Text>().text = "Level " + levelIndex.ToString();
        levelFadeAnim.Play("LevelFade",0,0);
        startDealy = true;
    }

    private bool startDealy = false;
    private float timer = 0;
    private void Update()
    {
        if (startDealy)
        {
            timer += Time.unscaledDeltaTime;
            if(timer > 0.7f)
            {
                startDealy = false;
                Time.timeScale = 1;
                timer = 0;
            }
        }
    }
}
