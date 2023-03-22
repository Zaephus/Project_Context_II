using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue Option", menuName = "Scriptable Objects/Dialogue Option")]
public class DialogueOption : ScriptableObject {

    public static System.Action OnDialogueEnded;

    public string characterName;
    public string characterJob;
    public Sprite characterSprite;

    [TextArea(15,20)]
    public string dialogue;

    public DialogueOption nextDialogue;

    public bool invokeOnEnd;
    
}