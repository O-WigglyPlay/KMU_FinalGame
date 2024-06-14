using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NoteManager : MonoBehaviour
{
    public GameObject[] noteObjects; // �̸� ������ ���� ������Ʈ���� �����մϴ�.
    public string[] messages; // �� ������ ������ ������ �迭�Դϴ�.

    void Start()
    {
        // noteObjects�� messages �迭�� ũ�Ⱑ �������� Ȯ���մϴ�.
        if (noteObjects.Length != messages.Length)
        {
            Debug.LogError("Note objects and messages arrays must have the same length!");
            return;
        }

        // �� ���� ������Ʈ�� ������ �����մϴ�.
        for (int i = 0; i < noteObjects.Length; i++)
        {
            NoteController noteController = noteObjects[i].GetComponent<NoteController>();
            noteController.SetText(messages[i]);
        }
    }
}
