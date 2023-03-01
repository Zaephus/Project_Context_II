using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager Instance {get; private set;}

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
    #endregion
    
    private enum GameState {
        MainMenu = 0,
        StageOne = 1,
        StageTwo = 2,
        Ending = 3
    }

    private GameState State {
        get {
            return state;
        }
        set {
            state = value;
        }
    }
    [SerializeField]
    private GameState state;
    
    [HideInInspector]
    public List<Tile> tiles = new List<Tile>();

    [SerializeField]
    private GameObject goodEnding;
    [SerializeField]
    private GameObject badEnding;

    [SerializeField]
    private LevelManager levelManager;

    private void Start() {
        levelManager.OnStart();
        levelManager.LevelFinished += EndLevel;
    }

    private void Update() {
        levelManager.OnUpdate();
    }

    private void EndLevel(bool _ending) {
        if(_ending) {
            goodEnding.SetActive(true);
        }
        else {
            badEnding.SetActive(true);
        }
        levelManager.LevelFinished -= EndLevel;
    }

}