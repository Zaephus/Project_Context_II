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

    public void OnStart() {

        placementManager = GetComponent<PlacementManager>();
        placementManager.OnStart();
        placementManager.WindmillTargetReached += WindmillTargetReached;
        placementManager.WindmillPlaced += SetScore;

        endTurnButton.SetActive(false);
        placementManager.placingToggle.transform.parent.gameObject.SetActive(true);

        score = 0;

        levelLoader = GetComponent<LevelLoader>();
        levelGenerator = GetComponent<LevelGenerator>();

        CameraMovement.CameraReset?.Invoke();

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
                indicator.GetComponentInChildren<DialogueBubble>().sprite = DialogueDatabase.Instance.dialogueOptions[GameManager.Instance.tiles[i].dialogueIndex-1].characterSprite;
            }
        }

    }

    public void OnEnable() {
        cameraMovement.mainCamera.orthographicSize = cameraMovement.minTransitionValue;
        cameraMovement.OnStart();
    }

    public void OnUpdate() {
        placementManager.OnUpdate();
    }

    public void ClearLevel() {
        OnStart();
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

    private void WindmillTargetReached() {
        endTurnButton.SetActive(true);
        placementManager.placingToggle.transform.parent.gameObject.SetActive(false);

        placementManager.WindmillTargetReached -= WindmillTargetReached;
        placementManager.WindmillPlaced -= SetScore;
    }

    private void SetScore(float _add) {
        score += _add;
    }

}