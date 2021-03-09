using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField]
    GroundCheckTest groundCheck;
    public ItemSystem playeritems;
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;
    public static bool didDJ = false;


    void Reset()
    {
        groundCheck = GetComponentInChildren<GroundCheckTest>();
        if (!groundCheck)
            groundCheck = GroundCheckTest.Create(transform);
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("Jump") && (groundCheck.isGrounded || playeritems.hasDoubleJump && !didDJ))
        {
            if(!groundCheck.isGrounded)
            {
                didDJ = true;
            }
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}
