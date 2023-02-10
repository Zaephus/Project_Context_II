using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private TurnManager turnManager;
    [SerializeField]
    private InventoryManager inventoryManager;

    [SerializeField]
    private GameObject[] tileObjects;

    private void Start() {
        terrainGenerator = GetComponent<TerrainGenerator>();
        placementManager = GetComponent<PlacementManager>();
        turnManager = GetComponent<TurnManager>();

        tiles = terrainGenerator.Generate();

        placementManager.Initialize();
        turnManager.OnStart();
    }

    private void Update() {
        inventoryManager.OnUpdate();
        placementManager.OnUpdate();
    }
    
    public GameObject GetTileByType(TileType _type) {
        if(_type == TileType.None) {
            return null;
        }
        return tileObjects[(int)_type - 1];
    }

    public TileType GetRandomTileType() {
        int typesLength = Enum.GetValues(typeof(TileType)).Length;
        int randomIndex = Random.Range(1, typesLength);
        return (TileType)randomIndex;
    }

}