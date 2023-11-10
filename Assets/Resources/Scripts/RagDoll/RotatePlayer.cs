using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private Rigidbody rb;
    public float rotateSpeed;

    Vector3 rotationLeft;
    Vector3 rotationRight;

    private void Start()
    {
     rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rotationLeft.Set(0f, -rotateSpeed, 0f);
        rotationRight.Set(0f, rotateSpeed, 0f);

        rotationLeft = -rotationLeft.normalized * -rotateSpeed;
        rotationRight = rotationRight.normalized * rotateSpeed;

        Quaternion deltataRotationLeft = Quaternion.Euler(rotationLeft * Time.fixedDeltaTime);
        Quaternion deltaRotationRight = Quaternion.Euler(rotationRight * Time.fixedDeltaTime);

        if(Input.GetKey (KeyCode.Q))
        {
            rb.MoveRotation(rb.rotation * deltataRotationLeft);
            Debug.Log("왼쪽실행");
        }
        if(Input.GetKey (KeyCode.E))
        {
            rb.MoveRotation(rb.rotation * deltaRotationRight);
            Debug.Log("오른쪽실행");
        }
    }
}
