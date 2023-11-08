using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCT : MonoBehaviour
{
    public Transform Object; // 카메라가 따라갈 대상 객체

    public Vector3 PosCam; // 카메라의 초기 위치
    public float VelZoom; // 줌 인/아웃 속도
    public float MinZoom; // 최소 줌 거리
    public float MaxZoom; // 최대 줌 거리
    public float VelRot = 100; // 회전 속도
    private float ZoomActural = 1; // 현재 줌 레벨
    private float AlturaCam; // 카메라의 높이
    public float AlturaCamInput = 1; // 사용자 입력에 의한 카메라 높이 조정 값
    public float Maxlutra = -10; // 카메라의 최대 높이 제한
    public float Minaltura = -0.5f; // 카메라의 최소 높이 제한
    private float Rotinput; // 회전 입력 값

    private void Start()
    {
        AlturaCam = PosCam.y; // 시작 시 카메라 높이 초기화
        Screen.lockCursor = true; // 마우스 커서를 잠급니다 (커서 숨김)
    }

    private void Update()
    {
        // 마우스 스크롤에 따라 줌 레벨을 조정합니다.
        ZoomActural -= Input.GetAxis("Mouse ScrollWheel") * VelZoom;
        // 마우스 Y축 입력에 따라 카메라 높이를 조절합니다.
        AlturaCam = -Input.GetAxis("Mouse Y") * 5 * Time.deltaTime;
        AlturaCamInput -= AlturaCam; // 높이 입력 값을 누적합니다.

        // 카메라 높이를 제한 범위 내로 고정합니다.
        AlturaCamInput = Mathf.Clamp(AlturaCamInput, Maxlutra, Minaltura);

        PosCam.y = AlturaCamInput; // 카메라 높이를 업데이트합니다.
        // 줌 레벨을 제한 범위 내로 고정합니다.
        ZoomActural = Mathf.Clamp(ZoomActural, MinZoom, MaxZoom);

        // 마우스 X축 입력에 따라 회전 입력 값을 조절합니다.
        Rotinput -= Input.GetAxis("Mouse X") * VelRot * Time.deltaTime;
    }

    private void LateUpdate()
    {
        // 카메라 위치를 대상 객체에 대한 오프셋으로 업데이트합니다.
        transform.position = Object.position - PosCam * ZoomActural;
        // 대상 객체를 바라보도록 카메라를 회전합니다.
        transform.LookAt(Object.position);
        // 대상 객체 주위를 회전 입력 값에 따라 회전합니다.
        transform.RotateAround(Object.position, Vector3.up, -Rotinput);
    }
}
