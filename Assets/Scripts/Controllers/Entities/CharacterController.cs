using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, ITakesDamage {
    public float maxHealth;
    public float currentHealth;

    public WeaponController currentWeapon;
    virtual public void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        Debug.Log(currentHealth);
        currentHealth -= damage;
        Debug.Log(currentHealth);
        CheckDeath();
    }

    public void CheckDeath() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
