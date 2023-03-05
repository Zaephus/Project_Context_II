using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour {

    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private float delayBeforeStart;
    [SerializeField]
    private float delayBetweenChars;

    private IEnumerator TypeWriter(string _dialogue) {

        dialogueText.text = "";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach(char c in _dialogue) {
            dialogueText.text += c;
            yield return new WaitForSeconds(delayBetweenChars);
        }

        yield return new WaitForSeconds(0.35f);

    }

}