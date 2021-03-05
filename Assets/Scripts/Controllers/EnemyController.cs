using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ITakesDamage {
    Enemy enemy = new Enemy(50f);
    void Start() {

    }

    void Update() {

    }
    public void TakeDamage(float damage) {
        Debug.Log(enemy.currentHealth);
        enemy.currentHealth -= damage;
        Debug.Log(enemy.currentHealth);
        CheckDeath();
    }

    public void CheckDeath() {
        if(enemy.currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
