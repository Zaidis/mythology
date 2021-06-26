﻿using System.Collections;
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
    public int keyCount { get; set; }
    public int boltSize { get; set; }
    public Color32 boltColor { get; set; }

    public Image damageBox;
    public Image attSpeedBox;
    public Image boltSizeBox;
    public Image moveSpeedBox;
    public Image maxHealthBox;
    public TextMeshProUGUI keyCounter;

    public List<Image> itemImages = new List<Image>();
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        player = FindObjectOfType<PlayerManager>();

        Physics2D.IgnoreLayerCollision(10, 11);
        Physics2D.IgnoreLayerCollision(11, 12);
        Physics2D.IgnoreLayerCollision(11, 11);
       
    }
    private void Start() {
        maxHealth = 5;
        health = maxHealth;
        damage = 1;
        attackSpeed = 2;
        speed = 3;
        boltSize = 2;
        ChangeHealthBarUI();
        boltColor = new Color32(255, 255, 255, 255);
        SettingsStats();
    }
    public void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            DamagePlayer(1);
        } else if (Input.GetKeyDown(KeyCode.Y)) {
            HealPlayer(1);
        } else if (Input.GetKeyDown(KeyCode.U)) {
            keyCount++;
            ChangeHealthBarUI();
        }
    }

    /// <summary>
    /// Affects the UI stats. 
    /// </summary>
    public void SettingsStats() {
        damageBox.fillAmount = (float)damage / 8;
        attSpeedBox.fillAmount = (float)attackSpeed / 8;
        boltSizeBox.fillAmount = (float)boltSize / 8;
        moveSpeedBox.fillAmount = (float)speed / 8;
        maxHealthBox.fillAmount = (float)maxHealth / 8;
    }

    /// <summary>
    /// This function makes sure all stats do not go below their default values. 
    /// </summary>
    public void CheckStats() {
        if(speed < 2) {
            speed = 2;
        }
        if(damage < 1) {
            damage = 1;
        }
        if(attackSpeed < 1) {
            attackSpeed = 1;
        }
        if(maxHealth < 1) {
            maxHealth = 1;
        }
        if(boltSize < 1) {
            boltSize = 1;
        }
    }
    public void PrintStats() {
        print("HEALTH: " + health);
        print("MAX HEALTH: " + maxHealth);
        print("SPEED: " + speed);
        print("DAMAGE: " + damage);
        print("ATTACK SPEED: " + attackSpeed);
    }
    public void ChangeHealthBarUI() {
        int num = health;
        //print(num);
        healthbar.fillAmount = (float)num / maxHealth;
        healthbarText.text = num + " / " + maxHealth;
        keyCounter.text = keyCount.ToString();
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
            health = 0;
            //you died
            print("You have died.");
        }
        ChangeHealthBarUI();
    }

    public void AddItemToUI(Sprite item) {
        Image img = itemImages[0];
        img.sprite = item;
        img.color = new Color32(255, 255, 255, 255);
        itemImages.RemoveAt(0);
    }
}
