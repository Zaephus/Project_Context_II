using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile Database", menuName = "Scriptable Objects/Tile Database")]
public class TileDatabase : SingletonScriptableObject<TileDatabase> {

    public GameObject baseTilePrefab;
    public GameObject[] plainsTilePrefabs;
    public GameObject[] forestTilePrefabs;
    public GameObject[] sandTilePrefabs;
    public GameObject[] siltTilePrefabs;
    public GameObject[] waterTilePrefabs;
    public GameObject[] cityTilePrefabs;
    public GameObject[] officeTilePrefabs;
    public GameObject[] farmTilePrefabs;
    public GameObject[] pastureTilePrefabs;
    public GameObject[] windmillTilePrefabs;
    public GameObject[] clubTilePrefabs;
    public GameObject[] beachHutTilePrefabs;
    public GameObject[] farmHouseTilePrefabs;

    public GameObject GetTileByType(TileType _type) {

        switch(_type) {

            case TileType.BaseTile:
                return baseTilePrefab;

            case TileType.PlainsTile:
                return plainsTilePrefabs[Random.Range(0, plainsTilePrefabs.Length)];

            case TileType.ForestTile:
                return forestTilePrefabs[Random.Range(0, forestTilePrefabs.Length)];

            case TileType.SandTile:
                return sandTilePrefabs[Random.Range(0, sandTilePrefabs.Length)];

            case TileType.SiltTile:
                return siltTilePrefabs[Random.Range(0, siltTilePrefabs.Length)];

            case TileType.WaterTile:
                return waterTilePrefabs[Random.Range(0, waterTilePrefabs.Length)];

            case TileType.CityTile:
                return cityTilePrefabs[Random.Range(0, cityTilePrefabs.Length)];

            case TileType.OfficeTile:
                return officeTilePrefabs[Random.Range(0, officeTilePrefabs.Length)];

            case TileType.FarmTile:
                return farmTilePrefabs[Random.Range(0, farmTilePrefabs.Length)];

            case TileType.PastureTile:
                return pastureTilePrefabs[Random.Range(0, pastureTilePrefabs.Length)];

            case TileType.WindmillTile:
                return windmillTilePrefabs[Random.Range(0, windmillTilePrefabs.Length)];

            case TileType.ClubTile:
                return clubTilePrefabs[Random.Range(0, clubTilePrefabs.Length)];

            case TileType.BeachHutTile:
                return beachHutTilePrefabs[Random.Range(0, beachHutTilePrefabs.Length)];

            case TileType.FarmHouseTile:
                return farmHouseTilePrefabs[Random.Range(0, farmHouseTilePrefabs.Length)];

            default:
                return null;

        }

    }

    public TileType GetRandomTileType() {
        int typesLength = System.Enum.GetValues(typeof(TileType)).Length;
        int randomIndex = Random.Range((int)TileType.PlainsTile, typesLength);
        return (TileType)randomIndex;
    }

}
