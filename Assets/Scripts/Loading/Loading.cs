using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading : MonoBehaviour
{
    public Slider S;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI tipText;  // 팁을 표시할 TextMeshProUGUI 추가
    bool IsDone = false;
    float T = 0f;
    AsyncOperation OP;

    // 팁 목록
    private string[] tips = new string[]
    {
        "Tip. 게임 중에는 휴식을 취하세요.",
        "Tip. 빠르게 건축을 해 나만의 안식처를 만드세요!",
        "Tip. 언제 어디서 적이 나타날지 모릅니다!"
    };

    void Start()
    {
        StartCoroutine(StartLoad("IntroScene"));
        StartCoroutine(TypeLoadingText());
        ShowRandomTip();  // 랜덤 팁 표시
    }

    void Update()
    {
        T += Time.deltaTime;
        S.value = T;

        if (T >= 5)
        {
            OP.allowSceneActivation = true;
        }
    }

    public IEnumerator StartLoad(string strSceneName)
    {
        OP = SceneManager.LoadSceneAsync(strSceneName);
        OP.allowSceneActivation = false;

        if (IsDone == false)
        {
            IsDone = true;

            while (OP.progress < 0.9f)
            {
                S.value = OP.progress;
                yield return true;
            }
        }
    }

    IEnumerator TypeLoadingText()
    {
        string originalText = "Loading";
        string typingText = "";

        foreach (char letter in originalText)
        {
            typingText += letter;
            loadingText.text = typingText + "";
            yield return new WaitForSeconds(0.1f);
        }

        while (true)
        {
            typingText += ".";
            loadingText.text = typingText;
            yield return new WaitForSeconds(0.5f);

            if (typingText.EndsWith("..."))
            {
                typingText = "Loading";
            }
        }
    }

    // 랜덤 팁을 표시하는 메소드
    void ShowRandomTip()
    {
        int tipIndex = Random.Range(0, tips.Length);
        tipText.text = tips[tipIndex];
    }
}
