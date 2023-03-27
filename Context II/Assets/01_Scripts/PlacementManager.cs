using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private bool isChecking = false;

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
    private Material grassMaterial;
    [SerializeField]
    private Material waterMaterial;
    
    [SerializeField]
    private Texture2D coloredTexture;
    [SerializeField]
    private Texture2D grayscaleTexture;

    public DialogueSystem dialogueSystem;
    
    [HideInInspector]
    public bool isPlacing = false;

    [SerializeField]
    private GameObject levelCanvas;
    [SerializeField]
    private GameObject dialogueCanvas;
    public Toggle placingToggle;

    private GameObject objectToInstantiate;
    private TileType selectedType;

    private Tile hoveredTile;

    [SerializeField]
    private TMP_Text windmillTargetText;

    [SerializeField]
    private int windmillTarget;
    [HideInInspector]
    public int currentWindmillAmount;

    [SerializeField]
    private float approvalModifier;

    public void OnStart() {

        windmillTargetText.text = currentWindmillAmount + "/" + windmillTarget;
        SetMaterialTextures(coloredTexture);

        CameraMovement.CursorLocked += ToggleChecking;
        CameraMovement.FinishedStartTransition += ShowUI;

    }

    public void OnUpdate() {

        if(IsChecking && !EventSystem.current.IsPointerOverGameObject()) {
            CheckForTile();
        }

        if(hoveredTile != null) {
            if(!isPlacing && hoveredTile.dialogueIndex != 0) {
                if(Input.GetMouseButtonDown(0)) {
                    ShowDialogue(DialogueDatabase.Instance.currentDialogueOptions[hoveredTile.dialogueIndex-1]);
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

            DialogueBubble.ToggledVisibility?.Invoke(false);

            selectedType = TileType.WindmillTile;
            windmillTargetText.gameObject.SetActive(true);
            SetMaterialTextures(grayscaleTexture);
        }
        else {
            tileSelector.SetActive(false);

            DialogueBubble.ToggledVisibility?.Invoke(true);

            selectedType = TileType.None;
            windmillTargetText.gameObject.SetActive(false);
            SetMaterialTextures(coloredTexture);
        }

    }

    private void ShowUI() {
        levelCanvas.gameObject.SetActive(true);
        IsChecking = true;
    }

    private void ToggleChecking(bool _value) {
        IsChecking = !_value;
        hoveredTile = null;
    }

    private void PlaceTile() {

        objectToInstantiate = TileDatabase.Instance.GetTileByType(selectedType);

        Vector3 tilePos = hoveredTile.transform.position;
        Vector3 tileRot = objectToInstantiate.transform.eulerAngles + new Vector3(0, Hex.GetTileRotation(TileRotation.ThreeSixth), 0);
        Vector3Int hexPos = hoveredTile.hexPosition;

        TileHeight tileHeight = hoveredTile.tileHeight;

        int tileIndex = GameManager.Instance.tiles.FindIndex(x => x == hoveredTile);

        GameManager.Instance.tiles.Remove(hoveredTile);

        Tile tile = Instantiate(objectToInstantiate, tilePos, Quaternion.Euler(tileRot), transform).GetComponent<Tile>();
        tile.hexPosition = hexPos;
        tile.tileType = selectedType;
        tile.tileHeight = tileHeight;
        tile.PowerApproval = hoveredTile.PowerApproval;
        tile.CitizenApproval = hoveredTile.CitizenApproval;

        WindmillPlaced?.Invoke((float)(tile.CitizenApproval / windmillTarget));

        Destroy(hoveredTile.gameObject);
        hoveredTile = null;

        GameManager.Instance.tiles.Insert(tileIndex, tile);

    }

    public void ShowDialogue(DialogueOption _option) {

        levelCanvas.SetActive(false);
        dialogueCanvas.SetActive(true);

        dialogueSystem.StartDialogue(_option);

        DialogueSystem.DialogueEnded += DialogueEnded;

        IsChecking = false;
        hoveredTile = null;

    }

    private void DialogueEnded() {

        levelCanvas.SetActive(true);
        dialogueCanvas.SetActive(false);

        Tile.ChangedCitizenApproval?.Invoke(approvalModifier);

        DialogueSystem.DialogueEnded -= DialogueEnded;

        IsChecking = true;

    }

    public void UpdateWindmillTarget(int _amount) {
        
        currentWindmillAmount = _amount;
        windmillTargetText.text = currentWindmillAmount + "/" + windmillTarget;

        if(currentWindmillAmount >= windmillTarget) {
            placingToggle.isOn = !placingToggle.isOn;
            WindmillTargetReached?.Invoke();
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
        SetMaterialTextures(coloredTexture);
    }

    private void SetMaterialTextures(Texture2D _texture) {
        tilesMaterial.mainTexture = _texture;
        grassMaterial.mainTexture = _texture;
        waterMaterial.SetTexture("_baseTexture", _texture);
    }

}