using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody;
    public ItemSystem playeritems;
    public float _dashTime;
    public float _dashSpeed;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && playeritems.hasDash)
        {
            var input = Input.inputString;
            switch(input)
            {
                case "a":
                    StartCoroutine(DashCoroutine("left"));
                    break;
                case "s":
                    StartCoroutine(DashCoroutine("back"));
                    break;
                case "d":
                    StartCoroutine(DashCoroutine("right"));
                    break;
                default:
                    StartCoroutine(DashCoroutine("forward"));
                    break;
            }
        }
    }

    private IEnumerator DashCoroutine(string direction)
    {
        float startTime = Time.time; // need to remember this to know how long to dash

        switch (direction)
        {
            case "right":
                while (Time.time < startTime + _dashTime)
                {
                    transform.Translate(transform.right * _dashSpeed * Time.deltaTime);
                    yield return null;
                }
                break;
            case "back":
                while (Time.time < startTime + _dashTime)
                {
                    transform.Translate((-transform.forward) * _dashSpeed * Time.deltaTime);
                    yield return null;
                }
                break;
            case "left":
                while (Time.time < startTime + _dashTime)
                {
                    transform.Translate((-transform.right) * _dashSpeed * Time.deltaTime);
                    yield return null;
                }
                break;
            default:
                while (Time.time < startTime + _dashTime)
                {
                    transform.Translate(transform.forward * _dashSpeed * Time.deltaTime);
                    yield return null;
                }
                break;
        }
    }
}
