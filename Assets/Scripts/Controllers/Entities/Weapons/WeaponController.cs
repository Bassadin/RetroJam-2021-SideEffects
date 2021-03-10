using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int maxAmmo;
    public float thrust;
    public float reloadTime;

    public bool reloading = false;
    public int currentAmmo;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPosition;
    private Camera firstPersonCamera;

    void Awake() {
        currentAmmo = maxAmmo;
        firstPersonCamera = GameObject.Find("First person camera").GetComponent<Camera>();
    }
    private void Start() {
        updateAmmoInUIController();
    }

    void Update() {
        
    }

    public void ShootProjectile() {
        if (reloading) {
            Debug.Log("Currently reloading");
            return;
        }
        currentAmmo--;
        updateAmmoInUIController();
        GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPosition.position, firstPersonCamera.transform.rotation);
        ProjectileController projectileController = projectileInstance.GetComponent<ProjectileController>();
        projectileController.transform.forward = firstPersonCamera.transform.forward;
        projectileController.rigidBody.AddForce(projectileController.transform.forward * thrust);
    }

    public IEnumerator StartReload() {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        Reload();
    }

    private void Reload() {
        currentAmmo = maxAmmo;
        updateAmmoInUIController();
        reloading = false;
    }

    private void updateAmmoInUIController() {
        UIMasterController.Instance.setAmmunitionAmount(currentAmmo, maxAmmo);
    }
}
