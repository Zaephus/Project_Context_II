using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public List<Tile> Generate(int _size) { return Generate(_size, null); }
    public List<Tile> Generate(int _size, List<Tile> _existingTiles) {

        float hexWidth = Mathf.Sqrt(3);
        float hexHeight = 2.0f;

        List<Tile> tiles = new List<Tile>();

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

        // int previousSize;

        // if(_existingTiles != null) {
        //     for(int i = 0; i < _existingTiles.Count; i++) {
        //         if(_existingTiles[i].hexPosition.y)
        //     }
        // }

        for(int x = -_size/2; x <= _size/2; x++) {

            if(x >= 0) {
                yMin = x - _size/2;
                yMax = _size/2;
            }
            else {
                yMin = -_size/2;
                yMax = _size/2 + x;
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

                objectToInstantiate = TileDatabase.Instance.GetTileByType(TileType.BaseTile);
                type = TileType.BaseTile;

                Tile tile = Instantiate(objectToInstantiate, tilePos, objectToInstantiate.transform.rotation, transform).GetComponent<Tile>();

                tile.hexPosition = hexPos;
                tile.tileRotation = TileRotation.Zero;
                tile.tileHeight = TileHeight.Zero;
                tile.tileType = type;
                tiles.Add(tile);

            }

        }

        return tiles;

    }
    
}