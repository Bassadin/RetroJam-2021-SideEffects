using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBehaviour : MonoBehaviour
{
    public float height;
    public Slider healthBar;
    public GameObject p;
    public PlayerController player;
    // Update is called once per frame
    void Awake()
    {
        player = p.GetComponent<PlayerController>();
        healthBar = GetComponent<Slider>();
    }

    void Update()
    {
        height = (player.currentHealth / player.maxHealth);
        healthBar.value = height;
    }
}
