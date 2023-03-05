using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public List<Tile> Generate(TileData[] _tileDatas) {

        List<Tile> tiles = new List<Tile>();

        Vector3 tilePos;
        Vector3 tileRot;

        Vector3Int hexPos;

        GameObject objectToInstantiate;

        for(int i = 0; i < _tileDatas.Length; i++) {

            objectToInstantiate = TileDatabase.Instance.GetTileByType(_tileDatas[i].tileType);

            tilePos = new Vector3(_tileDatas[i].x, Hex.GetTileHeight(_tileDatas[i].tileHeight), _tileDatas[i].z);
            tileRot = objectToInstantiate.transform.eulerAngles + new Vector3(0, Hex.GetTileRotation(_tileDatas[i].tileRotation), 0);

            hexPos = new Vector3Int(_tileDatas[i].q, _tileDatas[i].r, _tileDatas[i].s);

            Tile tile = Instantiate(objectToInstantiate, tilePos, Quaternion.Euler(tileRot), transform).GetComponent<Tile>();

            tile.hexPosition = hexPos;
            tile.tileRotation = _tileDatas[i].tileRotation;
            tile.tileHeight = _tileDatas[i].tileHeight;
            tile.tileType = _tileDatas[i].tileType;
            tile.dialogueIndex = _tileDatas[i].dialogueIndex;
            tile.PowerApproval = _tileDatas[i].powerApproval;
            tile.CitizenApproval = _tileDatas[i].citizenApproval;

            tiles.Add(tile);

        }

        return tiles;

    }

}