using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private Note currentNote;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentNote != null)
            {
                ToggleNoteUI(currentNote);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Note note = other.GetComponent<Note>();
        if (note != null)
        {
            currentNote = note;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Note note = other.GetComponent<Note>();
        if (note != null && note == currentNote)
        {
            currentNote = null;
        }
    }

    void ToggleNoteUI(Note note)
    {
        bool isActive = note.noteUIPanel.activeSelf;
        note.noteUIPanel.SetActive(!isActive);
    }
}
