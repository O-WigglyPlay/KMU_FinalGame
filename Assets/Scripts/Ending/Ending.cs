using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public float typingSpeed = 0.05f;
    public Camerashak cameraShake; // CameraShake 스크립트 참조

    private string[] texts = {
        "실험 기록: N01 - 1\n인류는 더 이상 바이러스를 이길 수 없다. 그렇기에 우리는 신인류가 되어야 한다.",
        "실험 기록: N01 - 7\n우리는 죽지 않는다. 항상 건강한 신체를 유지하며, 구인류보다 월등한 신체능력을 가진다.",
        "실험 기록: N02 - 2\n이런... 실패작들이 나오기 시작했다. 그들은 신인류의 아류이다. 죽지 않지만 지능이 없으며, 굶주려있다.",
        "실험 기록: N02 - 6\n실패작들이 너무 많아져 모두 한 곳에 폐기처리 하였다. 새롭게 다시 시작해야겠다. 우리들의 진화는 계속된다.",
        "실험 기록: N04 - 4\n끝이 보인다. 속도를 더 붙여야겠다. 더 많은 실험체가 필요하다.",
        "실험 기록: N04 - 8\n신인류가 탄생되었다. 이제 우리의 시대가 열렸다.",
        "실험 기록: N05 - 9\n실험 실패. 실험체들이 폭주하여 이곳을 달아났다. 실험실을 파괴하고 떠나야 한다."
    };

    void Start()
    {
        StartCoroutine(DisplayTexts());
    }

    IEnumerator DisplayTexts()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            yield return StartCoroutine(TypeText(texts[i], i == 2));
            yield return new WaitForSeconds(2f);  // Pause between texts
        }
        SceneManager.LoadScene("Startmenu");
    }

    IEnumerator TypeText(string text, bool triggerShake = false)
    {
        displayText.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            displayText.text += text[i];
            if (triggerShake && text.Substring(0, i + 1).EndsWith("이런..."))
            {
                if (cameraShake != null)
                {
                    cameraShake.TriggerShake();
                }
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
