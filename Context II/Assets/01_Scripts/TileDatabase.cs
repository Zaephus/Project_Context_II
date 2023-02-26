using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile Database")]
public class TileDatabase : SingletonScriptableObject<TileDatabase> {

    public GameObject baseTilePrefab;
    public GameObject[] emptyTilePrefabs;
    public GameObject[] farmTilePrefabs;
    public GameObject farmHouseTilePrefab;
    public GameObject[] energyTilePrefabs;

    public GameObject GetTileByType(TileType _type) {
        switch(_type) {
            case TileType.None:
                return null;
            case TileType.BaseTile:
                return baseTilePrefab;
            case TileType.EmptyTile:
                return emptyTilePrefabs[Random.Range(0, emptyTilePrefabs.Length)];
            case TileType.FarmTile:
                return farmTilePrefabs[Random.Range(0, farmTilePrefabs.Length)];
            case TileType.HouseTile:
                return farmHouseTilePrefab;
            case TileType.EnergyTile:
                return energyTilePrefabs[Random.Range(0, energyTilePrefabs.Length)];
            default:
                return null;
        }
    }

    public TileType GetRandomTileType() {
        int typesLength = System.Enum.GetValues(typeof(TileType)).Length;
        int randomIndex = Random.Range((int)TileType.EmptyTile, typesLength);
        return (TileType)randomIndex;
    }

}
