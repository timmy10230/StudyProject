using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, IClickable
{

    [Header("Animation")]
    public Animator anim;
    public GameObject hitParticle;
    private float hitParticleTime = 0.2f;

    [Header("Stats")]
    public int monsterId;
    Transform targetPlayer;     //The player this unit wants to attack
    public float totalHealth;
    public float currentHealth;
    public float expGranted;
    private bool dead;

    [Header("Movement")]
    public float movementSpeed;
    public float visionRange;

    [Header("Combat")]
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    private bool attackOnCooldown;

    [Header("Animation")]
    public AnimationEvents animationEvents;

    private GameObject[] players;

    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        currentHealth = totalHealth;

        //Start Coroutines
        StartCoroutine(FindPlayerInRange());

        //Subscribe to animation Events
        animationEvents.OnAnimationAttackEvent += () => {
            targetPlayer.GetComponent<PlayerController>().GetHit(attackDamage);
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetHit(float damage)
    {
        if (dead) return;
        anim.SetInteger("Condition", 3);
        currentHealth -= damage;
        StartCoroutine(PlayParticleHit());
        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(RecoverFromHit());
    }

    bool CanAttack()
    {
        if (attackOnCooldown) return false;
        return true;
    }

    void Die()
    {
        if (!PlayerData.monstersKilled.ContainsKey(monsterId)) PlayerData.monstersKilled.Add(monsterId, new PlayerData.MonsterKills());
        //Increase the amount of times we have killed this monster[id]
        PlayerData.monstersKilled[monsterId].amount++;

        //Set dead and do the rest
        dead = true;
        DropLoot();
        foreach (GameObject go in players)
        {
            go.GetComponent<PlayerController>().SetExperience(expGranted / players.Length);
        }
        anim.SetInteger("Condition", 4);
        GameObject.Destroy(this.gameObject, 5);
    }

    void DropLoot()
    {
        print("You Get the bounty");
    }

    IEnumerator AttackRoutine()
    {
        int previousState = anim.GetInteger("Condition");
        StartCoroutine(AttackCooldown());
        anim.SetInteger("Condition", 2);
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Condition", previousState);
    }

    IEnumerator AttackCooldown()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(1 / attackSpeed);
        attackOnCooldown = false;
    }

    IEnumerator PlayParticleHit()
    {
        hitParticle.SetActive(true);
        yield return new WaitForSeconds(hitParticleTime);
        hitParticle.SetActive(false);
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Condition", 0);
    }

    IEnumerator FindPlayerInRange()
    {
        while (!dead)
        {
            yield return new WaitForSeconds(0.1f);
            //If there is a target and the distance is less than range, continue
            if (targetPlayer && Vector3.Distance(transform.position, targetPlayer.position) < visionRange)
            {
                if (Vector3.Distance(transform.position, targetPlayer.position) <= attackRange)
                {
                    if (CanAttack()) StartCoroutine(AttackRoutine());
                }
                continue;
            }
            //other wise find a new player, if couldn't find, set the target to null
            else
            {
                bool foundTarget = false;
                foreach (var p in players)
                {
                    if (Vector3.Distance(transform.position, p.transform.position) < visionRange)
                    {
                        targetPlayer = p.transform;
                        anim.SetInteger("Condition", 1);
                        foundTarget = true;
                        break;
                    }
                }
                if (!foundTarget)
                {
                    targetPlayer = null;
                    anim.SetInteger("Condition", 0);
                }
            }
        }
        //Exit While Loop
    }

    public void OnLeftClick()
    {
        Debug.Log("You clicked on " + name);
    }
}
