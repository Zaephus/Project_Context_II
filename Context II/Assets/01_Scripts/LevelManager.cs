using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject placingToggle;

    [SerializeField]
    private GameObject dialogueBubblePrefab;
    [SerializeField]
    private GameObject dialogueBubbleContainer;

    [SerializeField]
    private TextAsset level;

    private float score;

    public void OnStart(GameState _state) {

        placementManager = GetComponent<PlacementManager>();
        placementManager.OnStart();
        placementManager.WindmillTargetReached += WindmillTargetReached;
        placementManager.WindmillPlaced += SetScore;

        endTurnButton.SetActive(false);
        placingToggle.SetActive(true);

        score = 0;

        levelLoader = GetComponent<LevelLoader>();
        levelGenerator = GetComponent<LevelGenerator>();

        if(_state == GameState.StageOne) {
            GameManager.Instance.tiles = levelLoader.Generate(level, levelGenerator);
        }
        else if(_state == GameState.StageTwo) {

            for(int i = GameManager.Instance.tiles.Count-1; i >= 0; i--) {
                Destroy(GameManager.Instance.tiles[i].gameObject);
            }
            GameManager.Instance.tiles = levelLoader.Generate(level, levelGenerator);

            for(int i = 0; i < GameManager.Instance.tiles.Count; i++) {
                if(GameManager.Instance.tiles[i].dialogueIndex == 0) {
                    continue;
                }
                else {
                    Vector3 indicatorPos = GameManager.Instance.tiles[i].transform.position + new Vector3(0, 0.2f, 0);
                    GameObject indicator = Instantiate(dialogueBubblePrefab, indicatorPos, dialogueBubblePrefab.transform.rotation, dialogueBubbleContainer.transform);
                }
            }

        }        

    }

    public void OnUpdate() {
        placementManager.OnUpdate();
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
        placingToggle.SetActive(false);
        placementManager.WindmillTargetReached -= WindmillTargetReached;
        placementManager.WindmillPlaced -= SetScore;
    }

    private void SetScore(float _add) {
        score += _add;
    }

}