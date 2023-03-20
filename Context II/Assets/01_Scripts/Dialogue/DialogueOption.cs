using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Option", menuName = "Scriptable Objects/Dialogue Option")]
public class DialogueOption : ScriptableObject {

    public string characterName;
    public Sprite characterSprite;

    [TextArea(15,20)]
    public string dialogue;

    public DialogueOption nextDialogue;
    
}