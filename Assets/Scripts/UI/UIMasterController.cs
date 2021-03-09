using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMasterController : MonoBehaviour
{
    public Text ammunitionText;

    //Singleton Stuff
    private static UIMasterController _instance;

    public static UIMasterController Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void setAmmunitionAmount(int currentAmmo, int maxAmmo)
    {
        ammunitionText.text = $"Ammo: {currentAmmo}/{maxAmmo}";
    }
}
