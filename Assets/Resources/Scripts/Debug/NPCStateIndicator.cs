using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCStateIndicator : MonoBehaviour
{
    public TextMeshProUGUI statusText; // 상태를 표시할 TextMeshProUGUI
    private Transform cameraTransform; // 메인 카메라의 Transform

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    public void UpdateStateText(string _newState)
    {
        if (statusText != null)
        {
            statusText.text = _newState;
        }
    }

    private void Update()
    {
        // TextMeshProUGUI가 항상 카메라를 바라보도록 회전
        if (cameraTransform != null)
        {
            // var rotation = Quaternion.LookRotation(statusText.transform.position - cameraTransform.position);
            // statusText.transform.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);

            Vector3 targetPosition = new Vector3(cameraTransform.position.x, this.transform.position.y, cameraTransform.position.z);
            this.transform.LookAt(targetPosition);

            this.transform.Rotate(0f, 180f, 0f);
        }
    }
}
