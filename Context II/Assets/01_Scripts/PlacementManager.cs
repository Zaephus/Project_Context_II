using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour {

    private bool IsChecking {
        get {
            return isChecking;
        }
        set {
            isChecking = value;
            if(!value) {
                tileSelector.SetActive(false);
            }
        }
    }
    private bool isChecking = false;

    [SerializeField]
    private GameObject tileSelector;

    [SerializeField]
    private float selectorOffset;

    [SerializeField]
    private Color selectableColor;
    [SerializeField]
    private Color unSelectableColor;

    private GameObject objectToInstantiate;
    private TileType selectedType;

    private Tile hoveredTile;

    public void OnUpdate() {

        if(IsChecking) {
            CheckForTile();
        }

        if(hoveredTile != null && selectedType != TileType.None) {
            if(Input.GetMouseButtonDown(0)) {
                PlaceTile();
            }
        }

    }

    public void ToggleSelection() {
        IsChecking = !IsChecking;

        if(IsChecking) {
            selectedType = TileType.EnergyTile;
        }
        else {
            selectedType = TileType.None;
        }
    }

    private void PlaceTile() {

        objectToInstantiate = TileDatabase.Instance.GetTileByType(selectedType);

        Vector3 tilePos = hoveredTile.transform.position;
        Vector3Int hexPos = hoveredTile.hexPosition;

        TileHeight tileHeight = hoveredTile.tileHeight;

        int tileIndex = GameManager.Instance.tiles.FindIndex(x => x == hoveredTile);

        GameManager.Instance.tiles.Remove(hoveredTile);

        Tile tile = Instantiate(objectToInstantiate, tilePos, objectToInstantiate.transform.rotation, transform).GetComponent<Tile>();
        tile.hexPosition = hexPos;
        tile.tileType = selectedType;
        tile.tileHeight = tileHeight;
        tile.PowerApproval = hoveredTile.PowerApproval;
        tile.CitizenApproval = hoveredTile.CitizenApproval;

        Destroy(hoveredTile.gameObject);
        hoveredTile = null;

        GameManager.Instance.tiles.Insert(tileIndex, tile);

    }

    private void CheckForTile() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit)) {
            hoveredTile = hit.collider.GetComponentInParent<Tile>();

            if(hoveredTile == null) {
                return;
            }
            
            tileSelector.SetActive(true);

            tileSelector.transform.position = new Vector3(
                hoveredTile.transform.position.x,
                hoveredTile.transform.position.y + selectorOffset,
                hoveredTile.transform.position.z
            );

        }
        else {
            hoveredTile = null;
            tileSelector.SetActive(false);
        }

    }

    // private bool CheckPossiblePlacement() {

    //     if(hoveredTile.tileType == TileType.EmptyTile) {

    //         Tile checkTile;

    //         if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x + 1, hoveredTile.hexPosition.y - 1, hoveredTile.hexPosition.z))) {
    //             checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x + 1, hoveredTile.hexPosition.y - 1, hoveredTile.hexPosition.z)];
    //             if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
    //                 return true;
    //             }
    //         }

    //         if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x + 1, hoveredTile.hexPosition.y, hoveredTile.hexPosition.z - 1))) {
    //             checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x + 1, hoveredTile.hexPosition.y, hoveredTile.hexPosition.z - 1)];
    //             if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
    //                 return true;
    //             }
    //         }

    //         if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x, hoveredTile.hexPosition.y + 1, hoveredTile.hexPosition.z - 1))) {
    //             checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x, hoveredTile.hexPosition.y + 1, hoveredTile.hexPosition.z - 1)];
    //             if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
    //                 return true;
    //             }
    //         }

    //         if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x - 1, hoveredTile.hexPosition.y + 1, hoveredTile.hexPosition.z))) {
    //             checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x - 1, hoveredTile.hexPosition.y + 1, hoveredTile.hexPosition.z)];
    //             if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
    //                 return true;
    //             }
    //         }

    //         if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x - 1, hoveredTile.hexPosition.y, hoveredTile.hexPosition.z + 1))) {
    //             checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x - 1, hoveredTile.hexPosition.y, hoveredTile.hexPosition.z + 1)];
    //             if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
    //                 return true;
    //             }
    //         }

    //         if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x, hoveredTile.hexPosition.y - 1, hoveredTile.hexPosition.z + 1))) {
    //             checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x, hoveredTile.hexPosition.y - 1, hoveredTile.hexPosition.z + 1)];
    //             if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
    //                 return true;
    //             }
    //         }

    //     }

    //     return false;

    // }

}