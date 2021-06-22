using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemyBulletManager : MonoBehaviour
{
    public int damage;
    public GameObject flameParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
           // collision.GetComponent<PlayerHealthManager>().hurtPlayer(damage);
            Instantiate(flameParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.tag != "enemy" && collision.tag != "heart")
        {
            Destroy(gameObject);
            Instantiate(flameParticle, transform.position, Quaternion.identity);

        }

    }
}