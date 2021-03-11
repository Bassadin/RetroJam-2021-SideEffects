using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody;
    public ItemSystem playeritems;
    public float _dashTime = 0.5f;
    public float _dashSpeed = 20;
    private float timeStamp;
    public float cooldown = 5;
    private byte backDash = 0;
    private byte forDash = 0;
    private byte leftDash = 0;
    private byte rightDash = 0;
    public float TimeBetweenTaps = 1;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {

        if (Input.GetKeyDown(KeyCode.A) && playeritems.hasDash && timeStamp <= Time.time)
        {
            leftDash++;
            Invoke("ResetTapTimes", TimeBetweenTaps);
            if (leftDash == 2)
            {
                rigidbody.AddForce(Camera.main.transform.right * -_dashSpeed, ForceMode.VelocityChange);
                timeStamp = Time.time + cooldown;
                Invoke("StopMovement", _dashTime);
                Invoke("ResetTapTimes", TimeBetweenTaps);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) && playeritems.hasDash && timeStamp <= Time.time)
        {
            backDash++;
            Invoke("ResetTapTimes", TimeBetweenTaps);
            if (backDash == 2)
            {
                rigidbody.AddForce(Camera.main.transform.forward * -_dashSpeed, ForceMode.VelocityChange);
                timeStamp = Time.time + cooldown;
                Invoke("StopMovement", _dashTime);
                Invoke("ResetTapTimes", TimeBetweenTaps);
            }

        }
        else if (Input.GetKeyDown(KeyCode.D) && playeritems.hasDash && timeStamp <= Time.time)
        {
            rightDash++;
            Invoke("ResetTapTimes", TimeBetweenTaps);
            if (rightDash == 2)
            {
                //rigidbody.velocity = Camera.main.transform.right * _dashSpeed;
                rigidbody.AddForce(Camera.main.transform.right * _dashSpeed, ForceMode.VelocityChange);
                timeStamp = Time.time + cooldown;
                Invoke("StopMovement", _dashTime);
                Invoke("ResetTapTimes", TimeBetweenTaps);
            }

        }
        else if (Input.GetKeyDown(KeyCode.W) && playeritems.hasDash && timeStamp <= Time.time)
        {
            forDash++;
            Invoke("ResetTapTimes", TimeBetweenTaps);
            if (forDash == 2)
            {
                rigidbody.AddForce(Camera.main.transform.forward * _dashSpeed, ForceMode.VelocityChange);
                timeStamp = Time.time + cooldown;
                Invoke("StopMovement", _dashTime);
                Invoke("ResetTapTimes", TimeBetweenTaps);
            }
        }
    }

    private void StopMovement()
    {
        rigidbody.velocity = Vector3.zero;
    }

    private void ResetTapTimes()
    {
        backDash = 0;
        forDash = 0;
        leftDash = 0;
        rightDash = 0;
    }
}
