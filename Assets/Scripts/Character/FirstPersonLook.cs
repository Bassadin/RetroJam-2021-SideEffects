using UnityEngine;
using System.Collections.Generic;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    public float sensitivity = 1;
    public float smoothing = 2;
    [SerializeField] private Camera fpsCamera;
    private List<ILockOnAble> lockonAbleTargetsInFOV = new List<ILockOnAble>();


    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get smooth mouse look.
        Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
        appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
        currentMouseLook += appliedMouseDelta;
        currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

        // Rotate camera and controller.
        transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);

    }

    private void FixedUpdate() {
        StartCoroutine("CheckTargetsInFOV");
    }

    private void CheckTargetsInFOV() {
        List<ILockOnAble> tempTargets = new List<ILockOnAble>();
        foreach (ILockOnAble lockonableTarget in SceneController.lockonableTargets) {
            Vector3 screenPoint = fpsCamera.WorldToViewportPoint(lockonableTarget.GetMiddle());
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (onScreen)
                tempTargets.Add(lockonableTarget);
        }
        lockonAbleTargetsInFOV = tempTargets;
        Debug.Log(lockonAbleTargetsInFOV.Count);
    }
}
