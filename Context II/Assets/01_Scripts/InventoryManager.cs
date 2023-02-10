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
            PlacementManager.TilePlaced += EmptySlot;
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

    private HexTile tileOne;
    private HexTile tileTwo;
    private HexTile tileThree;

    private bool isRotatingOne;
    private bool isRotatingTwo;
    private bool isRotatingThree;

    private int selectedSlot;

    public void OnUpdate() {

        if(Input.GetMouseButtonDown(1)) {
            SelectedType = TileType.None;
            isRotatingOne = false;
            isRotatingTwo = false;
            isRotatingThree = false;
            selectedSlot = 0;
        }

        RotateTiles();

    }

    public void FillSlots(TileType _slotOne, TileType _slotTwo, TileType _slotThree) {
        
        if(tileOne != null) {
            Destroy(tileOne.gameObject);
        }
        if(tileTwo != null) {
            Destroy(tileTwo.gameObject);
        }
        if(tileThree != null) {
            Destroy(tileThree.gameObject);
        }

        GameObject one = GameManager.Instance.GetTileByType(_slotOne);
        GameObject two = GameManager.Instance.GetTileByType(_slotTwo);
        GameObject three = GameManager.Instance.GetTileByType(_slotThree);

        tileOne = Instantiate(one, slotOne.position, one.transform.rotation, slotOne).GetComponent<HexTile>();
        tileOne.tileType = _slotOne;
        tileTwo = Instantiate(two, slotTwo.position, one.transform.rotation, slotTwo).GetComponent<HexTile>();
        tileTwo.tileType = _slotTwo;
        tileThree = Instantiate(three, slotThree.position, one.transform.rotation, slotThree).GetComponent<HexTile>();
        tileThree.tileType = _slotThree;

    }

    public void SelectSlot(int _index) {
        selectedSlot = _index;
        switch(_index) {
            case 1:
                if(tileOne != null) {
                    SelectedType = tileOne.tileType;
                    isRotatingOne = true;
                    isRotatingTwo = false;
                    isRotatingThree = false;
                }
                break;

            case 2:
                if(tileTwo != null) {
                    SelectedType = tileTwo.tileType;
                    isRotatingOne = false;
                    isRotatingTwo = true;
                    isRotatingThree = false;
                }
                break;

            case 3:
                if(tileThree != null) {
                    SelectedType = tileThree.tileType;
                    isRotatingOne = false;
                    isRotatingTwo = false;
                    isRotatingThree = true;
                }
                break;
        }
    }

    private void EmptySlot() {

        PlacementManager.TilePlaced -= EmptySlot;

        switch(selectedSlot) {
            case 1:
                if(tileOne != null) {
                    Destroy(tileOne.gameObject);
                }
                SelectedType = TileType.None;
                isRotatingOne = false;
                isRotatingTwo = false;
                isRotatingThree = false;
                selectedSlot = 0;
                break;

            case 2:
                if(tileTwo != null) {
                    Destroy(tileTwo.gameObject);
                }
                SelectedType = TileType.None;
                isRotatingOne = false;
                isRotatingTwo = false;
                isRotatingThree = false;
                selectedSlot = 0;
                break;

            case 3:
                if(tileThree != null) {
                    Destroy(tileThree.gameObject);
                }
                SelectedType = TileType.None;
                isRotatingOne = false;
                isRotatingTwo = false;
                isRotatingThree = false;
                selectedSlot = 0;
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