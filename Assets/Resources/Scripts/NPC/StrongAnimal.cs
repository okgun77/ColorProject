using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAnimal : Animal
{

    protected float ChaseTime; // 총 추격 시간
    protected float CurrentChaseTime; // 계산
    [SerializeField]
    protected float ChaseDelayTime; // 추격 딜레이

    public void Chase(Vector3 _targetPos)
    {
        isChasing = true;
        destination = _targetPos;
        nav.speed = runSpeed;
        anim.SetBool("Running", isRunning);
        nav.SetDestination(destination);
    }
}
