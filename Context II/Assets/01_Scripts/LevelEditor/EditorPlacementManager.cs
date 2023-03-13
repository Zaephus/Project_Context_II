using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    private Slider slider;
    [SerializeField]
    private TMP_Text sliderTitleText;
    [SerializeField]
    private TMP_Text sliderValueText;

    private int sliderValue = 5;

    [SerializeField]
    private TMP_Dropdown tileDropdown;

    [SerializeField]
    private Toggle powerApprovalToggle;
    [SerializeField]
    private Toggle citizenApprovalToggle;

    public GameObject dialogueIndicatorPrefab;
    public Dictionary<Vector3, GameObject> dialogueIndicators = new Dictionary<Vector3, GameObject>();

    private bool isEditingDialogue = false;

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

        List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();

        for(int i = 0; i < System.Enum.GetNames(typeof(TileType)).Length; i++) {
            dropdownOptions.Add(new TMP_Dropdown.OptionData(System.Enum.GetNames(typeof(TileType))[i]));
        }

        tileDropdown.AddOptions(dropdownOptions);

        EditorCameraMovement.CursorLocked += ToggleSelection;
    }

    public void OnUpdate() {

        SetTileRotation();
        SetTileHeight();

        if(IsChecking && !EventSystem.current.IsPointerOverGameObject()) {
            CheckForTile();
        }

        if(hoveredTile != null && (selectedTileType != TileType.None || selectedApprovalType != ApprovalType.None || isEditingDialogue)) {
            if(Input.GetMouseButtonDown(0)) {
                if(selectedTileType != TileType.None) {
                    PlaceTile();
                }
                else if(selectedApprovalType != ApprovalType.None) {
                    SetTileApproval();
                }
                else if(isEditingDialogue) {
                    SetTileDialogue();
                }
            }
        }

    }

    public void ToggleSelection(bool _value) {
        IsChecking = !_value;
        hoveredTile = null;
    }

    public void ChangeTileSelection() {

        selectedTileType = (TileType)tileDropdown.value;

        if(selectedTileType == TileType.None) {
            IsChecking = false;
        }
        else {
            powerApprovalToggle.isOn = false;
            citizenApprovalToggle.isOn = false;
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
            sliderTitleText.text = "Approval:";
            IsChecking = true;
            tileDropdown.value = 0;
        }
        
    }

    public void ChangeDialogueSelection() {

        isEditingDialogue = !isEditingDialogue;

        if(isEditingDialogue) {
            sliderTitleText.text = "Dialogue Option:";
            tileDropdown.value = 0;
            powerApprovalToggle.isOn = false;
            citizenApprovalToggle.isOn = false;
            IsChecking = true;
        }
        else {
            isChecking = false;
        }

    }

    public void SliderValueChanged() {
        sliderValue = (int)slider.value;
        sliderValueText.text = sliderValue.ToString();
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
                hoveredTile.PowerApproval = sliderValue;
                break;
            
            case ApprovalType.Citizen:
                hoveredTile.CitizenApproval = sliderValue;
                break;
        }
    }

    private void SetTileDialogue() {

        hoveredTile.dialogueIndex = sliderValue;

        if(dialogueIndicators.ContainsKey(hoveredTile.hexPosition)) {
            if(sliderValue == 0) {
                Destroy(dialogueIndicators[hoveredTile.hexPosition]);
                dialogueIndicators.Remove(hoveredTile.hexPosition);
            }
            dialogueIndicators[hoveredTile.hexPosition].GetComponent<TMP_Text>().text = sliderValue.ToString();
        }
        else if(sliderValue > 0) {
            Vector3 indicatorPos = hoveredTile.transform.position + new Vector3(0, 1.5f, 0);
            GameObject indicator = Instantiate(dialogueIndicatorPrefab, indicatorPos, dialogueIndicatorPrefab.transform.rotation, hoveredTile.transform);

            indicator.GetComponent<TMP_Text>().text = sliderValue.ToString();

            dialogueIndicators.Add(hoveredTile.hexPosition, indicator);
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
        if(!Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y > 0) {
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
        else if(!Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y < 0) {
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