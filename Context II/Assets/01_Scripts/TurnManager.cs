using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour {

    [SerializeField]
    private GameObject playerControls;

    [SerializeField]
    private TMP_Text turnText;

    private int TurnCounter {
        get {
            return turnCounter;
        }
        set {
            turnText.text = "Turn " + value;
            turnCounter = value;
        }
    }
    private int turnCounter;

    [SerializeField]
    private InventoryManager inventoryManager;

    private enum TurnState {
        PlayerTurn,
        EnergyTurn,
        Waiting
    }
    private TurnState State {
        get {
            return state;
        }
        set {
            switch(value) {

                case TurnState.PlayerTurn:
                    playerControls.gameObject.SetActive(true);
                    break;

                case TurnState.EnergyTurn:
                    playerControls.gameObject.SetActive(false);
                    StartCoroutine(EnergyTurn());
                    break;

                case TurnState.Waiting:
                    playerControls.gameObject.SetActive(false);
                    inventoryManager.FillSlots(
                        GameManager.Instance.GetRandomTileType(),
                        GameManager.Instance.GetRandomTileType(),
                        GameManager.Instance.GetRandomTileType()
                    );

                    State = TurnState.PlayerTurn;
                    break;

            }
            state = value;
        }
    }

    private TurnState state;

    public void OnStart() {
        State = TurnState.Waiting;
    }

    public void EndTurn() {
        State = TurnState.EnergyTurn;
        TurnCounter++;
    }

    private IEnumerator EnergyTurn() {
        yield return null;
        State = TurnState.Waiting;
    }

}