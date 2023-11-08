using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCT : MonoBehaviour
{
    public Transform Object; // ī�޶� ���� ��� ��ü

    public Vector3 PosCam; // ī�޶��� �ʱ� ��ġ
    public float VelZoom; // �� ��/�ƿ� �ӵ�
    public float MinZoom; // �ּ� �� �Ÿ�
    public float MaxZoom; // �ִ� �� �Ÿ�
    public float VelRot = 100; // ȸ�� �ӵ�
    private float ZoomActural = 1; // ���� �� ����
    private float AlturaCam; // ī�޶��� ����
    public float AlturaCamInput = 1; // ����� �Է¿� ���� ī�޶� ���� ���� ��
    public float Maxlutra = -10; // ī�޶��� �ִ� ���� ����
    public float Minaltura = -0.5f; // ī�޶��� �ּ� ���� ����
    private float Rotinput; // ȸ�� �Է� ��

    private void Start()
    {
        AlturaCam = PosCam.y; // ���� �� ī�޶� ���� �ʱ�ȭ
        Screen.lockCursor = true; // ���콺 Ŀ���� ��޴ϴ� (Ŀ�� ����)
    }

    private void Update()
    {
        // ���콺 ��ũ�ѿ� ���� �� ������ �����մϴ�.
        ZoomActural -= Input.GetAxis("Mouse ScrollWheel") * VelZoom;
        // ���콺 Y�� �Է¿� ���� ī�޶� ���̸� �����մϴ�.
        AlturaCam = -Input.GetAxis("Mouse Y") * 5 * Time.deltaTime;
        AlturaCamInput -= AlturaCam; // ���� �Է� ���� �����մϴ�.

        // ī�޶� ���̸� ���� ���� ���� �����մϴ�.
        AlturaCamInput = Mathf.Clamp(AlturaCamInput, Maxlutra, Minaltura);

        PosCam.y = AlturaCamInput; // ī�޶� ���̸� ������Ʈ�մϴ�.
        // �� ������ ���� ���� ���� �����մϴ�.
        ZoomActural = Mathf.Clamp(ZoomActural, MinZoom, MaxZoom);

        // ���콺 X�� �Է¿� ���� ȸ�� �Է� ���� �����մϴ�.
        Rotinput -= Input.GetAxis("Mouse X") * VelRot * Time.deltaTime;
    }

    private void LateUpdate()
    {
        // ī�޶� ��ġ�� ��� ��ü�� ���� ���������� ������Ʈ�մϴ�.
        transform.position = Object.position - PosCam * ZoomActural;
        // ��� ��ü�� �ٶ󺸵��� ī�޶� ȸ���մϴ�.
        transform.LookAt(Object.position);
        // ��� ��ü ������ ȸ�� �Է� ���� ���� ȸ���մϴ�.
        transform.RotateAround(Object.position, Vector3.up, -Rotinput);
    }
}
