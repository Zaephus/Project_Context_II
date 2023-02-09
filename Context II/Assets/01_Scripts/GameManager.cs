using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {
    None = 0,
    BaseTile = 1,
    FarmTile = 2,
    HouseTile = 3,
    EnergyTile = 4
}

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

    private TerrainGenerator terrainGenerator;
    private PlacementManager placementManager;
    [SerializeField]
    private InventoryManager inventoryManager;

    private void Start() {
        terrainGenerator = GetComponent<TerrainGenerator>();
        placementManager = GetComponent<PlacementManager>();

        tiles = terrainGenerator.Generate();

        placementManager.Initialize();
    }

    private void Update() {
        inventoryManager.OnUpdate();
        placementManager.OnUpdate();
    }

}