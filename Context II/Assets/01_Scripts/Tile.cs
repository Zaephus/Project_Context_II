using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileRotation {
    Zero = 0,
    OneSixth = 1,
    TwoSixth = 2,
    ThreeSixth = 3,
    FourSixth = 4,
    FiveSixth = 5
}

public enum TileHeight {
    Zero = 0,
    OneQuarter = 1,
    Half = 2,
    ThreeQuarter = 3,
    One = 4,
    One_OneQuarter = 5,
    One_Half = 6,
    One_ThreeQuarter = 7,
    Two = 8
}

[System.Serializable]
public class Tile : MonoBehaviour {

    public Vector3 hexPosition;
    public TileRotation tileRotation;
    public TileHeight tileHeight;
    public TileType tileType;
    [Range(0, 1)]
    public float powerApproval;
    [Range(0, 1)]
    public float citizenApproval;

}