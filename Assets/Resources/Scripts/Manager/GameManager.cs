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
    
    public TextMeshProUGUI timeText; // UI�� �ð��� ǥ���� Text ������Ʈ ����
    public GameObject gameOverScreen; // ���� ���� ȭ���� ����

    private bool isPaused = false;


    private void Awake()
    {
        // �̱��� ���� GameManager �ν��Ͻ���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ����Ǿ �ı��ȵ�
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
            
        // �̹� SerializeField�� ���� �Ҵ������Ƿ� �� ���� �ʿ� �����ϴ�.
        // timerManager = GetComponent<TimerManager>();
    
        // ���, timerManager�� �Ҵ�Ǿ����� Ȯ���մϴ�.
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
            Time.timeScale = 0f; // ���� �ð��� �����մϴ�.
            uiManager.ShowPauseMenu(); // �Ͻ� ���� �޴��� ǥ���մϴ�.
        }
        else
        {
            Time.timeScale = 1f; // ���� �ð��� �簳�մϴ�.
            uiManager.HidePauseMenu(); // �Ͻ� ���� �޴��� ����ϴ�.
        }
    }
    

    public void StartGame()
    {
        timerManager.StartTimer(elapseTime); // Ÿ�̸� ����
    }

    public void EndGame()
    {
        timerManager.StopTimer(); // Ÿ�̸� ����
        gameOverScreen.SetActive(true); // ���� ���� ȭ�� ǥ��
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� ���� ���ε��Ͽ� ���� �����
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


