using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCT : MonoBehaviour
{
    public Transform Object;

    public Vector3 PosCam;
    public float VelZoom;
    public float MinZoom;
    public float MaxZoom;
    public float VelRot = 100;
    private float ZoomActural = 1;
    private float AlturaCam = 5;
    public float AlturaCamInput = 1;
    public float Maxlutra = -10;
    public float Minaltura = -0.5f;
    private float Rotinput;

    private void Start()
    {
        AlturaCam = PosCam.y;
        Screen.lockCursor = true;
    }

    private void Update()
    {
        ZoomActural -= Input.GetAxis("Mouse ScrollWheel") * VelZoom;
        AlturaCam = -Input.GetAxis("Mouse Y") * 5 * Time.deltaTime;
        AlturaCamInput -= AlturaCam;

        AlturaCamInput = Mathf.Clamp(AlturaCamInput, Maxlutra, Minaltura);

        //if(AlturaCamInput > -0.5f)
        //{
        //    AlturaCamInput = -0.5f;
        //}
        PosCam.y = AlturaCamInput;
        ZoomActural = Mathf.Clamp(ZoomActural, MinZoom, MaxZoom);

        Rotinput -= Input.GetAxis("Mouse X") * VelRot * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = Object.position - PosCam * ZoomActural;
        transform.LookAt(Object.position);
        transform.RotateAround(Object.position, Vector3.up, -Rotinput);
    }
}
