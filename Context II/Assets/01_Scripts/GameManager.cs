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
    
    public List<Tile> tiles = new List<Tile>();
    private PlacementManager placementManager;
    
    private LevelLoader levelLoader;
    private LevelGenerator levelGenerator;

    [SerializeField]
    private TextAsset level;

    private void Start() {
        placementManager = GetComponent<PlacementManager>();

        levelLoader = GetComponent<LevelLoader>();
        levelGenerator = GetComponent<LevelGenerator>();

        tiles = levelLoader.Generate(level, levelGenerator);
    }

    private void Update() {
        placementManager.OnUpdate();
    }

}