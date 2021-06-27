using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraAI : MonoBehaviour
{

    public float timer;
    public GameObject bolt;
    public float maxTimer;
    public GameObject shootLoc;
    [Header("Bolt")]
    public float speed;
    public float boltSize;
    public float damage;
    private void Start() {
        timer = maxTimer;
    }
    private void Update() {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            Shoot();
        }

        shootLoc.transform.right = GameManager.instance.player.gameObject.transform.position - shootLoc.transform.position;
    }

    public void Shoot() {
        GameObject b = Instantiate(bolt, shootLoc.transform.position, Quaternion.identity);
        b.GetComponent<EnemyBolt>().speed = this.speed;
        b.GetComponent<EnemyBolt>().boltSize = this.boltSize;
        b.GetComponent<EnemyBolt>().damage = this.damage;
        b.GetComponent<Rigidbody2D>().AddForce(shootLoc.transform.right * speed, ForceMode2D.Impulse);
        timer = maxTimer;
        //return;
    }
}
