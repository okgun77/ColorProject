using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTransform : MonoBehaviour
{
    public bool isInit = false;
    private Animator anim = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInit) return;

        if (anim != null)
        {
            isInit = anim.isActiveAndEnabled;
        }
    }
}
