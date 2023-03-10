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

    private LevelGenerator levelGenerator;
    private TerrainGenerator terrainGenerator;
    private EditorPlacementManager editorPlacementManager;

    private List<TileData> tileDatas = new List<TileData>();

    private void Start() {

        levelGenerator = GetComponent<LevelGenerator>();
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
            tileDatas.Add(new TileData(t.transform.position, t.hexPosition, t.tileRotation, t.tileHeight, t.tileType, t.dialogueIndex, t.PowerApproval, t.CitizenApproval));
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

        tiles = levelGenerator.Generate(tileDatas.ToArray());

        Dictionary<Vector3, GameObject> dialogueIndicators = new Dictionary<Vector3, GameObject>();

        for(int i = 0; i < tiles.Count; i++) {
            if(tiles[i].dialogueIndex == 0) {
                continue;
            }
            else {
                Vector3 indicatorPos = tiles[i].transform.position + new Vector3(0, 1.5f, 0);
                GameObject indicator = Instantiate(editorPlacementManager.dialogueIndicatorPrefab, indicatorPos, editorPlacementManager.dialogueIndicatorPrefab.transform.rotation, tiles[i].transform);

                indicator.GetComponent<TMP_Text>().text = tiles[i].dialogueIndex.ToString();

                dialogueIndicators.Add(tiles[i].hexPosition, indicator);
            }
        }

        editorPlacementManager.dialogueIndicators = dialogueIndicators;

        size = Hex.GetHexDistanceInt(Vector3.zero, tiles[0].hexPosition) * 2 + 1;
        sizeValueText.text = size.ToString();
        slider.SetValueWithoutNotify(size);

        EditorCameraMovement.CameraReset.Invoke();

    }

}