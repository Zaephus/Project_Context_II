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

    [SerializeField, Range(0, 5), Tooltip("How much people need to be listened to, to exit the meeting.")]
    private int dialogueThreshold;

    private int dialogueFinished;

    [SerializeField]
    private float waitTimeAfterMeeting;

    public void OnStart() {
        dialogueFinished = 0;
        DialogueOption.OnDialogueEnded += IncrementDialogueFinishedAmount;
    }

    public void ShowDialogue(int _index) {
        meetingCanvas.SetActive(false);
        dialogueCanvas.SetActive(true);

        dialogueSystem.StartDialogue(DialogueDatabase.Instance.meetingOptions[_index]);
        DialogueSystem.DialogueEnded += DialogueEnded;
    }

    private void DialogueEnded() {
        meetingCanvas.SetActive(true);
        dialogueCanvas.SetActive(false);

        DialogueSystem.DialogueEnded -= DialogueEnded;
    }

    private void IncrementDialogueFinishedAmount() {
        dialogueFinished++;
        if(dialogueFinished >= dialogueThreshold) {
            StartCoroutine(EndMeeting());
            DialogueOption.OnDialogueEnded -= IncrementDialogueFinishedAmount;
        }
    }

    private IEnumerator EndMeeting() {
        yield return new WaitForSeconds(waitTimeAfterMeeting);
        MeetingFinished?.Invoke();
    }

}