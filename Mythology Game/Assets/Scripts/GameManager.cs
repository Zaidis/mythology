using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerManager player;
    public Image healthbar;
    public TextMeshProUGUI healthbarText;
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        player = FindObjectOfType<PlayerManager>();
    }

    public void ChangeHealthBarUI() {
        int num = player.health;
        print(num);
        healthbar.fillAmount = (float)num / 10;
        healthbarText.text = num + " / " + player.maxHealth;
    }
}
