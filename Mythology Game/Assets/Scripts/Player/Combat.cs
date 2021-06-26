using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject bolt; //the thing you shoot
    public float shootingTimer;
    public bool canShoot;
    [Header("SHOOT DIRECTION")]
    public bool shootUp;
    public bool shootDown;
    public bool shootLeft;
    public bool shootRight;
    //Combat Manager
    private void Update() {
        shootingTimer -= Time.deltaTime;
        if(shootingTimer <= 0) {
            shootingTimer = 0;
            canShoot = true;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            //shoot up
            shootUp = true;
            shootDown = false;
            shootLeft = false;
            shootRight = false;
            if (canShoot) {
                ShootBolt();
            }
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            //shoot down
            shootDown = true;
            shootUp = false;
            shootLeft = false;
            shootRight = false;
            if (canShoot) {
                ShootBolt();
            }
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            //shoot left
            shootLeft = true;
            shootUp = false;
            shootDown = false;
            shootRight = false;
            if (canShoot) {
                ShootBolt();
            }
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            //shoot right
            shootRight = true;
            shootUp = false;
            shootDown = false;
            shootLeft = false;
            if (canShoot) {
                ShootBolt();
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            shootUp = false;
        } else if (Input.GetKeyUp(KeyCode.DownArrow)) {
            shootDown = false;
        } else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            shootLeft = false;
        } else if (Input.GetKeyUp(KeyCode.RightArrow)) {
            shootRight = false;
        }
    }
    public void ShootBolt() {

        Instantiate(bolt, transform.position, Quaternion.identity);
        canShoot = false;
        shootingTimer = (2 / (float)GameManager.instance.attackSpeed);
    }
}
