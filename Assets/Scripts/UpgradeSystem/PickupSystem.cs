using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public GameObject thisObject;
    public string objectType;
    public ItemSystem playeritems;
    private GameObject player;
    private PlayerController playercon;
    // Start is called before the first frame update
    void Start()
    {
        player = playeritems.transform.parent.gameObject;
        playercon = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(player.tag == "Player")
        {
            switch (objectType)
            {
                case "resource":
                    playeritems.resources++;
                    break;
                case "DJ":
                    if (playeritems.resources > 1)
                    {
                        playeritems.resources--;
                        playeritems.hasDoubleJump = true;
                    }
                    else
                    {

                    }
                    break;
                case "dash":
                    if (playeritems.resources > 1)
                    {
                        playeritems.resources--;
                        playeritems.hasDash = true;
                    }
                    else
                    {

                    }
                    break;
                case "ammo":
                    playercon.fillAmmo();
                    break;
                case "hp":
                    playercon.refillHP();
                    break;
                default:

                    break;
            }

            Destroy(thisObject);
        }
    }
    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
