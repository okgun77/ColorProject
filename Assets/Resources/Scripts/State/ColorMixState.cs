using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColorMixingState
{
    MIX_NONE,       // 색상 조합 중이 아님
    MIX_ING, // 색상 조합 중
    MIX_COMPLETE    // 색상 조합 완료
}

public class ColorMixState
{
    private EColorMixingState mixingState = EColorMixingState.MIX_NONE;

    public EColorMixingState MixingState
    {
        get { return mixingState; }
    }

    // 색상 조합 상태를 시작합니다.
    public void StartMixing()
    {
        mixingState = EColorMixingState.MIX_ING;
    }

    // 색상 조합 상태를 종료합니다.
    public void StopMixing()
    {
        mixingState = EColorMixingState.MIX_COMPLETE;
    }

    // 현재 색상 조합 상태를 초기화합니다.
    public void Reset()
    {
        mixingState = EColorMixingState.MIX_NONE;
    }
}