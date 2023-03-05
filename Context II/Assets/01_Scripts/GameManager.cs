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
    
    private GameState State {
        get {
            return state;
        }
        set {
            HandleGameStateChanges(value);
            state = value;
        }
    }
    [SerializeField]
    private GameState state = GameState.MainMenu;
    
    [HideInInspector]
    public List<Tile> tiles = new List<Tile>();

    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject endingsContainer;

    [SerializeField]
    private GameObject goodEnding;
    [SerializeField]
    private GameObject badEnding;

    private void Start() {
        mainMenu.SetActive(true);
    }

    private void Update() {
        if(State == GameState.StageOne || State == GameState.StageTwo) {
            levelManager.OnUpdate();
        }
    }

    public void StartGame() {
        State = GameState.StageOne;
    }

    public void ContinueGame() {
        if(State == GameState.IntermediateEnding) {
            State = GameState.StageTwo;
        }
        else if(State == GameState.FinalEnding) {
            State = GameState.MainMenu;
        }
    }

    private void EndLevel(bool _goodEnding) {

        if(_goodEnding) {
            goodEnding.SetActive(true);
            badEnding.SetActive(false);
        }
        else {
            goodEnding.SetActive(false);
            badEnding.SetActive(true);
        }

        if(State == GameState.StageOne) {
            State = GameState.IntermediateEnding;
        }
        else if(State == GameState.StageTwo) {
            State = GameState.FinalEnding;
        }
        
    }

    private void HandleGameStateChanges(GameState _state) {

        switch(_state) {
            case GameState.MainMenu:

                mainMenu.SetActive(true);
                levelManager.gameObject.SetActive(false);
                endingsContainer.SetActive(false);

                break;

            case GameState.StageOne:

                levelManager.OnStart(GameState.StageOne);
                levelManager.LevelFinished += EndLevel;

                levelManager.gameObject.SetActive(true);
                mainMenu.SetActive(false);
                endingsContainer.SetActive(false);

                break;

            case GameState.StageTwo:

                levelManager.OnStart(GameState.StageTwo);
                levelManager.LevelFinished += EndLevel;

                levelManager.gameObject.SetActive(true);
                mainMenu.SetActive(false);
                endingsContainer.SetActive(false);

                break;

            case GameState.IntermediateEnding:

                levelManager.LevelFinished -= EndLevel;

                endingsContainer.SetActive(true);
                levelManager.gameObject.SetActive(false);
                break;

            case GameState.FinalEnding:

                levelManager.LevelFinished -= EndLevel;

                endingsContainer.SetActive(true);
                levelManager.gameObject.SetActive(false);

                break;

        }

    }

}