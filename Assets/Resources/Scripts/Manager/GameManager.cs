using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public enum EColorMatchStatus { MIX_ING, MIX_COMPLETE, MIX_FAIL }

public class GameManager : MonoBehaviour
{
    [SerializeField] public copymotion[] copy;
    [SerializeField] private TimerManager timerManager;
    [SerializeField] private float elapseTime;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CountDownManager countDownManager;

    [SerializeField] private ColorTable colorTable;
    [SerializeField] private Image targetColorImage;
    // [SerializeField] private TextMeshProUGUI resultText;
    
    [SerializeField] private PlayerColor playerColor;
    private ColorBasicInfo targetColorInfo;
    private NPCColor npcColor;
    private EnemyAI enemyAI;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject colorMatchCheck;

    [SerializeField] SoundManager soundManager;
    
    private bool isColorMatchSuccess = false;

    private EColorMatchStatus colorMatchStatus = EColorMatchStatus.MIX_ING;



    public EColorMatchStatus CurrentColorMatchStatus
    {
        get { return colorMatchStatus; }
    }
    
    public bool IsColorMatchSuccess
    {
        get { return isColorMatchSuccess; }
    }
    
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
    }

    private void Start()
    {
        copy = FindObjectsOfType<copymotion>();

        if (countDownManager != null)
        {
            countDownManager.CountDownStart(); // 카운트다운 시작
        }
        else
        {
            // Debug.LogError("카운트다운 매니저 등록 안되어있음!!");
        }

        timerManager.Init(elapseTime);
        Time.timeScale = 0f;
        
        if (timerManager == null)
        {
            // Debug.LogError("TimerManager is not assigned in the inspector!");
        }
        else
        {
            StartGame();
        }
        TestInit();
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
    
    public void StartGameAfterCountdown()
    {
        Time.timeScale = 1f;
       
        StartGame();
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 시간을 정지합니다.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            uiManager.ShowPauseMenu(); // 일시 정지 메뉴를 표시합니다.
            soundManager.audioSourceBgm.Pause();
        }
        else
        {
            Time.timeScale = 1f; // 게임 시간을 재개합니다.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            uiManager.HidePauseMenu(); // 일시 정지 메뉴를 숨깁니다.
            soundManager.audioSourceBgm.UnPause();
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
        // Time.timeScale = 1;
    }
    
    public void EndGame()
    {
        // Debug.Log($"EndGame 호출 전 playerColors: {string.Join(", ", playerColor.GetCurrentColorList())}");

        timerManager.StopTimer(); // 타이머 중지
        
        List<string> playerColorCodes = playerColor.GetCurrentColorList();

        // Debug.Log($"EndGame 호출 시 playerColors: {string.Join(", ", playerColorCodes)}");

        string playerCombinedColorNo = string.Join("", playerColorCodes.OrderBy(c => c)); // 정렬하여 하나의 문자열로 결합
        string targetColorNo = targetColorInfo.ColorNo; // 목표 색상 번호
    
        bool success = playerCombinedColorNo.Equals(targetColorNo);
    
        if(success)
        {
            // Debug.Log("(게임 종료) 플레이어 색상조합 성공!");
            // 성공 화면 표시
            // Screen.lockCursor = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            winScreen.SetActive(true);
            loseScreen.SetActive(false);
        }
        else
        {
            // Debug.Log("(게임 종료) 플레이어 색상조합 실패!");
            // 실패 화면 표시
            // Screen.lockCursor = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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

    
    // 컬러매칭중, 매칭실패, 매칭성공 상태 검사
    public EColorMatchStatus GetCurrentColorMatchStatus()
    {
        List<string> playerColorCodes = playerColor.GetCurrentColorList();
        string playerCombinedColorNo = string.Join("", playerColorCodes.OrderBy(c => c));
        string targetColorNo = targetColorInfo.ColorNo;

        // 성공적으로 목표 색상을 달성한 경우
        if (playerCombinedColorNo.Equals(targetColorNo))
        {
            return EColorMatchStatus.MIX_COMPLETE;
        }

        // 플레이어 색상 조합이 목표 색상보다 길어진 경우 (실패)
        if (playerCombinedColorNo.Length > targetColorNo.Length)
        {
            return EColorMatchStatus.MIX_FAIL;
        }

        // 목표 색상을 위해 필요한 색상들 생성
        HashSet<string> requiredColors = new HashSet<string>();
        for (int i = 0; i < targetColorNo.Length; i += 2)
        {
            requiredColors.Add(targetColorNo.Substring(i, 2));
        }

        // 플레이어가 습득한 색상 중 목표 색상에 필요하지 않은 색상이 있는지 확인
        foreach (string color in playerColorCodes)
        {
            if (!requiredColors.Contains(color))
            {
                return EColorMatchStatus.MIX_FAIL;
            }
        }
        
        // 아직 목표 색상을 달성하지 못한 경우
        return EColorMatchStatus.MIX_ING;
    }
    
    
    public void ColorMatchCheck()
    {
        List<string> playerColorCodes = playerColor.GetCurrentColorList();
        string playerCombinedColorNo = string.Join("", playerColorCodes.OrderBy(c => c));
        string targetColorNo = targetColorInfo.ColorNo;

        // Debug.Log($"플레이어 조합 색상: {playerCombinedColorNo}, 목표 색상: {targetColorNo}");

        bool success = playerCombinedColorNo.Equals(targetColorNo);

        if (success)
        {
            // Debug.Log("컬러 매치 성공!!");
            isColorMatchSuccess = true;
            colorMatchCheck.SetActive(true);
        }
        else
        {
            // Debug.Log("컬러 매치 실패");
            isColorMatchSuccess = false;
            colorMatchCheck.SetActive(false);
        }
    }

    private void TestInit()
    {
        foreach (var v in copy)
        {
            v.Init();
        }
    }
}


