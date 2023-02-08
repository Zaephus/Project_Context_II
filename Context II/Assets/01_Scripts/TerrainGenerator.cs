using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    private float hexWidth;
    private float hexHeight;

    [SerializeField]
    private GameObject baseTile;
    [SerializeField]
    private GameObject farmHouseTile;

    [SerializeField]
    private int size;

    [SerializeField, HideInInspector]
    private List<GameObject> tiles = new List<GameObject>();

    public void Generate() {

        if(size % 2 == 0) {
            size ++;
        }

        hexWidth = Mathf.Sqrt(3);
        hexHeight = 2.0f;

        ClearTiles();

        Vector3 tilePos;

        for(int x = -size/2; x <= size/2; x++) {
                
            tilePos = new Vector3(x * hexWidth, 0, 0);

            GameObject objectToInstantiate;

            if(x * hexWidth == 0) {
                objectToInstantiate = farmHouseTile;
                tilePos.y += 0.4f;
            }
            else {
                objectToInstantiate = baseTile;
            }
            
            GameObject tile = Instantiate(objectToInstantiate, tilePos, Quaternion.identity, transform);
            tiles.Add(tile);

        }

        int xMin;
        int xMax;
        float xOffset;

        for(int y = 1; y <= size/2; y++) {

            if(y % 2 == 0) {
                xMin = -(size - y) / 2;
                xMax = (size - y) / 2;
                xOffset = 0;
            }
            else {
                xMin = 1 - (size - y) / 2;
                xMax = (size - y) / 2;
                xOffset = 0.5f;
            }

            for(int x = xMin; x <= xMax; x++) {
                
                tilePos = new Vector3((x - xOffset) * hexWidth, 0, y * 0.75f * hexHeight);

                GameObject tile = Instantiate(baseTile, tilePos, Quaternion.identity, transform);
                tiles.Add(tile);

            }

        }

        for(int y = -size/2; y <= -1; y++) {

            if(y % 2 == 0) {
                xMin = -(size + y) / 2;
                xMax = (size + y) / 2;
                xOffset = 0;
            }
            else {
                xMin = 1 - (size + y) / 2;
                xMax = (size + y) / 2;
                xOffset = 0.5f;
            }

            for(int x = xMin; x <= xMax; x++) {
                
                tilePos = new Vector3((x - xOffset) * hexWidth, 0, y * 0.75f * hexHeight);

                GameObject tile = Instantiate(baseTile, tilePos, Quaternion.identity, transform);
                tiles.Add(tile);

            }

        }

    }

    public void ClearTiles() {

        for(int i = tiles.Count - 1; i >= 0; i--) {
            DestroyImmediate(tiles[i]);
        }

        tiles.Clear();

    }
    
}