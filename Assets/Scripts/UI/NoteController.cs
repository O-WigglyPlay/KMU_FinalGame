using UnityEngine;
using TMPro;

public class NoteController : MonoBehaviour
{
    public TextMeshProUGUI noteText; // ������ TextMeshProUGUI UI ��Ҹ� �����մϴ�.

    // �ؽ�Ʈ ������ �����ϴ� �޼���
    public void SetText(string newText)
    {
        if (noteText != null)
        {
            noteText.text = newText;
        }
    }
}