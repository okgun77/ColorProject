using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private TimerManager timerManager;
    [SerializeField] private float elapseTime;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private ColorTable colorTable;
    [SerializeField] private Image targetColorImage;
    // [SerializeField] private TextMeshProUGUI resultText;
    
    [SerializeField] private PlayerColor playerColor;
    private ColorBasicInfo targetColorInfo;
    private NPCColor npcColor;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject colorMatchCheck;
    
    
    public static GameManager Instance { get; private set; }
    
    public TextMeshProUGUI timeText; // UI에 시간을 표시할 Text 컴포넌트 참조
    public GameObject gameOverScreen; // 게임 오버 화면의 참조

    private bool isPaused = false;


    private void Awake()
    {
        // 싱글턴 패턴 GameManager 인스턴스
        if (Instance == null)
        {
            Instance = this; 
            // DontDestroyOnLoad(gameObject); // 씬이 변경되어도 파괴안됨
        }
        else
        {
            Destroy(gameObject);
        }
        
        playerColor.Init();
        // npcColor.Init();
    }

    private void Start()
    {


        
        timerManager.Init(elapseTime);
        
        // Application.targetFrameRate = 144;
        // QualitySettings.vSyncCount = 1;
            
        // 이미 SerializeField를 통해 할당했으므로 이 줄은 필요 없습니다.
        // timerManager = GetComponent<TimerManager>();
    
        // 대신, timerManager가 할당되었는지 확인합니다.
        if (timerManager == null)
        {
            Debug.LogError("TimerManager is not assigned in the inspector!");
        }
        else
        {
            StartGame();
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        
        // 플레이어가 목표 색상을 매칭했는지 확인
        // if (playerColor.IsTargetColorAchieved(targetColorInfo))
        // {
        //     // 목표 색상 매칭 성공 시 UI 업데이트
        //     uiManager.ShowTargetColorMatchedIndicator();
        // }
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 시간을 정지합니다.
            uiManager.ShowPauseMenu(); // 일시 정지 메뉴를 표시합니다.
        }
        else
        {
            Time.timeScale = 1f; // 게임 시간을 재개합니다.
            uiManager.HidePauseMenu(); // 일시 정지 메뉴를 숨깁니다.
        }
    }

    public void PauseGame()
    {
        isPaused = false;
        Time.timeScale = 0f; // 게임 시간을 정지합니다.

    }



    public void StartGame()
    {
        // 랜덤 타겟 색상 선택
        targetColorInfo = colorTable.GetRandomTargetColor();
        if (targetColorInfo != null) // Null 체크
        {
            // 타겟 색상의 조합을 찾기 위해 colorNo 분리
            string targetColorNo = targetColorInfo.ColorNo; // 예를 들어 "0104"
            List<ColorBasicInfo> requiredColors = new List<ColorBasicInfo>();

            // 두 자리씩 분리하여 기본 색상 조합 찾기
            for (int i = 0; i < targetColorNo.Length; i += 2)
            {
                string basicColorNo = targetColorNo.Substring(i, 2);
                ColorBasicInfo basicColorInfo = colorTable.GetBasicColor(basicColorNo);
                if (basicColorInfo != null)
                {
                    requiredColors.Add(basicColorInfo);
                }
            }

            // UIManager에 필요한 기본 색상들 표시 요청
            uiManager.DisplayRequiredColors(requiredColors);
        }
        else
        {
            // 오류 처리: 타겟 색상이 없음
        }
        // UI에 타겟 색상 표시
        uiManager.SetTargetColor(targetColorInfo.ColorValue);

        timerManager.StartTimer(elapseTime); // 타이머 시작
        
        // 매칭 성공 인디케이터 비활성화
        uiManager.HideTargetColorMatchedIndicator();
    }

    public void EndGame()
    {
        Debug.Log($"EndGame 호출 전 playerColors: {string.Join(", ", playerColor.GetCurrentColorList())}");
        
        timerManager.StopTimer(); // 타이머 중지
        
        List<string> playerColorCodes = playerColor.GetCurrentColorList();
        
        Debug.Log($"EndGame 호출 시 playerColors: {string.Join(", ", playerColorCodes)}");
        
        string playerCombinedColorNo = string.Join("", playerColorCodes.OrderBy(c => c)); // 정렬하여 하나의 문자열로 결합
        string targetColorNo = targetColorInfo.ColorNo; // 목표 색상 번호

        
        // Debug.Log($"(게임 종료) 플레이어 조합 색상: {playerCombinedColorNo}, Target color: {targetColorNo}");
        // Debug.Log($"(게임 종료) 플레이어 조합 색상: {playerCombinedColorNo}, 목표 색상: {targetColorNo}");
        
        bool success = playerCombinedColorNo.Equals(targetColorNo);

        if(success)
        {
            Debug.Log("(게임 종료) 플레이어 색상조합 성공!");
            // 성공 화면 표시
            Screen.lockCursor = false;
            winScreen.SetActive(true);
            loseScreen.SetActive(false);
        }
        else
        {
            Debug.Log("(게임 종료) 플레이어 색상조합 실패!");
            // 실패 화면 표시
            Screen.lockCursor = false;
            winScreen.SetActive(false);
            loseScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬을 리로드하여 게임 재시작
    }

    public void ReturnToGame()
    {
        TogglePause();
    }

    public void OpenOptions()
    {
        UIManager.Instance.ShowOptionMenu();
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_MainMenuScene");
    }

    public void ColorMatchCheck()
    {
        Debug.Log($"ColorMatchCheck 호출 전 playerColors: {string.Join(", ", playerColor.GetCurrentColorList())}");
        
        List<string> playerColorCodes = playerColor.GetCurrentColorList();
        
        Debug.Log($"ColorMatchCheck 호출 시 playerColors: {string.Join(", ", playerColorCodes)}");
        
        string playerCombinedColorNo = string.Join("", playerColorCodes.OrderBy(c => c)); // 정렬하여 하나의 문자열로 결합
        string targetColorNo = targetColorInfo.ColorNo; // 목표 색상 번호

        Debug.Log($"플레이어 컬러 코드!!!!!!!!!!!!!!: {string.Join(", ", playerColorCodes)}");
        
        
        Debug.Log($"플레이어 조합 색상: {playerCombinedColorNo}, 목표 색상: {targetColorNo}");
        

        bool success = playerCombinedColorNo.Equals(targetColorNo);

        if(success)
        {
            Debug.Log("컬러 매치 성공!!");
            
            colorMatchCheck.SetActive(true);
        }
    }
    
}


