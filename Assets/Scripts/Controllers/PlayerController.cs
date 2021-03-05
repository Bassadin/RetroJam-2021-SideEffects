using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ITakesDamage {
    Player player = new Player(50f);
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage) {
        Debug.Log(player.currentHealth);
        player.currentHealth -= damage;
        Debug.Log(player.currentHealth);
        CheckDeath();
    }

    public void CheckDeath() {
        if (player.currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
