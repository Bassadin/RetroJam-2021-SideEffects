using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Player(float _maxHealth) {
        maxHealth = _maxHealth;
        currentHealth = maxHealth;
    }
}
