using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour, ITakesDamage {
    public float maxHealth;
    public float currentHealth;

    public WeaponController currentWeapon;

    virtual public void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        Debug.Log(gameObject + " - New current health: " + currentHealth);
        CheckDeath();
    }

    public void CheckDeath() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    public abstract void ShootWeapon();
}
