using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortOrder : MonoBehaviour
{
    private int initialOrder = 100;
    private float offset = 0;
    [SerializeField] private bool runOnce = false;
    private Renderer rend;
    private float timer;
    private float maxTimer = .1f;

    private void Awake() {
        rend = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            timer = maxTimer;
            rend.sortingOrder = (int)(initialOrder - transform.position.y - offset);

            if (runOnce) {
                Destroy(this);
            }
        }
    }
}
