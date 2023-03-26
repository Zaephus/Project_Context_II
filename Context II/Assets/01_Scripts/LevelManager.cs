using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public System.Action<bool> LevelFinished;

    private PlacementManager placementManager;
    
    private LevelLoader levelLoader;
    private LevelGenerator levelGenerator;

    [SerializeField, Range(0, 10)]
    private float approvalThreshold;

    [SerializeField]
    private GameObject endTurnButton;

    [SerializeField]
    private GameObject dialogueBubblePrefab;

    [SerializeField]
    private TextAsset level;

    [SerializeField]
    private CameraMovement cameraMovement;

    private float score;

    private float dialogueAmountFinished;

    #region CEO Dialogue
    [Header("CEO Dialogue")]
    private DialogueOption currentDialogueCEO;

    [SerializeField, Range(0, 5), Tooltip("How many people need to be talked to, to trigger the \"Before Meeting CEO\" Dialogue")]
    private int beforeMeetingDialogueThreshold;

    [SerializeField]
    private GameObject phoneIcon;

    #endregion

    public void OnStart(GameState _state) {

        placementManager = GetComponent<PlacementManager>();
        placementManager.OnStart();
        placementManager.WindmillTargetReached += WindmillTargetReached;
        placementManager.WindmillPlaced += SetScore;

        endTurnButton.SetActive(false);
        placementManager.placingToggle.transform.parent.gameObject.SetActive(true);

        DialogueOption.OnDialogueEnded += IncrementDialogueFinishedAmount;

        dialogueAmountFinished = 0;

        CameraMovement.CameraReset?.Invoke();

        if(_state == GameState.PreMeeting) {
            ResetLevel();

            currentDialogueCEO = DialogueDatabase.Instance.startCEO;
            phoneIcon.SetActive(true);

            cameraMovement.mainCamera.orthographicSize = cameraMovement.minTransitionValue;
            cameraMovement.OnStart();
        }
        else if(_state == GameState.PostMeeting) {
            currentDialogueCEO = DialogueDatabase.Instance.afterMeetingCEO;
            phoneIcon.SetActive(true);
        }

    }

    public void OnUpdate() {
        placementManager.OnUpdate();
    }

    public void ClearLevel() {
        ResetLevel();
        if(placementManager.isPlacing) {
            placementManager.placingToggle.isOn = !placementManager.placingToggle.isOn;
        }
    }

    public void EndLevel() {
        if(score > approvalThreshold) {
            LevelFinished?.Invoke(true);
        }
        else {
            LevelFinished?.Invoke(false);
        }
    }

    public void StartCEODialogue() {
        placementManager.ShowDialogue(currentDialogueCEO);
        phoneIcon.gameObject.SetActive(false);
    }

    private void IncrementDialogueFinishedAmount() {
        dialogueAmountFinished++;
        if(dialogueAmountFinished == beforeMeetingDialogueThreshold) {
            currentDialogueCEO = DialogueDatabase.Instance.beforeMeetingCEO;
            phoneIcon.gameObject.SetActive(true);
            DialogueOption.OnDialogueEnded -= IncrementDialogueFinishedAmount;
        }
    }

    private void WindmillTargetReached() {
        endTurnButton.SetActive(true);
        placementManager.placingToggle.transform.parent.gameObject.SetActive(false);

        placementManager.WindmillTargetReached -= WindmillTargetReached;
        placementManager.WindmillPlaced -= SetScore;
    }

    private void SetScore(float _add) {
        score += _add;
    }

    private void ResetLevel() {

        levelLoader = GetComponent<LevelLoader>();
        levelGenerator = GetComponent<LevelGenerator>();

        score = 0;

        for(int i = GameManager.Instance.tiles.Count-1; i >= 0; i--) {
            Destroy(GameManager.Instance.tiles[i].gameObject);
        }
        GameManager.Instance.tiles = levelLoader.Generate(level, levelGenerator);

        for(int i = 0; i < GameManager.Instance.tiles.Count; i++) {
            if(GameManager.Instance.tiles[i].dialogueIndex == 0) {
                continue;
            }
            else {
                Vector3 indicatorPos = GameManager.Instance.tiles[i].transform.position;
                GameObject indicator = Instantiate(dialogueBubblePrefab, indicatorPos, dialogueBubblePrefab.transform.rotation, GameManager.Instance.tiles[i].transform);
                indicator.GetComponentInChildren<DialogueBubble>().sprite = DialogueDatabase.Instance.beforeMeetingOptions[GameManager.Instance.tiles[i].dialogueIndex-1].characterSprite;
            }
        }

    }

}