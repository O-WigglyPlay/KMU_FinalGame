using UnityEngine;
using TMPro;

public class NoteUI : MonoBehaviour
{
    public TextMeshProUGUI noteText; // 쪽지 내용 텍스트

    public void ShowNote(string content)
    {
        noteText.text = content; // 텍스트 설정
        gameObject.SetActive(true); // 패널 활성화
    }

    public void HideNote()
    {
        gameObject.SetActive(false); // 패널 비활성화
    }
}
