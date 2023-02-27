using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditorPlacementManager : MonoBehaviour {

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
    private bool isChecking;

    [SerializeField]
    private Slider approvalSlider;
    [SerializeField]
    private TMP_Text approvalValueText;

    private int approvalValue = 5;

    [SerializeField]
    private GameObject tileSelector;
    [SerializeField]
    private float selectorOffset;

    private GameObject objectToInstantiate;
    private TileType selectedTileType;

    private ApprovalType selectedApprovalType;

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

        if(IsChecking) {
            CheckForTile();
        }

        if(hoveredTile != null && (selectedTileType != TileType.None || selectedApprovalType != ApprovalType.None)) {
            if(Input.GetMouseButtonDown(0)) {
                if(selectedTileType != TileType.None) {
                    PlaceTile();
                }
                else if(selectedApprovalType != ApprovalType.None) {
                    SetTileApproval();
                }
            }
        }

    }

    public void ChangeTileSelection(int _type) {

        TileType type = (TileType)_type;

        if(selectedTileType == type) {
            selectedTileType = TileType.None;
        }
        else {
            selectedTileType = type;
        }

        if(selectedTileType == TileType.None) {
            IsChecking = false;
        }
        else {
            IsChecking = true;
        }

    }

    public void ChangeApprovalSelection(int _type) {

        ApprovalType type = (ApprovalType)_type;

        if(selectedApprovalType == type) {
            selectedApprovalType = ApprovalType.None;
        }
        else {
            selectedApprovalType = type;
        }

        switch(selectedApprovalType) {
            case ApprovalType.Power:
                Tile.TogglePowerApprovalVisibility?.Invoke(true);
                Tile.ToggleCitizenApprovalVisibility?.Invoke(false);
                break;
            case ApprovalType.Citizen:
                Tile.ToggleCitizenApprovalVisibility?.Invoke(true);
                Tile.TogglePowerApprovalVisibility?.Invoke(false);
                break;
            default:
                Tile.TogglePowerApprovalVisibility?.Invoke(false);
                Tile.ToggleCitizenApprovalVisibility?.Invoke(false);
                break;
        }

        if(selectedApprovalType == ApprovalType.None) {
            IsChecking = false;
        }
        else {
            IsChecking = true;
        }
        
    }

    public void ApprovalValueChanged() {
        approvalValue = (int)approvalSlider.value;
        approvalValueText.text = approvalValue.ToString();
    }

    private void PlaceTile() {

        objectToInstantiate = TileDatabase.Instance.GetTileByType(selectedTileType);

        Vector3 tilePos = new Vector3(hoveredTile.transform.position.x, Hex.GetTileHeight(tileHeight), hoveredTile.transform.position.z);
        Vector3 tileRot = objectToInstantiate.transform.eulerAngles + new Vector3(0, Hex.GetTileRotation(tileRotation), 0);

        Vector3Int hexPos = hoveredTile.hexPosition;

        int tileIndex = levelEditor.tiles.FindIndex(x => x == hoveredTile);

        levelEditor.tiles.Remove(hoveredTile);

        Destroy(hoveredTile.gameObject);
        hoveredTile = null;

        Tile tile = Instantiate(objectToInstantiate, tilePos, Quaternion.Euler(tileRot), transform).GetComponent<Tile>();
        tile.hexPosition = hexPos;
        tile.tileType = selectedTileType;
        tile.tileRotation = tileRotation;
        tile.tileHeight = tileHeight;

        levelEditor.tiles.Insert(tileIndex, tile);

    }

    private void SetTileApproval() {
        switch(selectedApprovalType) {
            case ApprovalType.Power:
                hoveredTile.PowerApproval = approvalValue;
                break;
            
            case ApprovalType.Citizen:
                hoveredTile.CitizenApproval = approvalValue;
                break;
        }
    }

    private void SetTileRotation() {
        if(Input.GetKeyDown(KeyCode.R)) {
            if((int)tileRotation >= System.Enum.GetValues(typeof(TileRotation)).Length - 1) {
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
            if((int)tileHeight >= System.Enum.GetValues(typeof(TileHeight)).Length - 1) {
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
                tileHeight = (TileHeight)System.Enum.GetValues(typeof(TileHeight)).Length - 1;
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
                selectorOffset + Hex.GetTileHeight(tileHeight),
                hoveredTile.transform.position.z
            );

        }
        else {
            hoveredTile = null;
            tileSelector.SetActive(false);
        }

    }

}