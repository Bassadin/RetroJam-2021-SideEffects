using UnityEngine;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine to shake the camera
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class CameraWalkBobbing : CinemachineExtension
{
    public float xFrequency = .1f;
    public float xAmplitude = .1f;
    public float yFrequency = .1f;
    public float yAmplitude = .1f;

    private float walkingTime;
    private bool isWalking = false;

    public Transform headTransform;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            if (!isWalking)
            {
                walkingTime = 0;
            }
            else
            {
                walkingTime += Time.deltaTime;
            }

            Vector3 shakeAmount = GetOffset(walkingTime);
            state.PositionCorrection += shakeAmount;
        }
    }

    Vector3 GetOffset(float time)
    {
        float horizontalOffset = 0;
        float verticalOffset = 0;
        Vector3 offset = Vector3.zero;

        if (time > 0)
        {
            horizontalOffset = Mathf.Cos(time * xFrequency) * xAmplitude;
            verticalOffset = Mathf.Sin(time * yFrequency) * yAmplitude;

            offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;
        }
        return offset;
    }

    public void SetWalking(bool isWalking)
    {
        this.isWalking = isWalking;
    }
}