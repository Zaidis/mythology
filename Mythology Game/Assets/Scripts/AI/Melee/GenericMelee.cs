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

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
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
    void wander()
    {
        print("I'm wandering");
    }
    void idle()
    {
        print("I'm idling and doing nothing");
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
        //Define Appropriate attack
        RaycastHit2D hit = Physics2D.CircleCast(attackPoint.transform.position, attackRadius, Vector2.one, player1);
        if (hit.collider != null)
        {
            print(hit.transform.gameObject);
            print("I did it");
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Agro = true;
            I_Approach = true;
            I_Attack = false;
            I_Idle = false;
            I_Wander = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Agro = false;
            I_Approach = false;
            I_Attack = false;
            I_Idle = true;
            I_Wander = false;
        }
    }
}
