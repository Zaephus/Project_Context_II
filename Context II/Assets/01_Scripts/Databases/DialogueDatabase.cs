using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Database", menuName = "Scriptable Objects/Dialogue Database")]
public class DialogueDatabase : SingletonScriptableObject<DialogueDatabase> {
    public DialogueOption[] dialogueOptions;
}