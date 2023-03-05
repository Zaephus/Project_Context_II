using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour {

    public System.Action DialogueEnded;

    private DialogueOption dialogueOption;

    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private float delayBeforeStart;
    [SerializeField]
    private float delayBetweenChars;

    private bool dialogueCompleted;

    public void StartDialogue(int _index) {

        Debug.Log("Starting Dialogue of index: " + _index);

        dialogueOption = DialogueDatabase.Instance.dialogueOptions[_index-1];
        
        titleText.text = dialogueOption.characterName;
        image.sprite = dialogueOption.characterSprite;

        StartCoroutine(TypeWriter(dialogueOption.dialogue));

    }

    public void DialogueClicked() {
        if(!dialogueCompleted) {
            //StopAllCoroutines();
            dialogueText.text = dialogueOption.dialogue;
            dialogueCompleted = true;
        }
        else {
            DialogueEnded?.Invoke();
        }
    }

    private IEnumerator TypeWriter(string _dialogue) {

        dialogueText.text = "";

        dialogueCompleted = false;
        yield return new WaitForSeconds(delayBeforeStart);

        foreach(char c in _dialogue) {
            if(dialogueCompleted) {
                break;
            }
            dialogueText.text += c;
            yield return new WaitForSeconds(delayBetweenChars);
        }

        yield return new WaitForSeconds(0.35f);
        dialogueCompleted = true;

    }

}