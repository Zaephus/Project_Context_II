using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour {

    [SerializeField]
    private int size;

    private TerrainGenerator terrainGenerator;

    private void Start() {
        if(size % 2 == 0) {
            size++;
        }
        terrainGenerator = GetComponent<TerrainGenerator>();
        terrainGenerator.Generate(size);
    }

}