using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("Base stats")]
    [SerializeField] private float health;
    [SerializeField] private float searchRadius;
    [SerializeField] private float atackRadius;
    [SerializeField] private float speed;
    [SerializeField] private float timeThatBodyExistsAfterDeath;
    [SerializeField] private float timeOfDeath;
    [SerializeField] private int enemyTag;


    [Header("Atack")]
    [SerializeField] private int atackDamage;
    [SerializeField] private float atackRate;

    Rigidbody2D rb;
    Animator anim;
    Player player;
    [SerializeField] private bool isPlayerSeen = false;
    bool isAlive;
    float nextAtackTime;
    public AIPath aiPath;
   // Vector2 direction;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        nextAtackTime = Time.time;
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        aiPath.target = player.gameObject.transform;
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isAlive", true);
    }

   // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            SearchPlayer();
        }
        else
        {
            if (Time.time - timeOfDeath > timeThatBodyExistsAfterDeath)
            {
                Destroy(gameObject);
            }
        }
    }


    private void SearchPlayer()
    {
        var a = Mathf.Abs(transform.position.x - player.transform.position.x);
        var b = Mathf.Abs(transform.position.y - player.transform.position.y);
        var distansToPlayer = Mathf.Sqrt(a * a + b * b);
        if (distansToPlayer < searchRadius && distansToPlayer > atackRadius) { isPlayerSeen = true; }
        else 
        {
            isPlayerSeen = false;
            if (distansToPlayer < atackRadius)
                Atack();
        }
        //anim.SetBool("isSeen", isPlayerSeen);
    }

    void Atack()
    {
        if (nextAtackTime <= Time.time && player.GetStatus())
        {
            nextAtackTime = Time.time + atackRate;
          //  anim.SetTrigger("Atack");
            DoDamageToPlayer();
        }
    }

    public void DoDamageToPlayer()
    {
        player.GetDamage(atackDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damage = collision.GetComponent<DamageDiller>();
        if (damage != null && isAlive)
        {
            GetDamage(damage.GetDamage());
            Destroy(collision.gameObject);
        }
    }   

    public void GetDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        anim.SetBool("isAlive",false);
        isAlive = false;
        rb.velocity = Vector3.zero;
        GetComponent<AIPath>().enabled = false;
        GetComponent<AIDestinationSetter>().enabled = false;
        timeOfDeath = Time.time;
        FindObjectOfType<FightSceneManager>().EnemyDeath(enemyTag);
    }
}
