using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NoteManager : MonoBehaviour
{
    public GameObject[] noteObjects; // 미리 생성된 쪽지 오브젝트들을 참조합니다.
    public string[] messages; // 각 쪽지의 내용을 설정할 배열입니다.

    void Start()
    {
        // noteObjects와 messages 배열의 크기가 동일한지 확인합니다.
        if (noteObjects.Length != messages.Length)
        {
            Debug.LogError("Note objects and messages arrays must have the same length!");
            return;
        }

        // 각 쪽지 오브젝트의 내용을 설정합니다.
        for (int i = 0; i < noteObjects.Length; i++)
        {
            NoteController noteController = noteObjects[i].GetComponent<NoteController>();
            noteController.SetText(messages[i]);
        }
    }
}
