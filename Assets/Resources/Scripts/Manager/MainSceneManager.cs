using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; // 메인 메뉴 UI
    [SerializeField] private GameObject optionMenu; // 옵션 메뉴 UI
    [SerializeField] private GameObject exitGameMenu;
    [SerializeField] private GameObject graphicOptionMenu;
    [SerializeField] private GameObject soundOptionMenu;
    [SerializeField] private GameObject backToMainMenuButton;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
        backToMainMenuButton.SetActive(false);
        soundOptionMenu.SetActive(false);
        graphicOptionMenu.SetActive(false);
        exitGameMenu.SetActive(false);
    }

    // 메인 메뉴를 다시 표시하고 옵션 메뉴를 숨기는 메소드
    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
        backToMainMenuButton.SetActive(false);
        soundOptionMenu.SetActive(false);
        graphicOptionMenu.SetActive(false);
        exitGameMenu.SetActive(false);
    }

    public void StartGame()
    {
        // DestroyUnnecessaryObjects();
        SceneManager.LoadScene("_GameScene");
    }

    public void ShowOptions()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
        backToMainMenuButton.SetActive(true);
        soundOptionMenu.SetActive(true);
        graphicOptionMenu.SetActive(true);
    }


    public void ShowGraphicOption()
    {
        graphicOptionMenu.SetActive(true);
        backToMainMenuButton.SetActive(true);
        soundOptionMenu.SetActive(false);
    }

    public void ShowSoundOption()
    {
        soundOptionMenu.SetActive(true);
        backToMainMenuButton.SetActive(true);
        graphicOptionMenu.SetActive(false);
    }
    
    // 게임 종료 버튼 누름
    public void ExitGame()
    {
        mainMenu.SetActive(false);
        exitGameMenu.SetActive(true);
    }

    // 게임을 종료하시겠습니까? - NO 게임 돌아가기
    public void ExitGameNo()
    {
        mainMenu.SetActive(true);
    }
    
    // 게임을 종료하시겠습니까? - YES 바탕화면 가기
    public void ExitGameYes()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    void DestroyUnnecessaryObjects()
    {
        // 예: 특정 태그를 가진 모든 오브젝트를 찾아 파괴
        foreach (var obj in GameObject.FindGameObjectsWithTag("DestroyOnLoad"))
        {
            Destroy(obj);
        }

        // 또는 DontDestroyOnLoad로 설정된 오브젝트를 파괴
        // 이러한 오브젝트는 별도의 관리가 필요
    }
}
