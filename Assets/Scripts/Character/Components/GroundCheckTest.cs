using UnityEngine;

public class GroundCheckTest : MonoBehaviour
{
    public float maxGroundDistance = .3f;
    public bool isGrounded = true;
    public event System.Action Grounded;


    void Reset() => transform.localPosition = Vector3.up * .01f;

    void LateUpdate()
    {
        bool isGroundedNow = Physics.Raycast(transform.position, Vector3.down, maxGroundDistance);
        if (isGroundedNow && !isGrounded)
            Grounded?.Invoke();
        isGrounded = isGroundedNow;
        if(isGrounded)
        {
            DoubleJump.didDJ = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxGroundDistance))
            Debug.DrawLine(transform.position, hit.point, Color.white);
        else
            Debug.DrawLine(transform.position, transform.position + Vector3.down * maxGroundDistance, Color.red);
    }


    public static GroundCheckTest Create(Transform parent)
    {
        GameObject newGroundCheckTest = new GameObject("Ground check test");
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(newGroundCheckTest, "Created ground check test");
#endif
        newGroundCheckTest.transform.parent = parent;
        newGroundCheckTest.transform.localPosition = Vector3.up * .01f;
        return newGroundCheckTest.AddComponent<GroundCheckTest>();
    }
}
