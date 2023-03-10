using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PlacementManager : MonoBehaviour {

    public System.Action WindmillTargetReached;
    public System.Action<float> WindmillPlaced;

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
    private bool isChecking = true;

    [SerializeField]
    private GameObject tileSelector;

    [SerializeField]
    private float selectorOffset;

    [SerializeField]
    private Color selectableColor;
    [SerializeField]
    private Color unSelectableColor;

    [SerializeField]
    private Material tilesMaterial;
    
    [SerializeField]
    private Texture2D coloredTexture;
    [SerializeField]
    private Texture2D grayscaleTexture;

    [SerializeField]
    private DialogueSystem dialogueSystem;
    
    private bool isPlacing = false;

    [SerializeField]
    private GameObject levelCanvas;
    [SerializeField]
    private GameObject dialogueCanvas;

    private GameObject objectToInstantiate;
    private TileType selectedType;

    private Tile hoveredTile;

    [SerializeField]
    private TMP_Text windmillTargetText;

    [SerializeField]
    private int windmillTarget;
    private int currentWindmillAmount;

    private GameState gameState;

    public void OnStart(GameState _state) {
        gameState = _state;
        currentWindmillAmount = 0;
        windmillTargetText.text = currentWindmillAmount + "/" + windmillTarget;
        tilesMaterial.mainTexture = coloredTexture;

        CameraMovement.CursorLocked += ToggleChecking;
    }

    public void OnUpdate() {

        if(IsChecking && !EventSystem.current.IsPointerOverGameObject()) {
            CheckForTile();
        }

        if(hoveredTile != null) {
            if(!isPlacing && hoveredTile.dialogueIndex != 0 && gameState == GameState.StageTwo) {
                if(Input.GetMouseButtonDown(0)) {
                    ShowDialogue();
                }
            }
            else if(CheckPossiblePlacement() && selectedType != TileType.None) {
                if(Input.GetMouseButtonDown(0)) {
                    PlaceTile();
                    UpdateWindmillTarget(currentWindmillAmount + 1);
                }
            }
        }

    }

    public void ToggleSelection() {

        isPlacing = !isPlacing;

        if(isPlacing) {
            tileSelector.SetActive(true);

            selectedType = TileType.WindmillTile;
            windmillTargetText.gameObject.SetActive(true);
            tilesMaterial.mainTexture = grayscaleTexture;
            Tile.TogglePowerApprovalVisibility?.Invoke(true);
        }
        else {
            tileSelector.SetActive(false);

            selectedType = TileType.None;
            windmillTargetText.gameObject.SetActive(false);
            tilesMaterial.mainTexture = coloredTexture;
            Tile.TogglePowerApprovalVisibility?.Invoke(false);
        }

    }

    private void ToggleChecking(bool _value) {
        IsChecking = !_value;
        hoveredTile = null;
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

        tile.ChangePowerApprovalVisibility(true);

        WindmillPlaced?.Invoke((float)(tile.CitizenApproval / windmillTarget));

        Destroy(hoveredTile.gameObject);
        hoveredTile = null;

        GameManager.Instance.tiles.Insert(tileIndex, tile);

    }

    private void ShowDialogue() {

        levelCanvas.SetActive(false);
        dialogueCanvas.SetActive(true);

        dialogueSystem.StartDialogue(hoveredTile.dialogueIndex);

        dialogueSystem.DialogueEnded += DialogueEnded;

        IsChecking = false;
        hoveredTile = null;

    }

    private void DialogueEnded() {

        levelCanvas.SetActive(true);
        dialogueCanvas.SetActive(false);

        dialogueSystem.DialogueEnded -= DialogueEnded;

        IsChecking = true;

    }

    private void UpdateWindmillTarget(int _amount) {
        
        currentWindmillAmount = _amount;
        windmillTargetText.text = currentWindmillAmount + "/" + windmillTarget;

        if(currentWindmillAmount >= windmillTarget) {
            WindmillTargetReached?.Invoke();
            if(IsChecking) {
                ToggleSelection();
            }
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
            
            if(isPlacing) {
                tileSelector.SetActive(true);

                tileSelector.transform.position = new Vector3(
                    hoveredTile.transform.position.x,
                    hoveredTile.transform.position.y + selectorOffset,
                    hoveredTile.transform.position.z
                );
            }

        }
        else {
            hoveredTile = null;
            tileSelector.SetActive(false);
        }

    }

    private bool CheckPossiblePlacement() {

        if(hoveredTile.dialogueIndex != 0 || hoveredTile.tileType == TileType.WindmillTile) {
            tileSelector.GetComponent<MeshRenderer>().material.color = unSelectableColor;
            return false;
        }

        tileSelector.GetComponent<MeshRenderer>().material.color = selectableColor;
        return true;

    }

    private void OnDisable() {
        tilesMaterial.mainTexture = coloredTexture;
    }

}