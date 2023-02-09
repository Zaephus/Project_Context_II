using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public static Action<TileType> SelectedTypeChanged;

    private TileType SelectedType {
        get {
            return selectedType;
        }
        set {
            selectedType = value;
            SelectedTypeChanged?.Invoke(value);
        }
    }
    [SerializeField]
    private TileType selectedType = TileType.None;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private Transform slotOne;
    [SerializeField]
    private Transform slotTwo;
    [SerializeField]
    private Transform slotThree;

    [SerializeField]
    private HexTile tileOne;
    [SerializeField]
    private HexTile tileTwo;
    [SerializeField]
    private HexTile tileThree;

    private bool isRotatingOne;
    private bool isRotatingTwo;
    private bool isRotatingThree;

    public void OnUpdate() {

        if(Input.GetMouseButtonDown(1)) {
            SelectedType = TileType.None;
            isRotatingOne = false;
            isRotatingTwo = false;
            isRotatingThree = false;
        }

        RotateTiles();

    }

    public void SelectSlot(int _index) {
        switch(_index) {
            case 1:
                SelectedType = tileOne.tileType;
                isRotatingOne = true;
                isRotatingTwo = false;
                isRotatingThree = false;
                break;

            case 2:
                SelectedType = tileTwo.tileType;
                isRotatingOne = false;
                isRotatingTwo = true;
                isRotatingThree = false;
                break;

            case 3:
                SelectedType = tileThree.tileType;
                isRotatingOne = false;
                isRotatingTwo = false;
                isRotatingThree = true;
                break;
        }
    }

    private void RotateTiles() {

        if(isRotatingOne) {
            tileOne.transform.localEulerAngles += new Vector3(0, rotateSpeed, 0);
        }

        if(isRotatingTwo) {
            tileTwo.transform.localEulerAngles += new Vector3(0, rotateSpeed, 0);
        }

        if(isRotatingThree) {
            tileThree.transform.localEulerAngles += new Vector3(0, rotateSpeed, 0);
        }

    }

}