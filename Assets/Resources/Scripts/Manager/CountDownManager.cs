using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CountDownManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private AudioSource countdownAudio;
    [SerializeField] private AudioClip countdownClip;
    [SerializeField] private GameManager gameManager;

    public void CountDownStart()
    {
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 0f;
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        
        countdownText.gameObject.SetActive(true);
        countdownText.text = "3";
        countdownAudio.PlayOneShot(countdownClip);
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "2";
        // countdownAudio.PlayOneShot(countdownClip);
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "1";
        // countdownAudio.PlayOneShot(countdownClip);
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "GO!";
        // 여기서 게임 시작 로직을 추가하세요.

        yield return new WaitForSecondsRealtime(1f);

        countdownText.gameObject.SetActive(false);
        gameManager.StartGameAfterCountdown();// 카운트다운 텍스트를 숨깁니다.
    }
}
