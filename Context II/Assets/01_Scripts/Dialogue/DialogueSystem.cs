using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour {

    public static System.Action DialogueEnded;

    private DialogueOption dialogueOption;
    [SerializeField]
    private TMP_Text jobText;
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

        dialogueOption = DialogueDatabase.Instance.beforeMeetingOptions[_index-1];

        jobText.text = dialogueOption.characterJob;
        titleText.text = dialogueOption.characterName;
        image.sprite = dialogueOption.characterSprite;

        StartCoroutine(TypeWriter(dialogueOption.dialogue));

    }

    public void StartDialogue(DialogueOption _option) {
        dialogueOption = _option;

        jobText.text = dialogueOption.characterJob;
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
        else if(dialogueOption.nextDialogue != null) {
            StartDialogue(dialogueOption.nextDialogue);
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

        if(dialogueOption.invokeOnEnd) {
            DialogueOption.OnDialogueEnded?.Invoke();
        }

    }

}