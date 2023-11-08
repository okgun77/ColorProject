using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyBonePositions : MonoBehaviour
{
    [SerializeField] Transform limb;
    ConfigurableJoint joint;

    Quaternion targetInitRotation;

    private void Start()
    {
        this.joint = GetComponent<ConfigurableJoint>();
        this.targetInitRotation = this.limb.transform.rotation;

    }

    private void FixedUpdate()
    {
        UpdateRotation();
    }

    Quaternion UpdateRotation()
    {
        return Quaternion.Inverse(this.limb.localRotation) * this.targetInitRotation;
    }
}
