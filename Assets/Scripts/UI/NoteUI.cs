using UnityEngine;
using TMPro;

public class NoteUI : MonoBehaviour
{
    public TextMeshProUGUI noteText; // ���� ���� �ؽ�Ʈ

    public void ShowNote(string content)
    {
        noteText.text = content; // �ؽ�Ʈ ����
        gameObject.SetActive(true); // �г� Ȱ��ȭ
    }

    public void HideNote()
    {
        gameObject.SetActive(false); // �г� ��Ȱ��ȭ
    }
}
