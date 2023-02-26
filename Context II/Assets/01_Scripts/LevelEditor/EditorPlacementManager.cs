using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorPlacementManager : MonoBehaviour {

    private bool IsChecking {
        get {
            return isChecking;
        }
        set {
            isChecking = value;
            if(value) {
                StartCoroutine(CheckForTile());
            }
            else {
                tileSelector.SetActive(false);
            }
        }
    }
    private bool isChecking;

    [SerializeField]
    private GameObject tileSelector;
    [SerializeField]
    private float selectorOffset;

    private GameObject objectToInstantiate;
    private TileType selectedType;

    private TileRotation tileRotation = TileRotation.Zero;
    private TileHeight tileHeight = TileHeight.Zero;

    private Tile hoveredTile;

    private LevelEditor levelEditor;

    public void Initialize(LevelEditor _levelEditor) {
        levelEditor = _levelEditor;
    }

    public void OnUpdate() {

        SetTileRotation();
        SetTileHeight();

        if(hoveredTile != null && selectedType != TileType.None) {
            if(Input.GetMouseButtonDown(0)) {
                PlaceTile();
            }
        }

    }

    public void ChangeSelection(int _type) {

        TileType type = (TileType)_type;

        if(selectedType == type) {
            selectedType = TileType.None;
        }
        else {
            selectedType = type;
        }

        if(selectedType == TileType.None) {
            IsChecking = false;
        }
        else {
            IsChecking = true;
        }

    }

    private void PlaceTile() {

        objectToInstantiate = TileDatabase.Instance.GetTileByType(selectedType);

        Vector3 tilePos = new Vector3(hoveredTile.transform.position.x, Hex.GetTileHeight(tileHeight), hoveredTile.transform.position.z);
        Vector3 tileRot = objectToInstantiate.transform.eulerAngles + new Vector3(0, Hex.GetTileRotation(tileRotation), 0);

        Vector3Int hexPos = hoveredTile.hexPosition;

        int tileIndex = levelEditor.tiles.FindIndex(x => x == hoveredTile);

        levelEditor.tiles.Remove(hoveredTile);

        Destroy(hoveredTile.gameObject);
        hoveredTile = null;

        Tile tile = Instantiate(objectToInstantiate, tilePos, Quaternion.Euler(tileRot), transform).GetComponent<Tile>();
        tile.hexPosition = hexPos;
        tile.tileType = selectedType;
        tile.tileRotation = tileRotation;
        tile.tileHeight = tileHeight;

        levelEditor.tiles.Insert(tileIndex, tile);

    }

    private void SetTileRotation() {
        if(Input.GetKeyDown(KeyCode.R)) {
            if((int)tileRotation >= Enum.GetValues(typeof(TileRotation)).Length - 1) {
                tileRotation = (TileRotation)0;
            }
            else {
                tileRotation++;
            }
            tileSelector.transform.eulerAngles = new Vector3(0, Hex.GetTileRotation(tileRotation), 0);
        }
    }

    private void SetTileHeight() {
        if(Input.mouseScrollDelta.y > 0) {
            if((int)tileHeight >= Enum.GetValues(typeof(TileHeight)).Length - 1) {
                tileHeight = (TileHeight)0;
            }
            else {
                tileHeight++;
            }

            tileSelector.transform.position = new Vector3(
                tileSelector.transform.position.x,
                selectorOffset + Hex.GetTileHeight(tileHeight),
                tileSelector.transform.position.z
            );

        }
        else if(Input.mouseScrollDelta.y < 0) {
            if((int)tileHeight <= 0) {
                tileHeight = (TileHeight)Enum.GetValues(typeof(TileHeight)).Length - 1;
            }
            else {
                tileHeight--;
            }

            tileSelector.transform.position = new Vector3(
                tileSelector.transform.position.x,
                selectorOffset + Hex.GetTileHeight(tileHeight),
                tileSelector.transform.position.z
            );

        }
    }

    private IEnumerator CheckForTile() {

        yield return new WaitForSeconds(0.2f);

        RaycastHit hit;

        while(isChecking) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)) {
                hoveredTile = hit.collider.GetComponentInParent<Tile>();
                tileSelector.SetActive(true);

                tileSelector.transform.position = new Vector3(
                    hoveredTile.transform.position.x,
                    selectorOffset + Hex.GetTileHeight(tileHeight),
                    hoveredTile.transform.position.z
                );

            }
            else {
                hoveredTile = null;
                tileSelector.SetActive(false);
            }
            yield return new WaitForSeconds(0.05f);
        }

    }

}