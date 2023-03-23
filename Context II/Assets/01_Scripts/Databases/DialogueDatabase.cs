using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Database", menuName = "Scriptable Objects/Dialogue Database")]
public class DialogueDatabase : SingletonScriptableObject<DialogueDatabase> {

    [Header("CEO")]
    public DialogueOption introCEO;
    public DialogueOption startCEO;
    public DialogueOption beforeMeetingCEO;
    public DialogueOption afterMeetingCEO;
    public DialogueOption goodEndingCEO;
    public DialogueOption badEndingCEO;

    [Header("Civilians - Before Meeting")]
    public DialogueOption[] beforeMeetingOptions;

    [Header("Civilians - Meeting")]
    public DialogueOption[] meetingOptions;

    [Header("Civilians - After Meeting")]
    public DialogueOption[] afterMeetingOptions;

    [Header("No Need To Touch This")]
    public DialogueOption[] currentDialogueOptions;

}