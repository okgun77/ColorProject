using UnityEngine;

public static class ConfigurableJointExtensions
{
    /// <summary>
    /// 주어진 로컬 회전을 맞추어 조인트의 targetRotation을 설정합니다.
    /// 조인트 트랜스폼의 로컬 회전은 Start에서 캐시되어야 하며, 이 메서드로 전달되어야 합니다.
    /// </summary>
    public static void SetTargetRotationLocal(this ConfigurableJoint joint, Quaternion targetLocalRotation, Quaternion startLocalRotation)
    {
        if (joint.configuredInWorldSpace)
        {
            Debug.LogError("SetTargetRotationLocal은 월드 공간에서 설정된 조인트에 사용되어서는 안됩니다. 월드 공간 조인트의 경우, SetTargetRotation을 사용하세요.", joint);
        }
        SetTargetRotationInternal(joint, targetLocalRotation, startLocalRotation, Space.Self);
    }

    /// <summary>
    /// 주어진 월드 회전을 맞추어 조인트의 targetRotation을 설정합니다.
    /// 조인트 트랜스폼의 월드 회전은 Start에서 캐시되어야 하며, 이 메서드로 전달되어야 합니다.
    /// </summary>
    public static void SetTargetRotation(this ConfigurableJoint joint, Quaternion targetWorldRotation, Quaternion startWorldRotation)
    {
        if (!joint.configuredInWorldSpace)
        {
            Debug.LogError("SetTargetRotation은 로컬 공간에서 설정된 조인트에 사용되어서는 안됩니다. 로컬 공간 조인트의 경우, SetTargetRotationLocal을 사용하세요.", joint);
        }
        SetTargetRotationInternal(joint, targetWorldRotation, startWorldRotation, Space.World);
    }

    static void SetTargetRotationInternal(ConfigurableJoint joint, Quaternion targetRotation, Quaternion startRotation, Space space)
    {
        // 조인트의 축과 보조 축에 의해 표현되는 회전을 계산합니다
        var right = joint.axis;
        var forward = Vector3.Cross(joint.axis, joint.secondaryAxis).normalized;
        var up = Vector3.Cross(forward, right).normalized;
        Quaternion worldToJointSpace = Quaternion.LookRotation(forward, up);

        // 월드 공간으로 변환합니다
        Quaternion resultRotation = Quaternion.Inverse(worldToJointSpace);

        // 반회전을 하고 새로운 로컬 회전을 적용합니다.
        // 조인트 공간은 월드 공간의 반대이므로 우리의 값도 반전시켜야 합니다
        if (space == Space.World)
        {
            resultRotation *= startRotation * Quaternion.Inverse(targetRotation);
        }
        else
        {
            resultRotation *= Quaternion.Inverse(targetRotation) * startRotation;
        }

        // 다시 조인트 공간으로 변환합니다
        resultRotation *= worldToJointSpace;

        // 목표 회전을 우리가 계산한 새로운 회전으로 설정합니다
        joint.targetRotation = resultRotation;
    }

    /// <summary>
    /// ConfigurableJoint 설정을 조정하여 CharacterJoint 동작과 유사하게 만듭니다
    /// </summary>
    public static void SetupAsCharacterJoint(this ConfigurableJoint joint)
    {
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.angularXMotion = ConfigurableJointMotion.Limited;
        joint.angularYMotion = ConfigurableJointMotion.Limited;
        joint.angularZMotion = ConfigurableJointMotion.Limited;
        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;

        joint.rotationDriveMode = RotationDriveMode.Slerp;
        var slerpDrive = joint.slerpDrive;
        slerpDrive.mode = JointDriveMode.Position;
        slerpDrive.maximumForce = Mathf.Infinity;
        joint.slerpDrive = slerpDrive;
    }
}
