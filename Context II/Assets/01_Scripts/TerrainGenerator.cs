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

    public Dictionary<Vector3Int, HexTile> Generate() {

        float hexWidth = Mathf.Sqrt(3);
        float hexHeight = 2.0f;

        if(size % 2 == 0) {
            size++;
        }

        Dictionary<Vector3Int, HexTile> tiles = new Dictionary<Vector3Int, HexTile>();

        Vector3 tilePos;
        Vector3Int hexPos;

        GameObject objectToInstantiate;
        TileType type;

        int yMin;
        int yMax;

        float xOffset;

        int q;
        int r;
        int s;

        for(int x = -size/2; x <= size/2; x++) {

            if(x >= 0) {
                yMin = x - size/2;
                yMax = size/2;
            }
            else {
                yMin = -size/2;
                yMax = size/2 + x;
            }

            for(int y = yMin; y <= yMax; y++) {

                if(y != 0) {
                    xOffset = y * 0.5f;
                }
                else {
                    xOffset = 0.0f;
                }

                q = x;
                r = -y;
                s = -q - r;

                tilePos = new Vector3((x - xOffset) * hexWidth, 0, y * 0.75f * hexHeight);
                hexPos = new Vector3Int(q, r, s);

                if(tilePos == Vector3.zero) {
                    objectToInstantiate = farmHouseTile;
                    type = TileType.HouseTile;
                    tilePos.y += 0.4f;
                }
                else {
                    objectToInstantiate = baseTile;
                    type = TileType.BaseTile;
                }

                HexTile tile = Instantiate(objectToInstantiate, tilePos, objectToInstantiate.transform.rotation, transform).GetComponent<HexTile>();

                tile.hexPosition = hexPos;
                tile.tileType = type;
                tiles.Add(hexPos, tile);

            }

        }

        return tiles;

    }
    
}