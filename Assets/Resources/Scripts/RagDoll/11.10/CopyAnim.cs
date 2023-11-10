 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyAnim : MonoBehaviour
{
    public Transform target;
    private ConfigurableJoint joint;

    private Quaternion startingRotation;

    private void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        startingRotation = transform.rotation;
    }
    private void Update()
    {
        joint.SetTargetRotationLocal(target.rotation, startingRotation);
    }
}
