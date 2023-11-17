using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{

    [SerializeField] private float viewAngle; // 시야각 (120도);
    [SerializeField] private float viewDistance; // 시야거리 (10미터);
    [SerializeField] private LayerMask targetMask; // 타겟 마스크 (플레이어)

    private NPCColor npcColor;

    void Start()
    {
        npcColor = FindObjectOfType<NPCColor>();   
    }

    public Vector3 GetTargetPos()
    {
        return npcColor.transform.position;
    }

    public bool View()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if (_targetTf.name == "Player")
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if (_angle < viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewDistance))
                    {
                        if (_hit.transform.name == "Player")
                        {
                            Debug.Log("플레이어가 적군 시야 내에 있습니다");
                            Debug.DrawRay(transform.position + transform.up, _direction, Color.blue);
                            return true; 
                        }
                    }
                }

            }
        }
        return false;
    }
}
