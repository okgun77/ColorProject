using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class FootMovement : MonoBehaviour
{
    [SerializeField] float radius = 1f;
    [SerializeField] int steps = 10;
    [SerializeField] float heightMultiplier = 0.5f;
    [SerializeField] Vector3 footLiftAngleMultiplier = Vector3.zero;
    [SerializeField] Transform LFootCenter, RFootCenter;
    [SerializeField] Transform LFootTarget, RFootTarget;
    Quaternion LFootInitRotation, RFootInitRotation;

    [SerializeField] Vector3[] LFootPoints, RFootPoints;

    [SerializeField] float walkCyclePercentage = 0;
    float RPositionPercentageOnLine = 0;
    float LPositionPercentageOnLine = 0;
    [SerializeField] private float forwardMultiplier;

    [SerializeField]TwoBoneIKConstraint LIK, RIK;

    private void FixedUpdate()
    {
        LFootPoints = new Vector3[steps];
        RFootPoints = new Vector3[steps];
        CalcWalkCyclePoints(LFootCenter, LFootPoints);
        CalcWalkCyclePoints(RFootCenter, RFootPoints);
    }

    private void Start()
    {
        LFootInitRotation = LFootTarget.rotation;
        RFootInitRotation = RFootTarget.rotation;
    }

    public void Walk(float forwardValue)
    {
        walkCyclePercentage += forwardValue * forwardMultiplier;
        LIK.weight = forwardValue;
        RIK.weight = forwardValue;
    }

    private void Update()
    {
        CalcWalkCyclePercentage();
        MoveAndRotateTargetToPoint(LFootTarget, LFootPoints, LPositionPercentageOnLine, LFootInitRotation);
        MoveAndRotateTargetToPoint(RFootTarget, RFootPoints, RPositionPercentageOnLine, RFootInitRotation);
    }

    private void CalcWalkCyclePercentage()
    {
        LPositionPercentageOnLine = walkCyclePercentage % 1;
        RPositionPercentageOnLine = (walkCyclePercentage + 0.5f) % 1;
    }

    private void MoveAndRotateTargetToPoint(Transform footTarget, Vector3[] footPoints, float positionPercentageOnLine, Quaternion footInitRotation)
    {
        int pointIndex = Mathf.FloorToInt(steps * Mathf.Abs(positionPercentageOnLine));
        if (pointIndex == steps)
        {
            pointIndex = 0;
        }

        float currentY = footPoints[pointIndex].y;
        float heightLerpValue = Mathf.Lerp(0, 1, currentY / (radius * heightMultiplier));

        footTarget.rotation = footInitRotation * Quaternion.Euler(heightLerpValue * footLiftAngleMultiplier);
        footTarget.position = footPoints[pointIndex];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(LFootCenter.position, .1f);
        Gizmos.DrawSphere(RFootCenter.position, .1f);
        if (LFootPoints.Length > 0)
        {
            for (int i = 0; i < LFootPoints.Length; i++)
            {
                Gizmos.color = Color.yellow;
                int nextIndex = (i + 1) % LFootPoints.Length;

                Gizmos.DrawLine(LFootPoints[i], LFootPoints[nextIndex]);
                Gizmos.DrawLine(RFootPoints[i], RFootPoints[nextIndex]);
            }
        }
    }

    private void CalcWalkCyclePoints(Transform center, Vector3[] array)
    {
        float angleDiff = 360 / steps;

        for (int i = 0; i < steps; i++)
        {
            float angleInRad = i * angleDiff * Mathf.Deg2Rad;
            float z = -1 * radius * Mathf.Cos(angleInRad);
            float y = radius * Mathf.Sin(angleInRad);

            y *= heightMultiplier;
            y = Mathf.Max(y, 0);

            Vector3 point = center.position + new Vector3(0, y, z);
            array[i] = point;
        }
    }
}
