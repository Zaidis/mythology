using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabAI : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject child;
    GameObject otherChild;
    public float angleOffset;
    public float speed;
    public LayerMask mask;
    float num;
    private void Awake() {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start() {
        int rand = Random.Range(0, 255);
        int rand2 = Random.Range(0, 255);
        int rand3 = Random.Range(0, 255);
        this.GetComponent<SpriteRenderer>().color = new Color32((byte)rand, (byte)rand2, (byte)rand3, 255);


        child = this.transform.GetChild(0).gameObject;
        otherChild = this.transform.GetChild(1).gameObject;
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>() ,GameManager.instance.player.GetComponent<Collider2D>());
        
    }
    private void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, child.transform.position, speed * Time.deltaTime);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up , 1, mask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, angleOffset) * transform.up, 1, mask);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, angleOffset * -1) * transform.up, 1, mask);
        Vector3 endPoint = transform.up * 1;
        Debug.DrawLine(transform.position, transform.position + endPoint, Color.green);
        Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, angleOffset) * transform.up * 1, Color.green);
        Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, angleOffset * -1) * transform.up * 1, Color.green);
        if (hit.collider != null || hit2.collider != null || hit3.collider != null) {
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, otherChild.transform.rotation, 100 * speed * Time.deltaTime);
        }
    }

}
