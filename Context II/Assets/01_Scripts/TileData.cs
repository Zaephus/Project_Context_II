using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData {

    public int q;
    public int r;
    public int s;

    public TileRotation tileRotation;
    public TileHeight tileHeight;
    public TileType tileType;
    public float powerApproval;
    public float citizenApproval;

    public TileData() {}

    public TileData(Vector3Int _hexPos, TileRotation _tileRot, TileHeight _tileHeight, TileType _tileType, float _powerApp, float _citizenApp) {

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