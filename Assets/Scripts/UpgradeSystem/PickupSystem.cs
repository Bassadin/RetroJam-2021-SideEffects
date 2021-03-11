using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public GameObject thisObject;
    public string objectType;
    public ItemSystem playeritems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(objectType)
        {
            case "resource":
                playeritems.resources++;
                break;
            case "doublejump":
                if(playeritems.resources > 1)
                {
                    playeritems.resources--;
                    playeritems.hasDoubleJump = true;
                }
                break;
            case "dash":
                if (playeritems.resources > 1)
                {
                    playeritems.resources--;
                    playeritems.hasDash = true;
                }
                break;
            default:

                break;
        }
        
        Destroy(thisObject);
    }
    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
