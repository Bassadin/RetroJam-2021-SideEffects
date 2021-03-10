using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController, ICanAttack {

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            AttackWithWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine("StartReload");
        }
    }

    public void AttackWithWeapon() {
        currentWeapon?.ShootProjectile();
    }

    public IEnumerator StartReload() {
        return currentWeapon?.StartReload();
    }
}
