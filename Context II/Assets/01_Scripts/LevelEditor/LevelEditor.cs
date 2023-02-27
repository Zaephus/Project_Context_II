using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelEditor : MonoBehaviour {
    
    public List<Tile> tiles = new List<Tile>();

    private int size = 5;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private TMP_Text sizeValueText;

    private EditorLevelLoader editorLevelLoader;
    private TerrainGenerator terrainGenerator;
    private EditorPlacementManager editorPlacementManager;

    private List<TileData> tileDatas = new List<TileData>();

    private void Start() {

        editorLevelLoader = GetComponent<EditorLevelLoader>();
        terrainGenerator = GetComponent<TerrainGenerator>();

        editorPlacementManager = GetComponent<EditorPlacementManager>();
        editorPlacementManager.Initialize(this);

        SizeValueChanged();

    }

    private void Update() {
        editorPlacementManager.OnUpdate();
    }

    public void SizeValueChanged() {

        if(slider.value % 2 == 0) {
            slider.SetValueWithoutNotify(slider.value + 1);
        }

        size = Mathf.RoundToInt(slider.value);
        sizeValueText.text = size.ToString();
        
        tiles = terrainGenerator.Generate(size, tiles);

    }

    public void SaveLevel() {

        tileDatas.Clear();
        foreach(Tile t in tiles) {
            tileDatas.Add(new TileData(t.transform.position, t.hexPosition, t.tileRotation, t.tileHeight, t.tileType, t.PowerApproval, t.CitizenApproval));
        }

        EditorDataManager.SaveLevel(tileDatas.ToArray());

    }

    public void LoadLevel() {

        TileData[] newTileDatas = EditorDataManager.LoadLevel();
        if(newTileDatas == null) {
            return;
        }

        tileDatas.Clear();
        tileDatas.AddRange(newTileDatas);

        for(int i = tiles.Count-1; i >= 0; i--) {
            Destroy(tiles[i].gameObject);
        }
        tiles.Clear();

        tiles = editorLevelLoader.Generate(tileDatas.ToArray());

        size = Hex.GetHexDistanceInt(Vector3.zero, tiles[0].hexPosition) * 2 + 1;
        sizeValueText.text = size.ToString();
        slider.SetValueWithoutNotify(size);

    }

}