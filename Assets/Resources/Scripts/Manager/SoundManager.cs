using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class Sound
{
    public string name;    // 곡 이름
    public AudioClip clip; // 곡
    [Range(0f, 1f)]
    public float volume = 1f; // 각 사운드의 볼륨. 기본값은 1 (최대 볼륨).
}
public class SoundManager : MonoBehaviour
{
    //싱글턴 씬 이동이 이뤄줘도 사운드 매니저가 파괴되지 않게
    //기존에 있던것도 씬을 이동하면 복제 되지 않게
    [Header("Scene BGMs")]
    [SerializeField] private List<string> sceneNames = new List<string>(); // 씬 이름들을 저장할 리스트
    [SerializeField] private List<AudioClip> bgmClips = new List<AudioClip>(); // BGM 클립들을 저장할 리스트

    [Header("Fade in Fade Out")]
    [SerializeField] private float fadeInTime = 1.0f; // Default is 1 second
    [SerializeField] private float fadeOutTime = 1.0f; // Default is 1 second

    private Dictionary<string, AudioClip> sceneToBGMMap = new Dictionary<string, AudioClip>(); // 씬 이름을 키로 사용하여 BGM 클립을 저장할 딕셔너리

    static public SoundManager Instance;                                    // 어디서도 접근 가능하게 
    private int[] effectSoundIndices;

    private float targetBGMVolume; // 목표 BGM 볼륨 값 저장

    #region singleton
    private void Awake() //객체 생성시 최초 실행
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);



        for (int i = 0; i < Mathf.Min(sceneNames.Count, bgmClips.Count); i++)
        {
            sceneToBGMMap.Add(sceneNames[i], bgmClips[i]);
        }

        // 최초 씬에 대한 페이드인 처리
        Scene currentScene = SceneManager.GetActiveScene();
        //StopBEWithFade(); // 현재 BGM을 페이드아웃으로 중단
        if (sceneToBGMMap.ContainsKey(currentScene.name))
        {
            PlayBE(currentScene.name); // 페이드인으로 BGM 시작
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion sington;

    public AudioSource[] audioSourceEffects;                                // 효과음
    public AudioSource audioSourceBgm;                                      // BGM

    public string[] playSoundNameTracking;                                  //효과음 추적

    public Sound[] effectSoundCountList;                                    // 만약 10개까지 등록했다면 10개만 재생
    public Sound[] bgmSounds;                                               // 배열로 브금사운드 관리

    //볼륨조절
    [Range(0f, 1f)]
    public float bgmVolume = 1f;

    [Range(0f, 1f)]
    public float sfxVolume = 1f;
    [Header("게임오버 관련 사운드")]
    public string gamaOverSound;
    [Header("주인공 관련 사운드")]
    public string PlayerWalkSound;
  
    [Header("물통적 관련 사운드")]
    public string WaterWalkSound;
   

    [Header("색깔적 관련 사운드")]
    public string ColorRunSound;
    public string ColorWalkSound;





    private void Update()
    {


        // 실시간으로 음량 체크를 위해 일시적으로 업데이트에서 음량 조정할수있게함
        //audioSourceBgm.volume = bgmVolume; // BGM 볼륨 실시간 업데이트

        //for (int i = 0; i < audioSourceEffects.Length; i++)
        //{
        //   if (effectSoundIndices[i] != -1 && effectSoundIndices[i] < effectSound.Length)
        //    {
        //        audioSourceEffects[i].volume = effectSound[effectSoundIndices[i]].volume * sfxVolume;
        //   }
        //   else
        //    {
        //        audioSourceEffects[i].volume = sfxVolume;
        //    }
        // }

    }
    private void Start()
    {
        playSoundNameTracking = new string[audioSourceEffects.Length];
        effectSoundIndices = new int[audioSourceEffects.Length];
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            effectSoundIndices[i] = -1; // 초기값을 -1로 설정합니다.
        }

    }
    public void PlaySE(string _name)                                        //사운드 이펙트 플레이 사운드 이펙트 (파라미미터값받기)
    {
        for (int i = 0; i < effectSoundCountList.Length; i++)                        //사운드 이름이 일치하는 이름이 있는지 확인
        {
            if (_name == effectSoundCountList[i].name)                                //이름 = 이름 일치하면 재생
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)                   //사운드가 재생중이라면 트루로 반환
                    {
                        playSoundNameTracking[j] = effectSoundCountList[i].name;             //재생중인 오디오소스와 이름 일치화
                        audioSourceEffects[j].clip = effectSoundCountList[i].clip;
                        effectSoundIndices[j] = i; // 인덱스 값을 기록합니다.
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 soundmanager에 등록되지 않았습니다.");
    }

    public void PlayBE(string sceneName)
    {
        if (sceneToBGMMap.TryGetValue(sceneName, out AudioClip bgm))
        {
            audioSourceBgm.clip = bgm;
            audioSourceBgm.volume = bgmVolume;
            audioSourceBgm.Play();

        }
        else
        {
            Debug.Log(sceneName + "에 대한 BGM이 SoundManager에 등록되지 않았습니다.");
        }
    }
    public void StopAllSE() //전부다 멈추기
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)                                        // 플레이사운드 == 이름과 같다면 그거만 멈추기
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundNameTracking[i] == _name && audioSourceEffects[i].isPlaying)
            {
                audioSourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생중인" + _name + "사운드가 없습니다.");
    }

    private void OnDestroy()
    {
        // 오브젝트가 파괴될 때 이벤트에서 메서드 연결 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        if (sceneToBGMMap.ContainsKey(scene.name))
        {
            Debug.Log("Playing BGM for scene: " + scene.name);
            PlayBE(scene.name); // 새로운 BGM을 페이드인으로 시작합니다.
        }
        else
        {
            Debug.Log("No BGM found for scene: " + scene.name);
            audioSourceBgm.Stop(); // 해당 씬의 BGM이 없으면 BGM을 중지
        }
    }

    public bool IsPlaying(string soundName)
    {
        foreach (var audioSource in audioSourceEffects)
        {
            if (audioSource.isPlaying && playSoundNameTracking.Contains(soundName))
            {
                return true;
            }
        }
        return false;
    }

   
}