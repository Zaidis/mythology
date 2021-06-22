using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyManager : MonoBehaviour
{
    public float distance;
    public float range;
    public float interval;
    public float bulletspeed;
    public float bulletTimer;
    float mover = 0;

    public int damage;

    public bool inRange = false;
    public bool attackk = false;

    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPoint;

    public GameObject flameParticle;

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            //rangeCheck();
            print(true);
    }
    public void rangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < range)
        {
            attack();
            inRange = true;
        }
        if(distance > range)
        {
            attack();
            inRange = false;
        }
    }
    public void attack()
    {
        bulletTimer += Time.deltaTime;

        Vector2 offset = target.position - transform.position;

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        shootPoint.transform.rotation = Quaternion.Euler(0f, 0f, angle + mover);

        if (bulletTimer >= interval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            if (inRange)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletspeed;

                bulletTimer = 0;
            }
        }
    }
}
