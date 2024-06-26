using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public static bool isGamePause = false;
    public static bool isInvenOn = false;
    public GameObject pausePanel;
    public GameObject settingPanel;
    public GameObject inventoryPanel;
    public GameObject pauseButton;
    public GameObject deathPanel; // Death 패널 오브젝트
    public Button respawnButton; // 리스폰 버튼
    public Button mainMenuButton; // 메인메뉴 버튼
    public Slider bgmSlider;
    public Slider effectSlider;
    public AudioSource bgmSource;
    public AudioSource[] effectSources;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;
    GameData gameData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGameData();
        InitializeResolutions();
        InitializeVolumeSettings();
        AddSliderListeners();
        AddButtonListeners(); // 버튼 리스너 추가
    }

    void InitializeGameData()
    {
        // 게임 데이터를 JSON 파일에서 불러오기
        gameData = DataManager.LoadGameData();
    }

    void InitializeResolutions()
    {
        // 해상도 설정 초기화
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // 현재 화면 해상도와 일치하는 해상도를 찾기
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        // JSON 데이터에서 저장된 해상도 인덱스를 불러오기
        resolutionDropdown.value = gameData.settings.resolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void InitializeVolumeSettings()
    {
        // JSON 데이터에서 저장된 볼륨 설정 불러오기
        bgmSlider.value = gameData.settings.bgmVolume;
        effectSlider.value = gameData.settings.effectVolume;

        // 초기 볼륨 설정 적용
        SetVolumeBGM(bgmSlider.value);
        SetVolumeEffect(effectSlider.value);
    }

    void AddSliderListeners()
    {
        // 슬라이더 이벤트 리스너 추가
        bgmSlider.onValueChanged.AddListener(SetVolumeBGM);
        effectSlider.onValueChanged.AddListener(SetVolumeEffect);
    }

    void AddButtonListeners()
    {
        // 버튼 이벤트 리스너 추가
        respawnButton.onClick.AddListener(RespawnPlayer);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isInvenOn)
        {
            if (isGamePause)
            {
                Resume();
                ResetOpt();
            }
            else
            {
                Pause();
            }
        }

        if (!isGamePause)
        {
            Inventory();
        }
    }

    public void Resume()
    {
        // 일시 정지 해제
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isGamePause = false;
    }

    public void Pause()
    {
        // 일시 정지
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isGamePause = true;
    }

    public void SaveGame()
    {
        Debug.Log("저장되었습니다.");
        SceneManager.LoadScene("StartMeue");
    }

    public void GameQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Settings()
    {
        settingPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResetOpt()
    {
        settingPanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void SetVolumeBGM(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetVolumeEffect(float volume)
    {
        foreach (AudioSource source in effectSources)
        {
            source.volume = volume;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ApplySettings()
    {
        // 현재 슬라이더 및 해상도 설정을 JSON 데이터에 저장
        gameData.settings.bgmVolume = bgmSlider.value;
        gameData.settings.effectVolume = effectSlider.value;
        gameData.settings.resolutionIndex = resolutionDropdown.value;
        
        // JSON 파일로 설정 저장
        DataManager.SaveGameData(gameData);

        // 해상도 변경 적용
        SetResolution(gameData.settings.resolutionIndex);

        Debug.Log("Settings applied and saved.");
    }

    void Inventory()
    {
        if (isInvenOn)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                inventoryPanel.SetActive(false);
                isInvenOn = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventoryPanel.SetActive(true);
                isInvenOn = true;
            }   
        }
    }

    public void ShowDeathPanel()
    {
        deathPanel.SetActive(true); // Death 패널 활성화
    }

    public void RespawnPlayer()
    {
        Player.instance.Respawn(); // 플레이어 리스폰
        deathPanel.SetActive(false); // Death 패널 비활성화
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // 게임 시간 재개
        SceneManager.LoadScene("StartMeue"); // 메인 씬으로 이동
    }
}
