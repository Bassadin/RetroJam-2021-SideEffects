﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 500;
    public float smoothing = 2;
    [SerializeField] private Camera fpsCamera;
    private float xRotation = 0f;
    private List<ILockOnAble> lockonAbleTargetsInFOVLeft = new List<ILockOnAble>();
    private List<ILockOnAble> lockonAbleTargetsInFOVRight = new List<ILockOnAble>();
    private bool targetMode = false;
    private ILockOnAble currentlyLockedOnTarget;
    [SerializeField] private FirstPersonMovement firstPersonMovement;

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
        if (Input.GetKey(KeyCode.LeftShift)) {
            UseTargetMode();
            firstPersonMovement.Strafe();
        }
        else if(Input.GetKey(KeyCode.RightShift)) {
            firstPersonMovement.canMove = false;
            RotateHorizontically();
            RotateVertically();
        }
        else {
            RotateHorizontically();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) {
            currentlyLockedOnTarget = null;
            firstPersonMovement.canMove = true;
            SetVerticalRotationToZero();
        }
    }

    private void RotateHorizontically() {
        float cameraX = Input.GetAxis("Horizontal") * sensitivity * Time.deltaTime;
        
        character.Rotate(Vector3.up * cameraX);
    }

    private void RotateVertically() {
        float cameraY = Input.GetAxis("Vertical") * sensitivity * Time.deltaTime;

        xRotation -= cameraY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void SetVerticalRotationToZero() {
        Quaternion currentRotation = transform.localRotation;
        //set x rotation to 0
        currentRotation.x = 0;
        transform.localRotation = currentRotation;
        //reset variable to 0 for vertical rotation function
        xRotation = 0;
    }

    private void UseTargetMode() {
        if (currentlyLockedOnTarget != null) {           
            SetCameraCenterOnLockTargetCenter();
        }
        try {
            if (Input.GetKeyDown(KeyCode.Q)) {
                CheckTargetsInFOV();
                currentlyLockedOnTarget = lockonAbleTargetsInFOVLeft[0];
            }
            else if (Input.GetKeyDown(KeyCode.E)) {
                CheckTargetsInFOV();
                currentlyLockedOnTarget = lockonAbleTargetsInFOVRight[0];
            }
        } catch (Exception arrayOutOfBounds) {
            //tried to get a target on either side, when there was none in the fov in that direction
            //Debug.Log(arrayOutOfBounds);
        }
    }

    private void CheckTargetsInFOV() {
        List<ILockOnAble> tempTargetsRight = new List<ILockOnAble>();
        List<ILockOnAble> tempTargetsLeft = new List<ILockOnAble>();
        //loop through all LockonableTargets in Scene and set those to the specific List
        foreach (ILockOnAble lockonableTarget in SceneController.getLockonableTargets()) {
            //we get the world positions and form them to the viewport location ( 0,0 is bottom left of camera viewport and 1,1 top right, so everything with an x less than 0.5 is left of center)
            Vector3 screenPoint = fpsCamera.WorldToViewportPoint(lockonableTarget.GetMiddle());
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (onScreen && currentlyLockedOnTarget != lockonableTarget) {
                if(screenPoint.x > 0.5)
                    tempTargetsRight.Add(lockonableTarget);
                else if(screenPoint.x <= 0.5)
                    tempTargetsLeft.Add(lockonableTarget);
                lockonableTarget.SetScreenPoint(screenPoint);
            }
        }
        tempTargetsRight.Sort(RightSort);
        tempTargetsLeft.Sort(LeftSort);
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
        Vector3 directionTowardsTargetFromCamera = currentlyLockedOnTarget.GetMiddle() - transform.position;
        Vector3 directionTowardsTargetFromCharacter = currentlyLockedOnTarget.GetMiddle() - character.position;

        
        Vector3 XZPlaneProjection = Vector3.ProjectOnPlane(directionTowardsTargetFromCharacter, character.up);
        float yRotation = Vector3.SignedAngle(character.forward, XZPlaneProjection, character.up);
        character.Rotate(new Vector3(0, yRotation, 0));

        Vector3 YZPlaneProjection = Vector3.ProjectOnPlane(directionTowardsTargetFromCamera, transform.right);
        float xRotation = Vector3.SignedAngle(transform.forward, YZPlaneProjection, transform.right);
        Debug.Log(xRotation);
        transform.Rotate(new Vector3(xRotation, 0, 0));


        //character.transform.localRotation = Quaternion.AngleAxis(xRotation, Vector3.up);

        currentlyLockedOnTarget.GetGameObject().GetComponent<Renderer>().material.color = Color.red;
        //transform.LookAt(currentlyLockedOnTarget.GetMiddle());
    }

    void OnDrawGizmosSelected() {
        
    }
}
