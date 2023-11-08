using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private float updateInterval = 0.5f; // 0.5�ʸ��� FPS�� ������Ʈ
    [SerializeField] private TextMeshProUGUI framerateText;
    private float accum = 0; // �ð� ������ ���� ����
    private int frames = 0; // ������ ������ ��
    private float timeLeft; // ������Ʈ���� ���� �ð�
    
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

        // ���� �ð��� �������� Ȯ��
        if (timeLeft <= 0.0)
        {
            // ��� FPS ���
            float fps = accum / frames;
            string format = System.String.Format("{0:F0} FPS", fps);
            framerateText.text = format;

            if (fps < 30)
                framerateText.color = Color.yellow;
            else if (fps < 10)
                framerateText.color = Color.red;
            else
                framerateText.color = Color.green;

            // �������� �缳��
            timeLeft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }
}