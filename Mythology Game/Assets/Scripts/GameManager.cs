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

    //all statistics are held by game manager
    public int health { get; set; }
    public int speed { get; set; }
    public int damage { get; set; }
    public int attackSpeed { get; set; }
    public int  maxHealth { get; set; }

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        player = FindObjectOfType<PlayerManager>();
    }
    private void Start() {
        maxHealth = 10;
        health = maxHealth;
    }
    public void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            DamagePlayer(1);
        } else if (Input.GetKeyDown(KeyCode.Y)) {
            HealPlayer(1);
        }
    }
    public void ChangeHealthBarUI() {
        int num = health;
        //print(num);
        healthbar.fillAmount = (float)num / maxHealth;
        healthbarText.text = num + " / " + maxHealth;
    }

    public void IncreaseMaxHealth(int amount) {
        maxHealth += amount;
        ChangeHealthBarUI();
    }
    public void HealPlayer(int amount) {
        health += amount;
        if (health > maxHealth) {
            health = maxHealth;
        }
        ChangeHealthBarUI();
    }

    public void DamagePlayer(int amount) {
        health -= amount;
        if (health <= 0) {
            //you died
            print("You have died.");
        }
        ChangeHealthBarUI();
    }
}
