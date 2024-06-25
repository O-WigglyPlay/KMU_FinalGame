using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenWithStoryTMP : MonoBehaviour
{
    public TextMeshProUGUI storyText; // 스토리 텍스트를 표시할 TMP 텍스트
    public string[] storyMessages; // 순차적으로 표시할 스토리 메시지 배열
    public Button startGameButton; // 게임 씬으로 넘어가는 버튼
    private int currentMessageIndex = 0; // 현재 표시 중인 메시지의 인덱스
    private string pressAnyKeyMessage = "";
    private float typingSpeed = 0.05f; // 타이핑 속도

    void Start()
    {
        startGameButton.gameObject.SetActive(false); // 시작할 때 버튼을 비활성화
        StartCoroutine(LoadAsyncScene());
        DisplayStory();
        StartCoroutine(AutoNextMessage());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StopCoroutine(AutoNextMessage()); // 자동 전환 코루틴 중지
            DisplayNextMessage();
            StartCoroutine(AutoNextMessage()); // 자동 전환 코루틴 재시작
        }
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void DisplayStory()
    {
        if (storyMessages.Length > 0)
        {
            StopAllCoroutines();
            StartCoroutine(TypeText(storyMessages[currentMessageIndex] + pressAnyKeyMessage));
        }
    }

    void DisplayNextMessage()
    {
        currentMessageIndex++;
        if (currentMessageIndex < storyMessages.Length)
        {
            StopAllCoroutines();
            StartCoroutine(TypeText(storyMessages[currentMessageIndex] + pressAnyKeyMessage));
        }
        else
        {
            startGameButton.gameObject.SetActive(true); // 모든 메시지가 출력되면 버튼 활성화
        }
    }

    IEnumerator AutoNextMessage()
    {
        while (currentMessageIndex < storyMessages.Length)
        {
            yield return new WaitForSeconds(3.0f); // 3초 대기
            DisplayNextMessage();
        }
    }

    IEnumerator TypeText(string message)
    {
        storyText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            storyText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void StartGame()
    {
        StartCoroutine(ActivateScene());
    }

    IEnumerator ActivateScene()
    {
        yield return new WaitForSeconds(0.5f); // 약간의 지연을 추가하여 키 입력 누락 방지
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        asyncLoad.allowSceneActivation = true;
    }
}
