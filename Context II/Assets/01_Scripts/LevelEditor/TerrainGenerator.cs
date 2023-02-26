using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public List<Tile> Generate(int _size) { return Generate(_size, null); }
    public List<Tile> Generate(int _size, List<Tile> _existingTiles) {

        float hexWidth = Mathf.Sqrt(3);
        float hexHeight = 2.0f;

        List<Tile> tiles = new List<Tile>();

        Vector3 tilePos;
        Vector3Int hexPos;

        GameObject objectToInstantiate = TileDatabase.Instance.GetTileByType(TileType.BaseTile);
        TileType type = TileType.BaseTile;

        int previousSize = 0;
        if(_existingTiles != null && _existingTiles.Count > 0) {
            previousSize = GetHexDistanceInt(Vector3.zero, _existingTiles[0].hexPosition) * 2 + 1;
        }

        int yMin;
        int yMax;

        float xOffset;

        int q;
        int r;
        int s;

        if(_size >= previousSize) {
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

                    q = x;
                    r = -y;
                    s = -q - r;

                    hexPos = new Vector3Int(q, r, s);

                    if(_existingTiles != null) {
                        Tile currentTile = _existingTiles.Find(tile => tile.hexPosition == hexPos);
                        if(currentTile != null) {
                            tiles.Add(currentTile);
                            continue;
                        }
                    }
                        
                    if(y != 0) {
                        xOffset = y * 0.5f;
                    }
                    else {
                        xOffset = 0.0f;
                    }

                    tilePos = new Vector3((x - xOffset) * hexWidth, 0, y * 0.75f * hexHeight);

                    objectToInstantiate = TileDatabase.Instance.GetTileByType(TileType.BaseTile);;

                    Tile tile = Instantiate(objectToInstantiate, tilePos, objectToInstantiate.transform.rotation, transform).GetComponent<Tile>();

                    tile.hexPosition = hexPos;
                    tile.tileRotation = TileRotation.Zero;
                    tile.tileHeight = TileHeight.Zero;
                    tile.tileType = type;
                    tiles.Add(tile);

                }

            }
        }
        else if(_size < previousSize) {

            int yMinNew;
            int yMaxNew;

            for(int x = -previousSize/2; x <= previousSize/2; x++) {

                if(x >= 0) {
                    yMin = x - previousSize/2;
                    yMax = previousSize/2;
                    yMinNew = x - _size/2;
                    yMaxNew = _size/2;
                }
                else {
                    yMin = -previousSize/2;
                    yMax = previousSize/2 + x;
                    yMinNew = -_size/2;
                    yMaxNew = _size/2 + x;
                }

                for(int y = yMin; y <= yMax; y++) {

                    q = x;
                    r = -y;
                    s = -q - r;

                    hexPos = new Vector3Int(q, r, s);

                    Tile currentTile = _existingTiles.Find(tile => tile.hexPosition == hexPos);

                    if(GetHexDistanceInt(Vector3.zero, hexPos) * 2 + 1 <= _size) {
                        tiles.Add(currentTile);
                    }
                    else {
                        Destroy(currentTile.gameObject);
                    }

                }

            }
            
        }
        
        return tiles;

    }

    private int GetHexDistanceInt(Vector3 _posOne, Vector3 _posTwo) {
        Vector3 diff = _posOne - _posTwo;
        return (int)Mathf.Max(Mathf.Abs(diff.x), Mathf.Abs(diff.y), Mathf.Abs(diff.z));
    }
    
}