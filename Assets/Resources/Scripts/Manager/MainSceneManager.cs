using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private MainSceneUIManager mainsceneUIManager;
    [SerializeField] private ColorTable colorTable;
        
    // [SerializeField] private PlayerColor playerColor;
    
    private bool isPaused = false;


    private void Awake()
    {

    }

    private void Start()
    {
        
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
            // mainsceneUIManager.ShowPauseMenu(); // 일시 정지 메뉴를 표시합니다.
        }
        else
        {
            Time.timeScale = 1f; // 게임 시간을 재개합니다.
            // mainsceneUIManager.HidePauseMenu(); // 일시 정지 메뉴를 숨깁니다.
        }
    }

    public void PauseGame()
    {
        isPaused = false;
        Time.timeScale = 0f; // 게임 시간을 정지합니다.

    }



    public void StartGame()
    {


    }

    public void EndGame()
    {
        
    }
    
    public void QuitGame()
    {
        
    }
    
    
    public void ReturnToGame()
    {
        TogglePause();
    }

    public void OpenOptions()
    {
        UIManager.Instance.ShowOptionMenu();
    }



}
