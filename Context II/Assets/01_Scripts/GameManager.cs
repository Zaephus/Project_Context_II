using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {
    BaseTile = 0,
    FarmTile = 1,
    HouseTile = 2,
    EnergyTile = 3
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
    
    public Dictionary<GameObject, TileType> tiles = new Dictionary<GameObject, TileType>();

    private TerrainGenerator terrainGenerator;
    private PlacementManager placementManager;


    private void Start() {
        terrainGenerator = GetComponent<TerrainGenerator>();
        placementManager = GetComponent<PlacementManager>();

        tiles = terrainGenerator.Generate();

        placementManager.Initialize();
    }

    private void Update() {
        placementManager.OnUpdate();
    }

}