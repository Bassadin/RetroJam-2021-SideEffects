using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    public Enemy(float _maxHealth) {
        maxHealth = _maxHealth;
        currentHealth = maxHealth;
    }
}
