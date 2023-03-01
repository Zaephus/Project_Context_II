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
    private TextAsset level;

    private float score;

    public void OnStart() {
        placementManager = GetComponent<PlacementManager>();
        placementManager.OnStart();
        placementManager.WindmillTargetReached += WindmillTargetReached;
        placementManager.WindmillPlaced += SetScore;

        endTurnButton.SetActive(false);
        placingToggle.SetActive(true);

        score = 0;

        levelLoader = GetComponent<LevelLoader>();
        levelGenerator = GetComponent<LevelGenerator>();

        GameManager.Instance.tiles = levelLoader.Generate(level, levelGenerator);
    }

    public void OnUpdate() {
        placementManager.OnUpdate();
    }

    public void EndLevel() {
        if(score < approvalThreshold) {
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