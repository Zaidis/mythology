using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMelee : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    bool I_Idle = true;
    bool I_Wander = false;
    bool I_Attack = false;
    [SerializeField]
    bool I_Approach = false;
    [SerializeField]
    LayerMask player1;
    [SerializeField]
    bool Agro = false;
    [SerializeField]
    float range;
    [SerializeField]
    GameObject attackPoint;
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    Animator anim;
    bool isLeft = true;
    [SerializeField]
    string SideAttack;
    [SerializeField]
    string SideWalk;
    [SerializeField]
    int damage = 1;
    float attackMaxTimer = 2;
    float attackTimer;
    public float SearchRadius;
    bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = attackMaxTimer;
        anim = GetComponent<Animator>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        LookForPlayer();
        if (!Agro)
        {
            if (I_Idle)
            {
                idle();
            }
            if (I_Wander)
            {
                wander();
            }
        }
        if (Agro)
        {
            if (I_Approach)
            {
                //anim.Play("GatorWalkSide");
                anim.Play(SideWalk);
                //anim.Play("GatorMoveUp");
                //anim.Play("GatorMoveDown");
                if(Player.transform.position.x >= transform.position.x && isLeft )
                {
                    isLeft = !isLeft;
                    Vector2 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                if (Player.transform.position.x <= transform.position.x && !isLeft)
                {
                    isLeft = !isLeft;
                    Vector2 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                if (Vector3.Distance(transform.position, Player.transform.position) <= range)
                {
                    I_Attack = true;
                    I_Approach = false;
                }
                approach();
            }
            if (I_Attack)
            {
                if (Vector3.Distance(transform.position, Player.transform.position) > range)
                {
                    I_Attack = false;
                    I_Approach = true;
                }
                attack();
            }
        }
    }
    void LookForPlayer()
    {
        //Define Appropriate attack
        RaycastHit2D hit = Physics2D.CircleCast(attackPoint.transform.position, SearchRadius, Vector2.one, player1);
        if (hit.collider != null)
        {
            monsterAgro();
        }
        else
        {
            Agro = false;
            I_Approach = false;
            I_Attack = false;
            I_Idle = true;
            I_Wander = false;
            attacking = true;
        }
    }
    void wander()
    {
        print("I'm wandering");
    }
    void idle()
    {
        print("I'm idling and doing nothing");
        anim.Play("New State");
    }
    void attack()
    {
        gameObject.GetComponent<Unit>().target = null;
        attackFunc();
    }
    void approach()
    {
        gameObject.GetComponent<Unit>().target = Player.transform;
    }
    void attackFunc()
    {
        if (attacking)
        {
            //Define Appropriate attack
            RaycastHit2D hit = Physics2D.CircleCast(attackPoint.transform.position, attackRadius, Vector2.one, player1);
            if (hit.collider != null)
            {
                //anim.Play("GatorSideAttack");
                anim.Play(SideAttack);
                GameManager.instance.DamagePlayer(damage);
                print("I did it");

            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                attackTimer = attackMaxTimer;
                attacking = true;
            }

        }
    }
    void monsterAgro()
    {
        Agro = true;
        I_Approach = true;
        I_Attack = false;
        I_Idle = false;
        I_Wander = false;
    }
    //Ctr+K+C mass comment 
    //Ctr+K+U mass uncomment
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    print(collision);
    //    if (collision.CompareTag("Player") && !Agro)
    //    {
    //        monsterAgro();
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Agro = false;
    //        I_Approach = false;
    //        I_Attack = false;
    //        I_Idle = true;
    //        I_Wander = false;
    //    }
    //}
}
