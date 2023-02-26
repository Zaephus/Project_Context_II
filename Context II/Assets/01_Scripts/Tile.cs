using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Vector3Int hexPosition;
    public TileRotation tileRotation;
    public TileHeight tileHeight;
    public TileType tileType;
    [Range(0, 1)]
    public float powerApproval;
    [Range(0, 1)]
    public float citizenApproval;

}