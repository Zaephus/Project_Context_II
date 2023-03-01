using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Approval Material Database", menuName = "Scriptable Objects/Approval Material Database")]
public class AppMatDatabase : SingletonScriptableObject<AppMatDatabase> {
    
    public Material[] powerApprovalMaterials;
    public Material[] citizenApprovalMaterials;

    public Material GetApprovalMaterial(ApprovalType _type, float _value) {
        switch(_type) {
            case ApprovalType.Power:
                return powerApprovalMaterials[(int)_value-1];
            case ApprovalType.Citizen:
                return citizenApprovalMaterials[(int)_value-1];
            default:
                return null;
        }
    }

}