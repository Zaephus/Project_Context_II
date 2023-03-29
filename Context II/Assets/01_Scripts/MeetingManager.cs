using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingManager : MonoBehaviour {

    public System.Action MeetingFinished;

    [SerializeField]
    private GameObject meetingCanvas;
    [SerializeField]
    private GameObject dialogueCanvas;

    [SerializeField]
    private DialogueSystem dialogueSystem;

    public void ShowDialogue(int _index) {
        meetingCanvas.SetActive(false);
        dialogueCanvas.SetActive(true);

        dialogueSystem.StartDialogue(DialogueDatabase.Instance.meetingOptions[_index]);
        DialogueSystem.DialogueEnded += DialogueEnded;
    }

    public void EndMeeting() {
        MeetingFinished?.Invoke();
    }

    private void DialogueEnded() {
        meetingCanvas.SetActive(true);
        dialogueCanvas.SetActive(false);

        DialogueSystem.DialogueEnded -= DialogueEnded;
    }

}