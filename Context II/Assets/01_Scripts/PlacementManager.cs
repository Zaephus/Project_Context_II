using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour {

    [HideInInspector]
    public bool isChecking = true;

    [SerializeField]
    private GameObject tileSelector;

    [SerializeField]
    private float selectorOffset;

    [SerializeField]
    private GameObject[] tiles;

    [SerializeField]
    private Color selectableColor;
    [SerializeField]
    private Color unSelectableColor;

    private HexTile hoveredTile;

    public void Initialize() {
        StartCoroutine(CheckForTile());
    }

    public void OnUpdate() {

        if(hoveredTile != null) {
            if(CheckPossiblePlacement()) {

                tileSelector.GetComponent<MeshRenderer>().material.color = selectableColor;

                if(Input.GetMouseButtonDown(0)) {

                    GameManager.Instance.tiles.Remove(hoveredTile.hexPosition);
                    Vector3 tilePos = hoveredTile.transform.position;
                    Vector3Int hexPos = hoveredTile.hexPosition;
                    tilePos.y += 0.4f;

                    Destroy(hoveredTile.gameObject);
                    hoveredTile = null;

                    HexTile tile = Instantiate(tiles[0], tilePos, Quaternion.identity, GameManager.Instance.transform).GetComponent<HexTile>();
                    tile.hexPosition = hexPos;
                    tile.tileType = TileType.FarmTile;
                    GameManager.Instance.tiles.Add(hexPos, tile);

                }

            }
            else {
                tileSelector.GetComponent<MeshRenderer>().material.color = unSelectableColor;
            }
        }

    }

    private IEnumerator CheckForTile() {

        yield return new WaitForSeconds(0.2f);

        RaycastHit hit;

        while(isChecking) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)) {
                hoveredTile = hit.collider.GetComponent<HexTile>();
                tileSelector.GetComponent<MeshRenderer>().enabled = true;
                tileSelector.transform.position = new Vector3(hoveredTile.transform.position.x, tileSelector.transform.position.y, hoveredTile.transform.position.z);
            }
            else {
                hoveredTile = null;
                tileSelector.GetComponent<MeshRenderer>().enabled = false;
            }
            yield return new WaitForSeconds(0.05f);
        }

    }

    private bool CheckPossiblePlacement() {

        if(hoveredTile.tileType == TileType.BaseTile) {

            HexTile checkTile;

            if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x + 1, hoveredTile.hexPosition.y - 1, hoveredTile.hexPosition.z))) {
                checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x + 1, hoveredTile.hexPosition.y - 1, hoveredTile.hexPosition.z)];
                if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
                    return true;
                }
            }

            if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x + 1, hoveredTile.hexPosition.y, hoveredTile.hexPosition.z - 1))) {
                checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x + 1, hoveredTile.hexPosition.y, hoveredTile.hexPosition.z - 1)];
                if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
                    return true;
                }
            }

            if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x, hoveredTile.hexPosition.y + 1, hoveredTile.hexPosition.z - 1))) {
                checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x, hoveredTile.hexPosition.y + 1, hoveredTile.hexPosition.z - 1)];
                if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
                    return true;
                }
            }

            if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x - 1, hoveredTile.hexPosition.y + 1, hoveredTile.hexPosition.z))) {
                checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x - 1, hoveredTile.hexPosition.y + 1, hoveredTile.hexPosition.z)];
                if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
                    return true;
                }
            }

            if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x - 1, hoveredTile.hexPosition.y, hoveredTile.hexPosition.z + 1))) {
                checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x - 1, hoveredTile.hexPosition.y, hoveredTile.hexPosition.z + 1)];
                if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
                    return true;
                }
            }

            if(GameManager.Instance.tiles.ContainsKey(new Vector3Int(hoveredTile.hexPosition.x, hoveredTile.hexPosition.y - 1, hoveredTile.hexPosition.z + 1))) {
                checkTile = GameManager.Instance.tiles[new Vector3Int(hoveredTile.hexPosition.x, hoveredTile.hexPosition.y - 1, hoveredTile.hexPosition.z + 1)];
                if(checkTile.tileType == TileType.FarmTile || checkTile.tileType == TileType.HouseTile) {
                    return true;
                }
            }

        }

        return false;

    }

}