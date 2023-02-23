using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile Database")]
public class TileDatabase : SingletonScriptableObject<TileDatabase> {

    public GameObject[] baseTilePrefabs;
    public GameObject[] farmTilePrefabs;
    public GameObject farmHouseTilePrefab;
    public GameObject[] energyTilePrefabs;

    public GameObject GetTileByType(TileType _type) {
        switch(_type) {
            case TileType.None:
                return null;
            case TileType.BaseTile:
                return baseTilePrefabs[Random.Range(0, baseTilePrefabs.Length)];
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
        int randomIndex = Random.Range(1, typesLength);
        return (TileType)randomIndex;
    }

}
