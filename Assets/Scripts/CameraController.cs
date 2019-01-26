using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Transform positionTarget;
    [SerializeField] Transform xRotationTarget;
    [SerializeField] Transform yRotationTarget;
    [SerializeField] Camera Camera;
    [SerializeField] float distance;
    [SerializeField] float scrollSpeed;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    [SerializeField] float rotationSmoothSpeed;
    [SerializeField] float distanceSmoothSpeed;
    [SerializeField] LayerMask collisionTest;
    [SerializeField] float collisionBias = 0.1f;
    [SerializeField] float collisionZBias = 0.1f;

    private Vector3 desiredCameraOffset;
    private Vector3 nearPlaneTL;
    private Vector3 nearPlaneTR;
    private Vector3 nearPlaneBL;
    private Vector3 nearPlaneBR;

    // Update is called once per frame
    void Update () {
        SetNearPlanePoints();
        SetParentPosition();
        SetDesiredDistance();
        CheckCollisions();
    }

    private void SetNearPlanePoints()
    {
        float fovRads = (Camera.fieldOfView / 360) * 2 * Mathf.PI;
        float z = Camera.nearClipPlane;
        float x = Mathf.Tan(fovRads) * z;
        float y = x / Camera.aspect;

        z *= collisionZBias;
        x *= collisionBias;
        y *= collisionBias;

        nearPlaneTL = new Vector3(-x, y, z);
        nearPlaneTR = new Vector3(x, y, z);
        nearPlaneBL = new Vector3(-x, -y, z);
        nearPlaneBR = new Vector3(x, -y, z);
    }

    private void SetParentPosition()
    {
        this.transform.position = this.positionTarget.position;

        Quaternion targetRotation = Quaternion.Euler(yRotationTarget.localRotation.eulerAngles.x, xRotationTarget.localRotation.eulerAngles.y, 0);
        Quaternion currentRotation = this.transform.localRotation;
        Vector3 newRotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * rotationSmoothSpeed).eulerAngles;
        newRotation.z = 0;
        this.transform.localRotation = Quaternion.Euler(newRotation);
    }


    private void SetDesiredDistance()
    {
        this.distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        this.distance = Mathf.Clamp(this.distance, this.minDistance, this.maxDistance);

        // keep x and y offsets
        this.desiredCameraOffset = this.Camera.transform.localPosition;
        // set distance
        this.desiredCameraOffset.z = -distance;
    }


    private void CheckCollisions()
    {     

        RaycastHit hit;

        Vector3 wsCameraPos = this.transform.TransformPoint(this.desiredCameraOffset);
        Vector3 wsNearPlaneTL = this.transform.TransformPoint(this.desiredCameraOffset + this.nearPlaneTL);
        Vector3 wsNearPlaneTR = this.transform.TransformPoint(this.desiredCameraOffset + this.nearPlaneTR);
        Vector3 wsNearPlaneBL = this.transform.TransformPoint(this.desiredCameraOffset + this.nearPlaneBL);
        Vector3 wsNearPlaneBR = this.transform.TransformPoint(this.desiredCameraOffset + this.nearPlaneBR);

        bool collision = false;
        float hitDistRatio;
        float smallest = 1;
        if (LinecastCameraToTarget(Vector3.zero, out hitDistRatio))
        {
            collision = true;
            smallest = Mathf.Min(smallest, hitDistRatio);            
        }
        if (LinecastCameraToTarget(nearPlaneTL, out hitDistRatio))
        {
            collision = true;
            smallest = Mathf.Min(smallest, hitDistRatio);
        }
        if (LinecastCameraToTarget(nearPlaneTR, out hitDistRatio))
        {
            collision = true;
            smallest = Mathf.Min(smallest, hitDistRatio);
        }
        if (LinecastCameraToTarget(nearPlaneBL, out hitDistRatio))
        {
            collision = true;
            smallest = Mathf.Min(smallest, hitDistRatio);
        }
        if (LinecastCameraToTarget(nearPlaneBR, out hitDistRatio))
        {
            collision = true;
            smallest = Mathf.Min(smallest, hitDistRatio);
        }

        if (collision)
        {
            desiredCameraOffset.z *= smallest;
            this.Camera.transform.localPosition = this.desiredCameraOffset;
        }
        else
        {
            this.Camera.transform.localPosition = Vector3.Lerp(this.Camera.transform.localPosition, desiredCameraOffset, Time.deltaTime * distanceSmoothSpeed);
        }

    }

    private bool LinecastCameraToTarget(Vector3 offset, out float hitDistRatio)
    {
        hitDistRatio = 0;

        Vector3 wsStart, wsEnd;
        wsStart = this.transform.position;
        wsEnd = this.transform.TransformPoint(this.desiredCameraOffset + offset);

        float worldSpaceDistance = (wsStart - wsEnd).magnitude;
        RaycastHit hit;
        if (Physics.Linecast(wsStart, wsEnd, out hit, this.collisionTest))
        {
            hitDistRatio = hit.distance / worldSpaceDistance;
            return true;
        }
        return false;
    }
    
}


