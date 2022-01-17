using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public int HP = 0;
    public int range = 0;
    public int bombCount = 1;

    private float speed = 0.1f;
    private Animator anim;
    private Rigidbody2D rig;
    private SpriteRenderer spriteRenderer;
    private Color color;

    private bool isInjured = false;
    private float bombBoomTime = 0;

    private List<GameObject> bombList = new List<GameObject>();

    public void ResetPlayer()
    {
        foreach (var item in bombList)
        {
            bombCount++;
            item.GetComponent<Bomb>().StopAllCoroutines();
            ObjectPool.Instance.Add(ObjectType.Bomb, item);
        }
        bombList.Clear();

        StopCoroutine("Injured");
        color.a = 1;
        spriteRenderer.color = color;
    }

    public void AddSpeed(float value = 0.03f)
    {
        speed += value;
        if (speed > 0.2f) speed = 0.2f;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    public void Init(int range, int HP, float boomTime)
    {
        this.range = range;
        this.HP = HP;
        this.bombBoomTime = boomTime;
    }

    private void Update()
    {
        Move();
        CreateBomb();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        anim.SetFloat("Horizontal", h);
        anim.SetFloat("Vertical", v);
        rig.MovePosition(transform.position + new Vector3(h, v) * speed);
    }

    private void CreateBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bombCount > 0)
        {
            AudioController.Instance.PlayFire();

            bombCount--;
            GameObject bomb =  ObjectPool.Instance.Get(ObjectType.Bomb, 
                new Vector3(Mathf.RoundToInt(transform.position.x), 
                Mathf.RoundToInt(transform.position.y)));
            bomb.GetComponent<Bomb>().Init(range, bombBoomTime,() => {
                bombCount++;
                bombList.Remove(bomb);
            });

            bombList.Add(bomb);
        }
    }

    public void PlayDieAni()
    {
        Time.timeScale = 0;
        anim.SetTrigger("Die");
    }

    private void DieAniFin()
    {
        GameController.Instance.Gameover();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInjured) return;
        if (collision.CompareTag(Tags.Enemy) || collision.CompareTag(Tags.BombEffect))
        {
            if (HP <= 0)
            {
                PlayDieAni();
                return;
            }
            HP--;
            StartCoroutine("Injured", 2f);
        }
    }

    IEnumerator Injured(float time)
    {
        isInjured = true;
        for(int i = 0; i < time * 2; i++)
        {
            color.a = 0;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.25f);
            color.a = 1;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.25f);
        }
        isInjured = false;
    }
}
