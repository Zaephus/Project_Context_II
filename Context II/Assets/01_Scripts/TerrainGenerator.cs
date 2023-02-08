using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject baseTile;
    [SerializeField]
    private GameObject farmHouseTile;

    [SerializeField]
    private int size;

    public Dictionary<GameObject, TileType> Generate() {

        float hexWidth = Mathf.Sqrt(3);
        float hexHeight = 2.0f;

        if(size % 2 == 0) {
            size++;
        }

        Dictionary<GameObject, TileType> tiles = new Dictionary<GameObject, TileType>();

        Vector3 tilePos;

        for(int x = -size/2; x <= size/2; x++) {
                
            tilePos = new Vector3(x * hexWidth, 0, 0);

            GameObject objectToInstantiate;
            TileType type;

            if(x * hexWidth == 0) {
                objectToInstantiate = farmHouseTile;
                type = TileType.HouseTile;
                tilePos.y += 0.4f;
            }
            else {
                objectToInstantiate = baseTile;
                type = TileType.BaseTile;
            }
            
            GameObject tile = Instantiate(objectToInstantiate, tilePos, Quaternion.identity, transform);
            tiles.Add(tile, type);

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
                tiles.Add(tile, TileType.BaseTile);

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
                tiles.Add(tile, TileType.BaseTile);

            }

        }

        return tiles;

    }
    
}