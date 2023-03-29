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
    
    public GameState State {
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
    private MeetingManager meetingManager;

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject endingsContainer;

    [SerializeField]
    private DialogueSystem endingDialogueSystem;

    [SerializeField]
    private GameObject goodEnding;
    [SerializeField]
    private GameObject badEnding;

    private void Start() {
        mainMenu.SetActive(true);
    }

    private void Update() {
        if(State == GameState.PreMeeting || State == GameState.PostMeeting) {
            levelManager.OnUpdate();
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            QuitGame();
        }
    }

    public void StartGame() {
        State = GameState.PreMeeting;
    }

    public void ContinueGame() {
        if(State == GameState.Meeting) {
            State = GameState.PostMeeting;
            meetingManager.MeetingFinished -= ContinueGame;
        }
        else if(State == GameState.Ending) {
            State = GameState.MainMenu;
        }
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void SubmitLevel(bool _goodEnding) {

        if(State == GameState.PreMeeting) {
            State = GameState.Meeting;
        }
        else if(State == GameState.PostMeeting) {
            State = GameState.Outro;
            if(_goodEnding) {
                goodEnding.SetActive(true);
                badEnding.SetActive(false);
                ShowEndDialogue(DialogueDatabase.Instance.goodEndingCEO);
            }
            else {
                goodEnding.SetActive(false);
                badEnding.SetActive(true);
                ShowEndDialogue(DialogueDatabase.Instance.badEndingCEO);
            }
        }
        
    }

    private void ShowEndDialogue(DialogueOption _option) {
        endingDialogueSystem.gameObject.SetActive(true);
        endingDialogueSystem.StartDialogue(_option);

        DialogueSystem.DialogueEnded += OnDialogueEnded;
    }

    private void OnDialogueEnded() {
        endingDialogueSystem.gameObject.SetActive(false);
        DialogueSystem.DialogueEnded -= OnDialogueEnded;
        State = GameState.Ending;
    }

    private void HandleGameStateChanges(GameState _state) {

        switch(_state) {
            case GameState.MainMenu:

                mainMenu.SetActive(true);
                levelManager.gameObject.SetActive(false);
                meetingManager.gameObject.SetActive(false);
                endingsContainer.SetActive(false);

                break;

            case GameState.PreMeeting:

                levelManager.gameObject.SetActive(true);
                meetingManager.gameObject.SetActive(false);
                mainMenu.SetActive(false);
                endingsContainer.SetActive(false);

                levelManager.OnStart(GameState.PreMeeting);
                levelManager.LevelFinished += SubmitLevel;

                DialogueDatabase.Instance.currentDialogueOptions = DialogueDatabase.Instance.beforeMeetingOptions;

                break;

            case GameState.PostMeeting:

                levelManager.gameObject.SetActive(true);
                meetingManager.gameObject.SetActive(false);
                mainMenu.SetActive(false);
                endingsContainer.SetActive(false);

                levelManager.OnStart(GameState.PostMeeting);

                DialogueDatabase.Instance.currentDialogueOptions = DialogueDatabase.Instance.afterMeetingOptions;

                break;

            case GameState.Meeting:
            
                meetingManager.MeetingFinished += ContinueGame;

                levelManager.gameObject.SetActive(false);
                meetingManager.gameObject.SetActive(true);
                mainMenu.SetActive(false);
                endingsContainer.SetActive(false);

                break;

            case GameState.Outro:

                levelManager.LevelFinished -= SubmitLevel;

                endingsContainer.SetActive(true);
                meetingManager.gameObject.SetActive(false);
                levelManager.gameObject.SetActive(false);

                break;

        }

    }

}