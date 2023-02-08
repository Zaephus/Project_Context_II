using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour {

    [SerializeField]
    private GameObject tileSelector;

    [SerializeField]
    private float selectorOffset;

    private Dictionary<GameObject, TileType> tiles = new Dictionary<GameObject, TileType>();

    public void Initialize(Dictionary<GameObject, TileType> _tiles) {
        tiles = _tiles;
    }

    public void OnUpdate() {

        StartCoroutine(CheckForTile());

    }

    private IEnumerator CheckForTile() {

        yield return new WaitForSeconds(0.2f);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit)) {
            tileSelector.GetComponent<MeshRenderer>().enabled = true;
            tileSelector.transform.position = new Vector3(hit.collider.transform.position.x, tileSelector.transform.position.y, hit.collider.transform.position.z);
        }
        else {
            tileSelector.GetComponent<MeshRenderer>().enabled = false;
        }

    }

}