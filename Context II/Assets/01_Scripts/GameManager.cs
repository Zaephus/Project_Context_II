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
    
    public Dictionary<Vector3Int, HexTile> tiles = new Dictionary<Vector3Int, HexTile>();
    private PlacementManager placementManager;
    private TurnManager turnManager;
    [SerializeField]
    private InventoryManager inventoryManager;

    private void Start() {
        placementManager = GetComponent<PlacementManager>();
        turnManager = GetComponent<TurnManager>();

        placementManager.Initialize();
        turnManager.OnStart();
    }

    private void Update() {
        inventoryManager.OnUpdate();
        placementManager.OnUpdate();
    }

}