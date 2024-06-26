using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public float typingSpeed = 0.05f;
    public Camerashak cameraShake; // CameraShake ��ũ��Ʈ ����

    private string[] texts = {
        "���� ���: N01 - 1\n�η��� �� �̻� ���̷����� �̱� �� ����. �׷��⿡ �츮�� ���η��� �Ǿ�� �Ѵ�.",
        "���� ���: N01 - 7\n�츮�� ���� �ʴ´�. �׻� �ǰ��� ��ü�� �����ϸ�, ���η����� ������ ��ü�ɷ��� ������.",
        "���� ���: N02 - 2\n�̷�... �����۵��� ������ �����ߴ�. �׵��� ���η��� �Ʒ��̴�. ���� ������ ������ ������, ���ַ��ִ�.",
        "���� ���: N02 - 6\n�����۵��� �ʹ� ������ ��� �� ���� ���ó�� �Ͽ���. ���Ӱ� �ٽ� �����ؾ߰ڴ�. �츮���� ��ȭ�� ��ӵȴ�.",
        "���� ���: N04 - 4\n���� ���δ�. �ӵ��� �� �ٿ��߰ڴ�. �� ���� ����ü�� �ʿ��ϴ�.",
        "���� ���: N04 - 8\n���η��� ź���Ǿ���. ���� �츮�� �ô밡 ���ȴ�.",
        "���� ���: N05 - 9\n���� ����. ����ü���� �����Ͽ� �̰��� �޾Ƴ���. ������� �ı��ϰ� ������ �Ѵ�."
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
            if (triggerShake && text.Substring(0, i + 1).EndsWith("�̷�..."))
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
