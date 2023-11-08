using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private TimerManager timerManager;
    [SerializeField] private float elapseTime;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private ColorTable colorTable;
    [SerializeField] private Image targetColorImage;
    // [SerializeField] private TextMeshProUGUI resultText;
    private ColorBasicInfo targetColorInfo;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    
    public static GameManager Instance { get; private set; }
    
    public TextMeshProUGUI timeText; // UI에 시간을 표시할 Text 컴포넌트 참조
    public GameObject gameOverScreen; // 게임 오버 화면의 참조

    private bool isPaused = false;


    private void Awake()
    {
        // 싱글턴 패턴 GameManager 인스턴스를
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 파괴안됨
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

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
        
        SelectAndDisplayTargetColor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
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
    

    public void StartGame()
    {
        timerManager.StartTimer(elapseTime); // 타이머 시작
    }

    public void EndGame()
    {
        timerManager.StopTimer(); // 타이머 중지
        gameOverScreen.SetActive(true); // 게임 오버 화면 표시
        
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
        SceneManager.LoadScene("MainMenuScene");
    }

    private void SelectAndDisplayTargetColor()
    {
        targetColorInfo = colorTable.GetRandomTargetColor();
        if (targetColorInfo != null)
        {
            targetColorImage.color = targetColorInfo.ColorValue;
        }
    }

}


