using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Hex {

    public static float GetTileRotation(TileRotation _tileRotation) {
        switch(_tileRotation) {
            case TileRotation.OneSixth:
                return 60.0f;
            case TileRotation.TwoSixth:
                return 120.0f;
            case TileRotation.ThreeSixth:
                return 180.0f;
            case TileRotation.FourSixth:
                return 240.0f;
            case TileRotation.FiveSixth:
                return 300.0f;
            default:
                return 0.0f;
        }
    }

    public static float GetRandomTileRotation() {
        TileRotation rotation = (TileRotation)Random.Range(0, System.Enum.GetValues(typeof(TileRotation)).Length);
        return GetTileRotation(rotation);
    }

    public static float GetTileHeight(TileHeight _tileHeight) {
        switch(_tileHeight) {
            case TileHeight.Zero:
                return 0.0f;
            case TileHeight.OneQuarter:
                return 0.25f;
            case TileHeight.Half:
                return 0.5f;
            case TileHeight.ThreeQuarter:
                return 0.75f;
            case TileHeight.One:
                return 1.0f;
            case TileHeight.One_OneQuarter:
                return 1.25f;
            case TileHeight.One_Half:
                return 1.5f;
            case TileHeight.One_ThreeQuarter:
                return 1.75f;
            case TileHeight.Two:
                return 2.0f;
            default:
                return 0.0f;
        }
    }

    public static int GetHexDistanceInt(Vector3 _posOne, Vector3 _posTwo) {
        Vector3 diff = _posOne - _posTwo;
        return (int)Mathf.Max(Mathf.Abs(diff.x), Mathf.Abs(diff.y), Mathf.Abs(diff.z));
    }

}