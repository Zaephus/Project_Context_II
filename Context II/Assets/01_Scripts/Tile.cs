using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public static System.Action<bool> TogglePowerApprovalVisibility;
    public static System.Action<bool> ToggleCitizenApprovalVisibility;

    public static System.Action<float> ChangedCitizenApproval;

    public Vector3Int hexPosition;
    public TileRotation tileRotation;
    public TileHeight tileHeight;
    public TileType tileType;

    public int dialogueIndex;

    [SerializeField]
    private MeshRenderer powerApprovalRenderer;
    [SerializeField]
    private MeshRenderer citizenApprovalRenderer;


    public float PowerApproval {
        get {
            return powerApproval;
        }
        set {
            powerApproval = value;
            if(powerApprovalRenderer != null) {
                powerApprovalRenderer.material = AppMatDatabase.Instance.GetApprovalMaterial(ApprovalType.Power, value);
                // powerApprovalRenderer.transform.localScale = new Vector3(powerApprovalRenderer.transform.localScale.x, 0.1f, powerApprovalRenderer.transform.localScale.z);
            }
        }
    }
    private float powerApproval;

    public float CitizenApproval {
        get {
            return citizenApproval;
        }
        set {
            citizenApproval = value;
            if(citizenApprovalRenderer != null) {
                citizenApprovalRenderer.material = AppMatDatabase.Instance.GetApprovalMaterial(ApprovalType.Citizen, value);
                // citizenApprovalRenderer.transform.localScale = new Vector3(citizenApprovalRenderer.transform.localScale.x, 0.1f, citizenApprovalRenderer.transform.localScale.z);
            }
        }
    }
    private float citizenApproval;

    private void Awake() {
        TogglePowerApprovalVisibility += ChangePowerApprovalVisibility;
        ToggleCitizenApprovalVisibility += ChangeCitizenApprovalVisibility;

        ChangedCitizenApproval += ChangeCitizenApproval;
    }

    private void OnDestroy() {
        TogglePowerApprovalVisibility -= ChangePowerApprovalVisibility;
        ToggleCitizenApprovalVisibility -= ChangeCitizenApprovalVisibility;

        ChangedCitizenApproval += ChangeCitizenApproval;
    }

    private void ChangePowerApprovalVisibility(bool _enabled) {
        powerApprovalRenderer.enabled = _enabled;
        PowerApproval = PowerApproval;
    }

    private void ChangeCitizenApprovalVisibility(bool _enabled) {
        citizenApprovalRenderer.enabled = _enabled;
        CitizenApproval = CitizenApproval;
    }

    private void ChangeCitizenApproval(float _amount) {
        if(CitizenApproval != 0 && _amount != 0) {
            CitizenApproval = _amount;
        }
    }

}