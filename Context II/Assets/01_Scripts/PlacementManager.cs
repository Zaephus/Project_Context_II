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

    private GameObject hoveredObject;

    public void Initialize() {
        StartCoroutine(CheckForTile());
    }

    public void OnUpdate() {

        if(hoveredObject != null) {
            if(GameManager.Instance.tiles[hoveredObject] == TileType.BaseTile) {

                tileSelector.GetComponent<MeshRenderer>().material.color = selectableColor;

                if(Input.GetMouseButtonDown(0)) {
                    GameManager.Instance.tiles.Remove(hoveredObject);
                    Vector3 pos = hoveredObject.transform.position;
                    pos.y += 0.4f;

                    Destroy(hoveredObject);
                    hoveredObject = null;

                    GameObject tile = Instantiate(tiles[0], pos, Quaternion.identity, GameManager.Instance.transform);
                    GameManager.Instance.tiles.Add(tile, TileType.FarmTile);
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
                hoveredObject = hit.collider.gameObject;
                tileSelector.GetComponent<MeshRenderer>().enabled = true;
                tileSelector.transform.position = new Vector3(hoveredObject.transform.position.x, tileSelector.transform.position.y, hoveredObject.transform.position.z);
            }
            else {
                hoveredObject = null;
                tileSelector.GetComponent<MeshRenderer>().enabled = false;
            }
            yield return new WaitForSeconds(0.05f);
        }

    }

}