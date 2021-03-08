using UnityEngine;
using System.Collections.Generic;
using System;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    public float sensitivity = 1;
    public float smoothing = 2;
    [SerializeField] private Camera fpsCamera;
    private List<ILockOnAble> lockonAbleTargetsInFOVLeft = new List<ILockOnAble>();
    private List<ILockOnAble> lockonAbleTargetsInFOVRight = new List<ILockOnAble>();
    private bool lockedOn = false;
    private ILockOnAble currentlyLockedOnTarget;

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
        if(!Input.GetMouseButton(1) && (Mathf.Abs(Input.GetAxisRaw("Mouse X")) > 0 || Mathf.Abs(Input.GetAxisRaw("Mouse Y")) > 0)) {
            // Get smooth mouse look.
            Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
            appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
            currentMouseLook += appliedMouseDelta;
            currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

            // Rotate camera and controller.
            transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
        }
        else {
            if (Mathf.Abs(Input.GetAxisRaw("Mouse X")) > 0) {
                CheckTargetsInFOV();
                try {
                    if (Input.GetAxisRaw("Mouse X") < 0)
                        currentlyLockedOnTarget = lockonAbleTargetsInFOVLeft[0];
                    else if (Input.GetAxisRaw("Mouse X") > 0)
                        currentlyLockedOnTarget = lockonAbleTargetsInFOVRight[0];
                    SetCameraCenterOnLockTargetCenter();
                } catch(Exception arrayOutOfBounds) {
                    //tried to get a target on either side, when there was none in the fov in that direction
                }
            }
        }
    }

    private void FixedUpdate() {

    }

    private void CheckTargetsInFOV() {
        List<ILockOnAble> tempTargetsLeft = new List<ILockOnAble>();
        List<ILockOnAble> tempTargetsRight = new List<ILockOnAble>();
        //loop through all LockonableTargets in Scene and set those to the specific List
        foreach (ILockOnAble lockonableTarget in SceneController.getLockonableTargets()) {
            //we get the world positions and form them to the viewport location ( 0,0 is bottom left of camera viewport and 1,1 top right, so everything with an x less than 0.5 is left of center)
            Vector3 screenPoint = fpsCamera.WorldToViewportPoint(lockonableTarget.GetMiddle());
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (onScreen) {
                if(screenPoint.x <= 0.5)
                    tempTargetsLeft.Add(lockonableTarget);
                else
                    tempTargetsRight.Add(lockonableTarget);
                lockonableTarget.SetScreenPoint(screenPoint);
            }
        }
        tempTargetsLeft.Sort(LeftSort);
        tempTargetsRight.Sort(RightSort);
        lockonAbleTargetsInFOVLeft = tempTargetsLeft;
        lockonAbleTargetsInFOVRight = tempTargetsRight;
    }

    private int LeftSort(ILockOnAble c1, ILockOnAble c2) {
        if (c1.GetScreenPoint().x < c2.GetScreenPoint().x) {
            return 1;
        }
        else if (c1.GetScreenPoint().x > c2.GetScreenPoint().x) {
            return -1;
        }
        return 0;
    }
    private int RightSort(ILockOnAble c1, ILockOnAble c2) {
        if (c1.GetScreenPoint().x > c2.GetScreenPoint().x) {
            return 1;
        }
        else if (c1.GetScreenPoint().x < c2.GetScreenPoint().x) {
            return -1;
        }
        return 0;
    }

    private void SetCameraCenterOnLockTargetCenter() {
        Vector3 directionTowardsTarget = currentlyLockedOnTarget.GetMiddle() - transform.position;
        Vector3 projectedOntoYZ = Vector3.ProjectOnPlane(directionTowardsTarget, transform.right);
       
        float xRotation = Vector3.SignedAngle(transform.forward, projectedOntoYZ, Vector3.right);
        Debug.Log(xRotation);
        transform.Rotate(new Vector3(xRotation, 0, 0));

        Vector3 projectedOntoXZ = Vector3.ProjectOnPlane(directionTowardsTarget, transform.up);

        float yRotation = Vector3.SignedAngle(transform.forward, projectedOntoXZ, Vector3.up);
        character.Rotate(new Vector3(0, yRotation, 0));


        //transform.LookAt(currentlyLockedOnTarget.GetMiddle());
    }

    void OnDrawGizmosSelected() {
        
    }
}
