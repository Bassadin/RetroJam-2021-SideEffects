using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolProjectile : MonoBehaviour, IProjectile
{
    public float damage = 5;
    [SerializeField] private Rigidbody rigidBody;
    public void DealDamage() {
        
    }

    private void OnCollisionEnter(Collision collision) {
        ITakesDamage takeDamageBehaviour = collision.gameObject.GetComponent<ITakesDamage>();
        takeDamageBehaviour?.TakeDamage(damage);
        Destroy(gameObject);
    }

    public void Awake() {
        Destroy(gameObject, 10);
    }

    public void Shoot(float weaponThrust) {
        rigidBody.AddForce(gameObject.transform.forward * weaponThrust);
    }

    void Update() {
        
    }
}
