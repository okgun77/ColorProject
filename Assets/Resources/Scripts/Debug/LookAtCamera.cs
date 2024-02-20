using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        // 카메라의 방향을 바라보게 합니다.
        // 이 경우, 카메라의 위치를 향해 UI 요소의 전면이 바라보게 됩니다.
        transform.LookAt(Camera.main.transform.position);

        // 카메라가 UI의 뒷면을 보는 문제를 해결하기 위해 180도 회전시킵니다.
        // 이는 UI 요소가 3D 공간에서 카메라를 바라볼 때 필요한 조정입니다.
        transform.Rotate(0, 180, 0);
    }
}
