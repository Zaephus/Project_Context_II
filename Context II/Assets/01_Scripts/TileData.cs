using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData {

    public float x;
    public float z;

    public int q;
    public int r;
    public int s;

    public TileRotation tileRotation;
    public TileHeight tileHeight;
    public TileType tileType;
    public int powerApproval;
    public int citizenApproval;

    public TileData() {}

    public TileData(Vector3 _pos, Vector3Int _hexPos, TileRotation _tileRot, TileHeight _tileHeight, TileType _tileType, int _powerApp, int _citizenApp) {

        x = _pos.x;
        z = _pos.z;

        q = _hexPos.x;
        r = _hexPos.y;
        s = _hexPos.z;

        tileRotation = _tileRot;
        tileHeight = _tileHeight;
        tileType = _tileType;
        powerApproval = _powerApp;
        citizenApproval = _citizenApp;
        
    }

}