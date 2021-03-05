using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody rigidBody;
    public void DealDamage() {
        
    }

    public void Awake() {
        Destroy(gameObject, 10);
    }

    public void Shoot(float weaponThrust) {
        Debug.Log(gameObject.transform.forward + " ///" + weaponThrust);
        rigidBody.AddForce(gameObject.transform.forward * weaponThrust);
    }

    void Update() {
        
    }
}
