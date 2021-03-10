using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    public float damage = 5;
    public Rigidbody rigidBody;
    private LayerMask layerMask;

    void Start() {
        
    }

    void Update() {
        
    }

    private void OnCollisionEnter(Collision collision) {
        ITakesDamage takeDamageBehaviour = collision.gameObject.GetComponent<ITakesDamage>();
        takeDamageBehaviour?.TakeDamage(damage);
        Destroy(gameObject);
    }

    public void Awake() {
        Destroy(gameObject, 10);
    }
}
