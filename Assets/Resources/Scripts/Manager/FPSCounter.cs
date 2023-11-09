using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private float updateInterval = 0.5f; // 0.5초마다 FPS를 업데이트
    [SerializeField] private TextMeshProUGUI framerateText;
    private float accum = 0; // 시간 누적을 위한 변수
    private int frames = 0; // 누적된 프레임 수
    private float timeLeft; // 업데이트까지 남은 시간
    
    void Start()
    {
        if (!framerateText)
        {
            Debug.Log("FPSCounter needs a Text component!");
            enabled = false;
            return;
        }

        timeLeft = updateInterval;  
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // 갱신 시간이 지났는지 확인
        if (timeLeft <= 0.0)
        {
            // 평균 FPS 계산
            float fps = accum / frames;
            string format = System.String.Format("{0:F0} FPS", fps);
            framerateText.text = format;

            if (fps < 30)
                framerateText.color = Color.yellow;
            else if (fps < 10)
                framerateText.color = Color.red;
            else
                framerateText.color = Color.green;

            // 변수들을 재설정
            timeLeft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }
}