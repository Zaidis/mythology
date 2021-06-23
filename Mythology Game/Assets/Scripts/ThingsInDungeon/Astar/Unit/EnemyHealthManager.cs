using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    //public GameObject DeathParticle;
    //public GameObject attackedParticle;
    public GameObject heartDrop;

    public bool left = true;
    public bool isBoss = false;
    public Transform target;

    int dropChance;
    Transform enemyPos;

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        enemyPos = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            //Instantiate(DeathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);

            dropChance = Random.Range(0, 3);
           if(dropChance >= 2)
            {
                Instantiate(heartDrop, transform.position, Quaternion.identity);
            }

            if (isBoss)
            {
                //make stairs??
                print("you killed a boss");
            }
        }

        //checks if looking right
        if (target != null)
        {
            if (target.transform.position.x > transform.position.x)
            {
                left = false;
            }
            if (target.transform.position.x < transform.position.x)
            {
                left = true;
            }
        }
    }

    public void hurtEnemy(int damage)
    {
        enemyCurrentHealth -= damage;

    }

    public void setMaxHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

}
