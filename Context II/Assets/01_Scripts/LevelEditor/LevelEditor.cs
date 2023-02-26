using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelEditor : MonoBehaviour {

    private int size = 5;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private TMP_Text sizeValueText;

    private TerrainGenerator terrainGenerator;

    private List<Tile> tiles = new List<Tile>();
    private List<TileData> tileDatas = new List<TileData>();

    private void Start() {

        terrainGenerator = GetComponent<TerrainGenerator>();
        SizeValueChanged();

    }

    public void SizeValueChanged() {

        if(slider.value % 2 == 0) {
            slider.SetValueWithoutNotify(slider.value + 1);
        }

        size = Mathf.RoundToInt(slider.value);
        sizeValueText.text = size.ToString();
        
        tiles = terrainGenerator.Generate(size, tiles);

        tileDatas.Clear();
        foreach(Tile t in tiles) {
            tileDatas.Add(new TileData(t.hexPosition, t.tileRotation, t.tileHeight, t.tileType, t.powerApproval, t.citizenApproval));
        }

    }

    public void SaveLevel() {
        EditorDataManager.SaveLevel(tileDatas.ToArray());
    }

    public void LoadLevel() {
        Debug.Log(EditorDataManager.LoadLevel().Length);
    }

}