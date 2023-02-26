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

    private void Start() {

        terrainGenerator = GetComponent<TerrainGenerator>();
        SizeValueChanged();

    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.D)) {
            slider.value ++;
        }
        if(Input.GetKeyDown(KeyCode.A)) {
            slider.value --;
        }
    }

    public void SizeValueChanged() {

        if(slider.value % 2 == 0) {
            slider.SetValueWithoutNotify(slider.value + 1);
        }

        size = Mathf.RoundToInt(slider.value);
        sizeValueText.text = size.ToString();
        
        tiles = terrainGenerator.Generate(size, tiles);

    }

}