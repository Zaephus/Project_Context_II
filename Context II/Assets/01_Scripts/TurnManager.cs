using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour {

    [SerializeField]
    private Canvas playerControls;

    [SerializeField]
    private TMP_Text turnText;

    private int TurnCounter {
        get {
            return turnCounter;
        }
        set {
            turnText.text = "Text " + value;
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
                    playerControls.enabled = true;
                    break;

                case TurnState.EnergyTurn:
                    playerControls.enabled = false;
                    StartCoroutine(EnergyTurn());
                    break;

                case TurnState.Waiting:
                    playerControls.enabled = false;
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