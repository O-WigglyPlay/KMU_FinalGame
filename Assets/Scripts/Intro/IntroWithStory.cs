using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class IntroWithStoryTMP : MonoBehaviour
{
    public TextMeshProUGUI storyText; // ���丮 �ؽ�Ʈ�� ǥ���� TMP �ؽ�Ʈ
    public string[] storyMessages; // ���������� ǥ���� ���丮 �޽��� �迭
    public Button startGameButton; // ���� ������ �Ѿ�� ��ư
    private int currentMessageIndex = 0; // ���� ǥ�� ���� �޽����� �ε���
    private string pressAnyKeyMessage = "";
    private float typingSpeed = 0.05f; // Ÿ���� �ӵ�

    void Start()
    {
        startGameButton.gameObject.SetActive(false); // ������ �� ��ư�� ��Ȱ��ȭ
        StartCoroutine(LoadAsyncScene());
        DisplayStory();
        StartCoroutine(AutoNextMessage());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StopCoroutine(AutoNextMessage()); // �ڵ� ��ȯ �ڷ�ƾ ����
            DisplayNextMessage();
            StartCoroutine(AutoNextMessage()); // �ڵ� ��ȯ �ڷ�ƾ �����
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
            startGameButton.gameObject.SetActive(true); // ��� �޽����� ��µǸ� ��ư Ȱ��ȭ
        }
    }

    IEnumerator AutoNextMessage()
    {
        while (currentMessageIndex < storyMessages.Length)
        {
            yield return new WaitForSeconds(3.0f); // 3�� ���
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
        yield return new WaitForSeconds(0.5f); // �ణ�� ������ �߰��Ͽ� Ű �Է� ���� ����
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        asyncLoad.allowSceneActivation = true;
    }
}
