using UnityEngine;
using TMPro;

public class NoteController : MonoBehaviour
{
    public TextMeshProUGUI noteText; // 쪽지의 TextMeshProUGUI UI 요소를 참조합니다.

    // 텍스트 내용을 변경하는 메서드
    public void SetText(string newText)
    {
        if (noteText != null)
        {
            noteText.text = newText;
        }
    }
}