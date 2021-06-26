using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraAI : MonoBehaviour
{

    public GameObject child;
    public float speed;
    public LayerMask mask;
    private void Start() {
        child = this.transform.GetChild(0).gameObject;
        speed = 2f;
    }


    private void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, child.transform.position, speed * Time.deltaTime);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1, mask);
        if (hit.collider != null) {
            int rand = Random.Range(1, 3);
            switch (rand) {
                case 1:
                    var ang = Quaternion.Euler(new Vector3(this.transform.rotation.x, transform.rotation.y, transform.rotation.z + 90));
                    transform.rotation = Quaternion.Slerp(this.transform.rotation, ang, 100);
                    break;
                case 2:
                    var ang2 = Quaternion.Euler(new Vector3(this.transform.rotation.x, transform.rotation.y, transform.rotation.z + 180));
                    transform.rotation = Quaternion.Slerp(this.transform.rotation, ang2, 100);
                    break;
                case 3:
                    var ang3 = Quaternion.Euler(new Vector3(this.transform.rotation.x, transform.rotation.y, transform.rotation.z + 270));
                    transform.rotation = Quaternion.Slerp(this.transform.rotation, ang3, 100);
                    break;

            }

        }
    }
}
